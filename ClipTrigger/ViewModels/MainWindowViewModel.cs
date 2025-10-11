using Prism.Mvvm;

namespace ClipTrigger.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string _title = "Prism Application";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}