using System.IO;

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
