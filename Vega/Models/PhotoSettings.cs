using System.IO;
using System.Linq;

namespace Vega.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            var path = Path.GetExtension(fileName.ToLower());
            var result = AcceptedFileTypes.Any(s => s == path);
            return result;
        }
    }
}
