using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;

namespace Out
{
    public interface ISerializer
    {
        string Serialize(TraceResult result);
    }
}
