using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.Services
{
    public abstract class FileService
    {
        public static void WriteData(string path, string content) => File.WriteAllText(path, content);

    }
}
