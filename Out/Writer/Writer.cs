using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out
{
    public class Writer
    {
        public static void WriteData(TextWriter destination, string data)
        {
            destination.Write(data);
        }
    }
}
