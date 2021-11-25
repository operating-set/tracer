using System.IO;
using System.Xml.Serialization;
using Tracer;

namespace Out
{
    public class XmlResultSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TraceResult));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, result);
                return textWriter.ToString();
            }
        }
    }
}
