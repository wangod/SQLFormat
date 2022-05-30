using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SQLFormat.Common
{
    public class Class2XmlUtil
    {
        public static void ToXml<T>(object obj, string path)
        {
            FileStream xmlfile = null;
            try
            {
                xmlfile = new FileStream(path, FileMode.OpenOrCreate);
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(xmlfile, obj);
                xmlfile.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
            finally
            {
                if (xmlfile != null)
                    xmlfile.Close();
            }
        }

        public static T ToClass<T>(string path) where T : class, new()
        {
            T t = new T();
            FileStream xmlfile = null;
            try
            {
                if (!File.Exists(path))
                {
                    ToXml<T>(t, path);
                }
                else
                {
                    xmlfile = new FileStream(path, FileMode.Open);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    t = xmlSerializer.Deserialize(xmlfile) as T;
                    xmlfile.Close();
                }
            }
            catch (Exception ex)
            {
                t = new T();
                //throw new Exception("将XML转换成实体对象异常",ex);
            }
            finally
            {
                if (xmlfile != null)
                    xmlfile.Close();

            }

            return t;
        }
    }
}
