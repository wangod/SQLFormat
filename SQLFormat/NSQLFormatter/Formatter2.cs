using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormat.NSQLFormatter
{
    public class Formatter2
    {
        public const string WhiteSpace = " \n\r\f\t";//空格 回车 换行

        /// <summary>
        /// 缩排字符
        /// </summary>
        protected const string IndentString = "    ";
        /// <summary>
        /// 初始的格式
        /// </summary>
        protected const string Initial = "";
        protected static readonly HashSet<string> beginClauses = new HashSet<string>();
        protected static readonly HashSet<string> dml = new HashSet<string>();
        protected static readonly HashSet<string> endClauses = new HashSet<string>();
        protected static readonly HashSet<string> logical = new HashSet<string>();
        protected static readonly HashSet<string> misc = new HashSet<string>();
        protected static readonly HashSet<string> quantifiers = new HashSet<string>();

        static Formatter2()
        {
            beginClauses.Add("left");
            beginClauses.Add("right");
            beginClauses.Add("inner");
            beginClauses.Add("outer");
            beginClauses.Add("group");
            beginClauses.Add("order");

            endClauses.Add("where");
            endClauses.Add("set");
            endClauses.Add("having");
            endClauses.Add("join");
            endClauses.Add("from");
            endClauses.Add("by");
            endClauses.Add("into");
            endClauses.Add("union");

            logical.Add("and");
            logical.Add("or");
            logical.Add("when");
            logical.Add("else");
            logical.Add("end");

            quantifiers.Add("in");
            quantifiers.Add("all");
            quantifiers.Add("exists");
            quantifiers.Add("some");
            quantifiers.Add("any");

            dml.Add("insert");  //insert into
            dml.Add("update");
            dml.Add("delete");

            misc.Add("select"); //select ... into
            misc.Add("on");
        }

        public static string Format(string source)
        {
            return new FormatProcess(source).Perform();
        }

        #region Nested type: FormatProcess

        private class FormatProcess
        {
            private readonly List<bool> afterByOrFromOrSelects = new List<bool>();
            private readonly List<int> parenCounts = new List<int>();
            private readonly StringBuilder result = new StringBuilder();
            private readonly IEnumerator<string> tokens;
            private bool afterBeginBeforeEnd;
            private bool afterBetween;
            private bool afterByOrSetOrFromOrSelect;
            private bool afterInsert;

            /// <summary>
            /// 是否在On的后面
            /// </summary>
            private bool afterOn = false;
            /// <summary>
            /// 是否是行的开始
            /// </summary>
            private bool beginLine = true;
            private bool endCommandFound;

            /// <summary>
            /// 缩排数量
            /// </summary>
            private int indent = 1;
            private int inFunction;

            private string lastToken;
            private int parensSinceSelect;
            /// <summary>
            /// 当前字符串
            /// </summary>
            private string token;
            /// <summary>
            /// 当前字符串(小写化)
            /// </summary>
            private string lcToken;

            public FormatProcess(string sql)
            {
                // TODO : some delimiter may depend from a specific Dialect/Drive (as ';' to separate multi query)
                tokens = new StringTokenizer(sql, "()+*/-=<>'`\"[],;"+ WhiteSpace, true).GetEnumerator();
            }

            public string Perform()
            {
                result.Append(Initial);

                while (tokens.MoveNext())
                {
                    token = tokens.Current;
                    lcToken = token.ToLowerInvariant();

                    if ("'".Equals(token))      
                    {
                        ExtractStringEnclosedBy("'");
                    }
                    else if ("\"".Equals(token))
                    {
                        ExtractStringEnclosedBy("\"");
                    }

                    if (";".Equals(token))
                    {
                        StartingNewQuery();
                    }
                    else if (",".Equals(token))
                    {
                        if (afterByOrSetOrFromOrSelect)
                        {
                            CommaAfterByOrFromOrSelect();
                        }
                        else if (afterOn)
                        {
                            CommaAfterOn();
                        }
                    }
                    else if ("(".Equals(token))
                    {
                        OpenParen();
                    }
                    else if (")".Equals(token))
                    {
                        CloseParen();
                    }


                    if (beginClauses.Contains(lcToken))
                    {
                        BeginNewClause();
                    }
                    else if (endClauses.Contains(lcToken))
                    {
                        EndNewClause();
                    }
                    else if ("select".Equals(lcToken))
                    {
                        Select();
                    }
                    else if (dml.Contains(lcToken))
                    {
                        UpdateOrInsertOrDelete();
                    }
                    else if ("values".Equals(lcToken))
                    {
                        Values();
                    }
                    else if ("on".Equals(lcToken))
                    {
                        On();
                    }
                    else if ("and".Equals(lcToken) && afterBetween)
                    {
                        Misc();
                        afterBetween = false;
                    }
                    else if (logical.Contains(lcToken))
                    {
                        Logical();
                    }
                    else if (IsWhitespace(token))
                    {
                        //White();
                    }
                    else
                    {
                        Misc();
                    }

                    if (!IsWhitespace(token))
                    {
                        lastToken = lcToken;
                    }
                }
                return result.ToString();
            }

            private void StartingNewQuery()
            {
                Out();
                indent = 1;
                endCommandFound = true;
                NewLine();
            }

            /// <summary>
            /// 多查询分隔符 ;
            /// </summary>
            /// <param name="delimiter"></param>
            /// <returns></returns>
            private bool IsMultiQueryDelimiter(string delimiter)
            {
                return ";".Equals(delimiter);
            }

            /// <summary>
            /// 提取指定字符 '***'
            /// </summary>
            /// <param name="stringDelimiter"></param>
            private void ExtractStringEnclosedBy(string stringDelimiter)
            {
                while (tokens.MoveNext())
                {
                    string t = tokens.Current;
                    token += t;
                    if (stringDelimiter.Equals(t))
                    {
                        break;
                    }
                }
            }

            /// <summary>
            /// 逗号在ON后面
            /// </summary>
            private void CommaAfterOn()
            {
                Out();
                indent--;
                NewLine();
                afterOn = false;
                afterByOrSetOrFromOrSelect = true;
            }

            /// <summary>
            /// 逗号在By、From、Select后面
            /// select A,
            ///        B
            /// </summary>
            private void CommaAfterByOrFromOrSelect()
            {
                TrimEndSpace();
                Out();
                NewLineAfterComma();
            }

            /// <summary>
            /// 逗号在By、From、Select后面
            /// select A
            ///      , B
            /// </summary>
            private void CommaAfterByOrFromOrSelect2()
            {
                Out();
                NewLine();
            }

            private void Logical()
            {
                if ("end".Equals(lcToken))
                {
                    indent--;
                }
                NewLine();
                Out();
                beginLine = false;
            }

            private void On()
            {
                indent++;
                afterOn = true;
                NewLine();
                Out();
                beginLine = false;
            }

            private void Misc()
            {
                Out();
                if ("between".Equals(lcToken))
                {
                    afterBetween = true;
                }
                if (afterInsert)
                {
                    NewLine();
                    afterInsert = false;
                }
                else
                {
                    beginLine = false;
                    if ("case".Equals(lcToken))
                    {
                        indent++;
                    }
                }
            }

            private void White()
            {
                if (!beginLine)
                {
                    result.Append(" ");
                }
            }

            private void UpdateOrInsertOrDelete()
            {
                Out();
                indent++;
                beginLine = false;
                if ("update".Equals(lcToken))
                {
                    NewLine();
                }
                if ("insert".Equals(lcToken))
                {
                    afterInsert = true;
                }
                endCommandFound = false;
            }

            /// <summary>
            /// select A,
            ///        B
            /// </summary>
            private void Select()
            {
                Out();
                //indent++;
                //NewLine();
                parenCounts.Insert(parenCounts.Count, parensSinceSelect);
                afterByOrFromOrSelects.Insert(afterByOrFromOrSelects.Count, afterByOrSetOrFromOrSelect);
                parensSinceSelect = 0;
                afterByOrSetOrFromOrSelect = true;
                endCommandFound = false;
            }

            /// <summary>
            /// 输出
            /// </summary>
            private void Out()
            {
                result.Append(token + " ");
            }

            private void EndNewClause()
            {
                if (!afterBeginBeforeEnd)
                {
                    indent--;
                    if (afterOn)
                    {
                        indent--;
                        afterOn = false;
                    }
                    NewLine();
                }
                Out();
                if (!"union".Equals(lcToken))
                {
                    indent++;
                }
                NewLine();
                afterBeginBeforeEnd = false;
                afterByOrSetOrFromOrSelect = "by".Equals(lcToken) || "set".Equals(lcToken) || "from".Equals(lcToken);
            }

            private void BeginNewClause()
            {
                if (!afterBeginBeforeEnd)
                {
                    if (afterOn)
                    {
                        indent--;
                        afterOn = false;
                    }
                    indent--;
                    NewLine();
                }
                Out();
                beginLine = false;
                afterBeginBeforeEnd = true;
            }

            private void Values()
            {
                indent--;
                NewLine();
                Out();
                indent++;
                NewLine();
            }

            private void CloseParen()
            {
                if (endCommandFound)
                {
                    Out();
                    return;
                }
                parensSinceSelect--;
                if (parensSinceSelect < 0)
                {
                    indent--;
                    int tempObject = parenCounts[parenCounts.Count - 1];
                    parenCounts.RemoveAt(parenCounts.Count - 1);
                    parensSinceSelect = tempObject;

                    bool tempObject2 = afterByOrFromOrSelects[afterByOrFromOrSelects.Count - 1];
                    afterByOrFromOrSelects.RemoveAt(afterByOrFromOrSelects.Count - 1);
                    afterByOrSetOrFromOrSelect = tempObject2;
                }
                if (inFunction > 0)
                {
                    inFunction--;
                    Out();
                }
                else
                {
                    if (!afterByOrSetOrFromOrSelect)
                    {
                        indent--;
                        NewLine();
                    }
                    Out();
                }
                beginLine = false;
            }

            private void OpenParen()
            {
                if (endCommandFound)
                {
                    Out();
                    return;
                }
                if (IsFunctionName(lastToken) || inFunction > 0)
                {
                    inFunction++;
                }
                beginLine = false;
                if (inFunction > 0)
                {
                    Out();
                }
                else
                {
                    Out();
                    if (!afterByOrSetOrFromOrSelect)
                    {
                        indent++;
                        NewLine();
                        beginLine = true;
                    }
                }
                parensSinceSelect++;
            }

            private static bool IsFunctionName(string tok)
            {
                char begin = tok[0];
                bool isIdentifier = (char.IsLetter(begin) || begin.CompareTo('$') == 0 || begin.CompareTo('_') == 0) || '"' == begin;
                return isIdentifier && !logical.Contains(tok) && !endClauses.Contains(tok) && !quantifiers.Contains(tok)
                       && !dml.Contains(tok) && !misc.Contains(tok);
            }

            private static bool IsWhitespace(string token)
            {
                return WhiteSpace.IndexOf(token) >= 0;
            }

            /// <summary>
            /// 新行
            /// </summary>
            private void NewLine()
            {
                result.Append("\n");
                for (int i = 0; i < indent; i++)
                {
                    result.Append(IndentString);
                }
                beginLine = true;
            }

            /// <summary>
            /// 新行在逗号后面
            /// </summary>
            private void NewLineAfterComma()
            {
                result.Append("\n");



                beginLine = true;
            }

            /// <summary>
            /// 去掉最后空格
            /// </summary>
            private void TrimEndSpace()
            {
                string s = result.ToString().TrimEnd();
                result.Clear();
                result.Append(s);
            }
        }

        #endregion
    }
}
