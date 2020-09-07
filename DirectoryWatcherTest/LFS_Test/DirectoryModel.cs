using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS_Test
{
    class DirectoryModel
    {
        private List<string> _directoryFilters;

        private List<DirectoryModel> _directories;
        private List<FileInfo> _files;

        private DirectoryWatcher DirWatcher;
        private string Path { get; set; }

        private string Name { get; set; }

        public DirectoryModel(string path, List<string> directoryFilters)
        {
            _directoryFilters = directoryFilters;
            Path = path;
            Name = new DirectoryInfo(Path).Name;

            DirWatcher = new DirectoryWatcher(Path);
            DirWatcher.OnChanged += HandleChanged;
            DirWatcher.OnDeleted += HandleDeleted;
            DirWatcher.OnRenamed += HandleRenamed;
            DirWatcher.OnCreated += HandleCreated;

            UpdateDirectories();
            UpdateFiles();
        }

        private bool CanFollowDirectory(string path)
        {
            return !_directoryFilters.Any(path.Contains);
        }

        public void Show(int depth = 0)
        {
            string currentIndent = String.Concat(Enumerable.Repeat("\t", depth));
            string nextIndent = String.Concat(Enumerable.Repeat("\t", depth + 1));
            Console.WriteLine($"{currentIndent}d:{Name}");
            foreach (var file in _files)
            {
                Console.WriteLine($"{nextIndent}f:{file.Name}");
            }

            foreach (var directory in _directories)
            {
                directory.Show(depth + 1);
            }
        }

        private void UpdateDirectories()
        {
            var directories = Directory.GetDirectories(Path);
            _directories = new List<DirectoryModel>();
            _directories.AddRange(directories.Where(CanFollowDirectory).Select(x => new DirectoryModel(x, _directoryFilters)));
        }

        private void UpdateFiles()
        {
            var files = Directory.GetFiles(Path);
            _files = new List<FileInfo>();
            _files.AddRange(files.Select(x => new FileInfo(x)));
        }

        private void HandleCreated(object sender, CreatedEventArgs e)
        {
            if (e.IsDir)
            {
                UpdateDirectories();
                Console.WriteLine($"New dir \"{e.Name}\" in dir \"{Path}\"");
            }
            else
            {
                Console.WriteLine($"File Created in dir \"{Path}\"");
            }
        }

        private void HandleRenamed(object sender, RenamedEventArgs e)
        {
            if (!e.IsDir)
            {
                Console.WriteLine($"File Renamed \"{e.Name}\" in \"{Path}\"");
            }
        }

        private void HandleDeleted(object sender, DeletedEventArgs e)
        {
            if (!e.IsDir)
            {
                Console.WriteLine($"File Deleted \"{e.Name}\" in \"{Path}\"");
            }
        }

        private void HandleChanged(object sender, ChangedEventArgs e)
        {
            if (!e.IsDir)
            {
                Console.WriteLine($"File Changed \"{e.Name}\" in \"{Path}\"");
            }
        }
    }
}
