using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 880;
            this.Height = 680;

            this.tlpMain.Dock = DockStyle.Fill;
            this.rtxtSource.Dock = DockStyle.Fill;
            this.rtxtTarget.Dock = DockStyle.Fill;
            this.tlpBtn.Dock = DockStyle.Fill;
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                string source = this.rtxtSource.Text;

                if (cboxUpper.Checked)
                    source = ToUpper(source);
                     
                if (cboxParam.Checked)
                    source = ToMSSParam(source);

                if (cboxNull.Checked)
                    source = ToNull(source, ':');

                if (cboxNull2.Checked)
                    source = ToNull(source, '@');

                if (cboxDefine.Checked)
                    source = ToDefineParam(source);

                if (cboxReplace.Checked)
                    source = ToReplace(source, this.txtReplace.Text);

                if (cboxSQL.Checked)
                    source = ToSQL(source);

                this.rtxtTarget.Text = source;

                this.rtxtTarget.Focus();
                this.rtxtTarget.SelectAll();
                this.rtxtTarget.Copy(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ToSQL(string source)
        {
           return NSQLFormatter.Formatter.Format(source);
        }

        private string ToUpper(string source)
        {
            return source?.ToUpper();
        }

        private string ToReplace(string source,string oldValue)
        {
            string[] str = oldValue?.Split(',');
            if(str!=null && str.Length > 0)
            {
                foreach (var item in str)
                {
                    source = source?.Replace(item, "");
                }
            }

            return source;
        }

        private string ToMSSParam(string source)
        {
            return source?.Replace(":", "@");
        }

        /// <summary>
        /// 把参数化转换成 ''
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string ToNull(string source, char param)
        {
            int count = source.Count(t => t == param);
            int startIndex = 0;
            List<string> list = new List<string>();   //有可能包含字符串 ":"并不是 :Param
            for (int i = 0; i < count; i++)
            {
                startIndex = source.IndexOf(param, startIndex);
                string name = source[startIndex].ToString();// :
                for (int j = startIndex + 1; j < source.Length; j++)
                {
                    startIndex++;
                    if (source[j] == ' ' || !IsValid(source[j].ToString()))    //1.空格  2.不是 A–Z, a–z, 0–9, _
                    {
                        break;
                    }
                    name += source[j];
                }
                if (name != param.ToString())
                    list.Add(name);
            }

            //:Name :Name1 :Name2  可能参数有相同部分 替换顺序 先从最长的开始
            int len = list.Count;
            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - i - 1; j++)
                {
                    if (list[j].Length < list[j + 1].Length)
                    {
                        string temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }

            foreach (var item in list)
            {
                source = source.Replace(item, "''");
            }

            return source;
        }

        private bool IsValid(string str)
        {
            Regex reg = new Regex("[0-9]|[a-z]|[A-Z]|[_]");

            return reg.IsMatch(str);
        }

        /// <summary>
        /// ${} ->''    
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string ToDefineParam(string source, char param = '$')
        {
            int count = source.Count(t => t == param);
            int startIndex = 0;
            List<string> list = new List<string>();

            //$"SELECT {appcom.GetGenID("APPROVAL", "AP.RAWID")} AS APPROVAL_ID " +
            //$"   ,{sfasfas} 
            for (int i = 0; i < count; i++)
            {
                startIndex = source.IndexOf(param, startIndex); // $
                startIndex = source.IndexOf('{', startIndex); //{                                                
                string name = source[startIndex].ToString(); //{
                for (int j = startIndex + 1; j < source.Length; j++)
                {
                    startIndex++;
                    name += source[j];
                    if (source[j] == '}') 
                    {
                        break;
                    }
                }
                if (name != param.ToString())
                    list.Add(name);
            }

            //{Name} {Name1} {Name2}  可能参数有相同部分 替换顺序 先从最长的开始
            int len = list.Count;
            for (int i = 0; i < len - 1; i++)
            {
                for (int j = 0; j < len - i - 1; j++)
                {
                    if (list[j].Length < list[j + 1].Length)
                    {
                        string temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }

            source = source.Replace(param, ' ');
            foreach (var item in list)
            {
                source = source.Replace(item, "''");
            }

            return source;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

        }
    }
}
