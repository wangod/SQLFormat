using SQLFormat.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SQLFormat
{
    public partial class SettingForm : Form
    {
        DBInfo _dbInfo = null;
        private IDictionary<string, string> _singleDic = null;
        private IDictionary<string, List<IDictionary<string, string>>> _multipleDic = null;

        public SettingForm(DBInfo dbInfo)
        {
            InitializeComponent();

            this.tlpOracle.Dock = DockStyle.Fill;
            this.tlpGauss.Dock = DockStyle.Fill;

            _dbInfo = dbInfo;

            InitData(_dbInfo);
        }

        private void InitData(DBInfo dbInfo)
        {
            this.txtBoxIP.Text = dbInfo.OracleDB.IP;
            this.txtBoxPort.Text = dbInfo.OracleDB.Port;
            this.txtBoxServiceName.Text = dbInfo.OracleDB.ServiceName;
            this.txtBoxUserID.Text = dbInfo.OracleDB.UserID;
            this.txtBoxPassword.Text = dbInfo.OracleDB.Password;

            this.txtBoxGaussIP.Text = dbInfo.GaussDB.IP;
            this.txtBoxGaussPort.Text = dbInfo.GaussDB.Port;            
            this.txtBoxGaussUserID.Text = dbInfo.GaussDB.UserID;
            this.txtBoxGaussPassword.Text = dbInfo.GaussDB.Password;

            _singleDic = new Dictionary<string, string>();
            _multipleDic = new Dictionary<string, List<IDictionary<string, string>>>();
        }

        private void btnOracle_Click(object sender, EventArgs e)
        {
            try
            {
                _dbInfo.OracleDB.IP = this.txtBoxIP.Text.Trim();
                _dbInfo.OracleDB.Port = this.txtBoxPort.Text.Trim();
                _dbInfo.OracleDB.ServiceName = this.txtBoxServiceName.Text.Trim();
                _dbInfo.OracleDB.UserID = this.txtBoxUserID.Text.Trim();
                _dbInfo.OracleDB.Password = this.txtBoxPassword.Text.Trim();

                _singleDic.Clear();
                _singleDic.Add($"/DBInfo/OracleDB/IP", _dbInfo.OracleDB.IP);
                _singleDic.Add($"/DBInfo/OracleDB/Port", _dbInfo.OracleDB.Port);
                _singleDic.Add($"/DBInfo/OracleDB/ServiceName", _dbInfo.OracleDB.ServiceName);
                _singleDic.Add($"/DBInfo/OracleDB/UserID", _dbInfo.OracleDB.UserID);
                _singleDic.Add($"/DBInfo/OracleDB/Password", _dbInfo.OracleDB.Password);

                UpdateToXml();

                MessageBox.Show("保存成功");
            }
            catch
            {

            }
            
        }

        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                _dbInfo.GaussDB.IP = this.txtBoxGaussIP.Text.Trim();
                _dbInfo.GaussDB.Port = this.txtBoxGaussPort.Text.Trim();
                _dbInfo.GaussDB.UserID = this.txtBoxGaussUserID.Text.Trim();
                _dbInfo.GaussDB.Password = this.txtBoxGaussPassword.Text.Trim();

                _singleDic.Clear();
                _singleDic.Add($"/DBInfo/GaussDB/IP", _dbInfo.GaussDB.IP);
                _singleDic.Add($"/DBInfo/GaussDB/Port", _dbInfo.GaussDB.Port);
                _singleDic.Add($"/DBInfo/GaussDB/UserID", _dbInfo.GaussDB.UserID);
                _singleDic.Add($"/DBInfo/GaussDB/Password", _dbInfo.GaussDB.Password);

                UpdateToXml();

                MessageBox.Show("保存成功");
            }
            catch
            {

            }
        }
        
        private void UpdateToXml()
        {
            XmlUtil.ModifyAttribute("db.xml", (t) =>
            {
                //单个 存在更新,不存在添加
                foreach (var item in _singleDic)
                {
                    XmlNode xmlNode = t.SelectSingleNode(item.Key);
                    if (xmlNode != null)
                        xmlNode.InnerText = item.Value;
                    else
                    {
                        CreateSingleNode(t, item.Key, item.Value);
                    }
                }

                //多个 无法知道更新的是哪个子项 只能先删除再添加
                foreach (var item in _multipleDic)
                {
                    string[] arr = item.Key.Split('|');
                    string xpath = arr[0];
                    XmlNode node = t.SelectSingleNode(xpath);
                    if (node == null)
                    {
                        node = CreateSingleNode(t, xpath, "");
                    }
                    else
                    {
                        node.RemoveAll();
                    }

                    foreach (var dic in item.Value)
                    {
                        XmlNode subNode = t.CreateElement(arr[1]);
                        node.AppendChild(subNode);

                        foreach (var sub in dic)
                        {
                            XmlNode newNode = t.CreateElement(sub.Key);
                            newNode.InnerText = sub.Value;
                            subNode.AppendChild(newNode);
                        }
                    }
                }
            });
        }

        private XmlNode CreateSingleNode(XmlDocument xml, string xpath, string innerText)
        {
            string[] arr = xpath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string xp = string.Empty;
            int i = 0;
            XmlNode parentNode = null;
            foreach (string item in arr)//根节点是存在的
            {
                xp += "/" + item;
                if (i != 0)
                {
                    if (xml.SelectSingleNode(xp) == null)
                    {
                        XmlNode newNode = xml.CreateElement(item);
                        if (i == arr.Length - 1)
                        {
                            newNode.InnerText = innerText;
                        }
                        parentNode.AppendChild(newNode);
                    }
                }

                i++;
                parentNode = xml.SelectSingleNode(xp);
            }

            return parentNode;
        }

        private void btnOracleTest_Click(object sender, EventArgs e)
        {
            _dbInfo.OracleDB.IP = this.txtBoxIP.Text.Trim();
            _dbInfo.OracleDB.Port = this.txtBoxPort.Text.Trim();
            _dbInfo.OracleDB.ServiceName = this.txtBoxServiceName.Text.Trim();
            _dbInfo.OracleDB.UserID = this.txtBoxUserID.Text.Trim();
            _dbInfo.OracleDB.Password = this.txtBoxPassword.Text.Trim();

            string oracleresult = DAS.Instance.OracleSql(_dbInfo.OracleDB.IP, _dbInfo.OracleDB.Port, _dbInfo.OracleDB.ServiceName, _dbInfo.OracleDB.UserID, _dbInfo.OracleDB.Password, "SELECT SYSDATE FROM DUAL");
            MessageBox.Show(oracleresult);
        }
        
        private void btnGaussTest_Click(object sender, EventArgs e)
        {
            string gaussresult = DAS.Instance.GaussSql(_dbInfo.GaussDB.IP, _dbInfo.GaussDB.Port, _dbInfo.GaussDB.UserID, _dbInfo.GaussDB.Password, "SELECT SYSDATE FROM SYS_DUMMY");
            MessageBox.Show(gaussresult);
        }
    }

    public class DBInfo
    {
        public OracleDB OracleDB { get; set; } = new OracleDB();
        public GaussDB GaussDB { get; set; } = new GaussDB();
    }

    public class OracleDB
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string ServiceName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public class GaussDB
    {
        public string IP { get; set; }
        public string Port { get; set; }      
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
