using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ProtoBuf;
using ProtoBuf.Serializers;

namespace com.VotoVisible.Utils
{
    /// <summary>
    /// Descripción breve de Serializer
    /// </summary>
    public class Serializer
    {
        public Serializer()
        {

        }

        public static string SerializeObj2XML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();

            serializer.Serialize(writer, obj);
            return writer.ToString();
        }

        public static Object DeserializeXML2Obj(string xml, Type objType)
        {
            StringReader strReader = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(objType);
            XmlTextReader xmlReader = new XmlTextReader(strReader);
            try
            {
                Object obj = serializer.Deserialize(xmlReader);
                return obj;
            }
            finally
            {
                xmlReader.Close();
                strReader.Close();
            }
        }
    }
}