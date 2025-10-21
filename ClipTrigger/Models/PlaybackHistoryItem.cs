using System;
using System.IO;
using Prism.Mvvm;

namespace ClipTrigger.Models
{
    /// <summary>
    /// 視聴履歴アイテム
    /// - 再生したファイルのフルパス/ファイル名
    /// - 最終再生日時
    /// - 再生回数
    /// </summary>
    public class PlaybackHistoryItem : BindableBase
    {
        private string fullPath;
        private string fileName;
        private DateTime lastPlayedAt;
        private int playCount;

        public PlaybackHistoryItem()
        {
        }

        /// <summary>
        /// ファイルのフルパスを入力し、それに基づいてメンバーを初期化します。
        /// </summary>
        /// <param name="path">この履歴が参照するファイルのフルパスを入力します</param>
        public PlaybackHistoryItem(string path)
        {
            var pathInfo = new FileInfo(path);
            FullPath = path;
            FileName = pathInfo.Name;
            LastPlayedAt = DateTime.Now;
            PlayCount = 1;
        }

        public string FullPath
        {
            get => fullPath;
            set => SetProperty(ref fullPath, value);
        }

        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }

        public DateTime LastPlayedAt
        {
            get => lastPlayedAt;
            set => SetProperty(ref lastPlayedAt, value);
        }

        public int PlayCount
        {
            get => playCount;
            set => SetProperty(ref playCount, value);
        }
    }
}