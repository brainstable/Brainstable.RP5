using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.RP5
{
    public class ReaderRP5Csv : ReaderRP5
    {
        protected override string[] CreateArrayByLine(string fileName)
        {
            return File.ReadAllLines(fileName, Encoding);
        }
    }
}
