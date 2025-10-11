using System.Collections.ObjectModel;
using System.IO;
using Prism.Mvvm;

namespace ClipTrigger.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string title = "Prism Application";

    public MainWindowViewModel()
    {
        InjectDummies();
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<DirectoryInfo> SourceDirectories { get; set; } = new ();

    private void InjectDummies()
    {
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test"));
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test1"));
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test2"));
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test3"));
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test4"));
        SourceDirectories.Add(new DirectoryInfo(@"C:\Users\testUser\Desktop\test5"));
    }
}