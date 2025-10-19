using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows;
using ClipTrigger.Models;
using ClipTrigger.Utils;
using Prism.Commands;
using Prism.Mvvm;

namespace ClipTrigger.ViewModels;

// ReSharper disable once ClassNeverInstantiated.Global
public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new ();
    private string inputText;

    public MainWindowViewModel()
    {
        InjectDummies();
    }

    public string Title => appVersionInfo.Title;

    public ObservableCollection<SearchFolderItem> SourceDirectories { get; set; } = new ();

    public DelegateCommand<SearchFolderItem> ToggleIncludeInSearchCommand => new (item =>
    {
        if (item == null)
        {
            return;
        }

        item.IncludeInSearch = !item.IncludeInSearch;
    });

    public DelegateCommand<SearchFolderItem> RemoveSourceDirectoryCommand => new (item =>
    {
        if (item == null)
        {
            return;
        }

        SourceDirectories.Remove(item);
    });

    public DelegateCommand AddSourceDirectoryCommand => new (() =>
    {
        if (string.IsNullOrWhiteSpace(InputText))
        {
            return;
        }

        SourceDirectories.Add(new SearchFolderItem(InputText));
        InputText = string.Empty;
    });

    public string InputText { get => inputText; set => SetProperty(ref inputText, value); }

    public void OnClipboardChanged(string text)
    {
        if (!IsFileName(text))
        {
            return;
        }

        var path = FindSoundFile(text);
        if (!string.IsNullOrEmpty(path))
        {
            PlaySound(path);
        }
    }

    private bool IsFileName(string text)
    {
        // 拡張子がある or 英数字のみの単語と仮定（必要に応じて調整）
        return !string.IsNullOrWhiteSpace(text) && (text.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);
    }

    private string FindSoundFile(string fileName)
    {
        // 拡張子付き → そのまま探す
        if (Path.HasExtension(fileName))
        {
            foreach (var dir in SourceDirectories)
            {
                var fullPath = Path.Combine(dir.FullPath, fileName);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
        }
        else
        {
            var supportedExtensions = new[] { ".wav", ".mp3", ".ogg", };

            // 拡張子なし → .wav/.mp3をつけて探す
            foreach (var dir in SourceDirectories)
            {
                foreach (var ext in supportedExtensions)
                {
                    var fullPath = Path.Combine(dir.FullPath, fileName + ext);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
            }
        }

        return null;
    }

    private void PlaySound(string filePath)
    {
        try
        {
            if (filePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                var player = new SoundPlayer(filePath);
                player.Play();
            }
            else
            {
                // NAudio などを使えば mp3 も対応できる
                MessageBox.Show("MP3 再生は別ライブラリが必要です");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("再生失敗: " + ex.Message);
        }
    }

    [Conditional("DEBUG")]
    private void InjectDummies()
    {
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test"));
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test1"));
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test2"));
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test3"));
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test4"));
        SourceDirectories.Add(new (@"C:\Users\testUser\Desktop\test5"));
    }
}