using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_Test
{
    class DirectoryWatcher
    {
        private bool Debug = false;
        private string Path { get; }

        private FileSystemWatcher FsWatcher { get; }

        public DirectoryWatcher(string pathDirectory)
        {
            Path = pathDirectory;

            FsWatcher = new FileSystemWatcher(Path)
            {
                NotifyFilter = NotifyFilters.Attributes |
                               NotifyFilters.CreationTime |
                               NotifyFilters.FileName |
                               NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.Size |
                               NotifyFilters.Security
            };

            FsWatcher.Changed += HandleChanged;
            FsWatcher.Deleted += HandleDeleted;
            FsWatcher.Renamed += HandleRenamed;
            FsWatcher.Created += HandleCreated;

            FsWatcher.EnableRaisingEvents = true;
        }

        public event EventHandler<ChangedEventArgs> OnChanged;
        public event EventHandler<DeletedEventArgs> OnDeleted;
        public event EventHandler<RenamedEventArgs> OnRenamed;
        public event EventHandler<CreatedEventArgs> OnCreated;

        private bool IsDir(string path)
        {
            return Directory.Exists(path);
        }

        private void HandleCreated(object sender, FileSystemEventArgs e)
        {
            if (Debug)
            {
                Console.WriteLine($"Created {e.Name}");
            }

            var handler = OnCreated;
            handler?.Invoke(this, new CreatedEventArgs() { Name = e.Name, Path = e.FullPath, IsDir = IsDir(e.FullPath) });
        }

        private void HandleRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            if (Debug)
            {
                Console.WriteLine($"Renamed {e.Name}");
            }

            var handler = OnRenamed;
            handler?.Invoke(this,
                new RenamedEventArgs()
                {
                    Name = e.OldName,
                    Path = e.OldFullPath,
                    NewName = e.Name,
                    NewPath = e.FullPath,
                    IsDir = IsDir(e.FullPath)
                });
        }

        private void HandleChanged(object sender, FileSystemEventArgs e)
        {
            if (Debug)
            {
                Console.WriteLine($"Changed {e.Name}");
            }

            var handler = OnChanged;
            handler?.Invoke(this, new ChangedEventArgs() { Name = e.Name, Path = e.FullPath, IsDir = IsDir(e.FullPath) });
        }

        private void HandleDeleted(object sender, FileSystemEventArgs e)
        {
            if (Debug)
            {
                Console.WriteLine($"Deleted {e.Name}");
            }

            var handler = OnDeleted;
            handler?.Invoke(this, new DeletedEventArgs() { Name = e.Name, Path = e.FullPath, IsDir = IsDir(e.FullPath) });
        }
    }
}
