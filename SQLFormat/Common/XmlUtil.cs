using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SQLFormat.Common
{
    public class XmlUtil
    {
        public static void ModifyAttribute(string srcFile, Action<XmlDocument> callback)
        {
            XmlDocument srcDoc = new XmlDocument();

            Stream configStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SQLFormat." + srcFile);

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + srcFile;
                if (configStream == null)
                    srcDoc.Load(path);
                else
                    srcDoc.Load(configStream);

                callback(srcDoc);

                if (configStream == null)
                    srcDoc.Save(path);
                else
                    srcDoc.Save(configStream);
            }
            catch (UnauthorizedAccessException uaae)
            {

            }
            catch (Exception e)
            {

            }
            finally
            {
                if (configStream != null)
                    configStream.Close();
            }
        }
    }
}
