using System;
using System.Windows;
using ClipTrigger.Services;
using ClipTrigger.ViewModels;

namespace ClipTrigger.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ClipboardMonitor clipboardMonitor;

    public MainWindow()
    {
        InitializeComponent();
        clipboardMonitor = new ClipboardMonitor();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        var viewModel = (MainWindowViewModel)DataContext;
        clipboardMonitor.ClipboardTextChanged += viewModel.OnClipboardChanged;
        clipboardMonitor.Start(this);
    }

    protected override void OnClosed(EventArgs e)
    {
        clipboardMonitor.Dispose();
        base.OnClosed(e);
    }
}