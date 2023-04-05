using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
namespace EmployeeDetails_CRUD_Operation
{
    class Common : IDisposable
    {
        public string GetFilePath(string sFilePath)
        {
            string folderPath = "";
            try
            {
                XmlDocument oXML = new XmlDocument();
                XmlNode oNode;
                oXML.Load("..\\..\\Setting.xml");
                oNode = oXML.SelectSingleNode("//ApiBasicCredential/" + sFilePath);
                if (oNode != null)
                    folderPath = oNode.InnerXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return folderPath;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
