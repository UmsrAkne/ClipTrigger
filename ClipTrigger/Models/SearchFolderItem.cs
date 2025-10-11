using System.IO;
using Prism.Mvvm;

namespace ClipTrigger.Models
{
    public class SearchFolderItem : BindableBase
    {
        private bool includeInSearch;

        public SearchFolderItem(string path)
        {
            DirectoryInfo = new DirectoryInfo(path);
            FullPath = DirectoryInfo.FullName;
        }

        public string FullPath { get; private set; }

        public bool IncludeInSearch { get => includeInSearch; set => SetProperty(ref includeInSearch, value); }

        private DirectoryInfo DirectoryInfo { get; set; }
    }
}