using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LFS_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = new DirectoryModel(@"D:\git\GitLFSTest1", new List<string>() {".git"});
            directory.Show();

            Console.WriteLine("Press 'q' to quit the sample.");
            while (Console.Read() != 'q')
            {
            }
        }
    }
}
