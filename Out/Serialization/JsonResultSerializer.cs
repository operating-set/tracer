using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tracer;
using Newtonsoft.Json;

namespace Out.Serialization
{
    public class JsonResultSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;
        }
    }
}
