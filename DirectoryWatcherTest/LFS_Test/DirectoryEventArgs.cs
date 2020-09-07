using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_Test
{
    public class DirectoryEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDir { get; set; }
    }

    public class ChangedEventArgs : DirectoryEventArgs
    {
    }

    public class RenamedEventArgs : DirectoryEventArgs
    {
        public string NewName { get; set; }
        public string NewPath { get; set; }
    }

    public class DeletedEventArgs : DirectoryEventArgs
    {
    }

    public class CreatedEventArgs : DirectoryEventArgs
    {
    }
}
