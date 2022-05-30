using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormat.NSQLFormatter
{
    /// <summary>
    /// A StringTokenizer java like object 
    /// </summary>
    public class StringTokenizer : IEnumerable<string>
    {
        private const string _defaultDelim = " \t\n\r\f";
        public string _origin;
        public string _delim;

        /// <summary>
        /// 遇到默认字符是否返回
        /// </summary>
        public bool _returnDelim;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public StringTokenizer(string str)
        {
            _origin = str;
            _delim = _defaultDelim;
            _returnDelim = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delim"></param>
        public StringTokenizer(string str, string delim)
        {
            _origin = str;
            _delim = delim;
            _returnDelim = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delim"></param>
        /// <param name="returnDelims"></param>
        public StringTokenizer(string str, string delim, bool returnDelims)
        {
            _origin = str.Replace("  ", " ");
            _delim = delim;
            _returnDelim = returnDelims;
        }

        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return new StringTokenizerEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new StringTokenizerEnumerator(this);
        }

        #endregion


        
    }
}
