using Caliburn.Micro;
using MeetingSdk.NetAgent.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace MeetingSdk.Wpf
{
    public class VideoBoxManager : IVideoBoxManager, IScreen
    {
        private readonly IEventAggregator _eventAggregator;
        public VideoBoxManager()
        {
            _eventAggregator = IoC.Get<IEventAggregator>();
            Properties = new Dictionary<string, object>();
        }

        private readonly Dictionary<string, VideoBox> _items =
            new Dictionary<string, VideoBox>();
        IList<VideoBox> IVideoBoxManager.Items => _items.Values.ToList();

        public Dictionary<string, object> Properties { get; private set; }

        public void SetProperty(string key, object value)
        {
            if (Properties.Keys.Contains(key))
            {
                Properties[key] = value;
            }
            else
            {
                Properties.Add(key, value);
            }

        }

        public void SetVideoBox(VideoBox videoBox)
        {
            if (videoBox == null)
                throw new ArgumentNullException(nameof(videoBox));

            if (!_items.ContainsKey(videoBox.Name))
            {
                _items.Add(videoBox.Name, videoBox);
            }
            else
            {
                _items[videoBox.Name] = videoBox;
            }
        }

        public bool TryGet(AccountModel accountModel, VideoBoxType videoBoxType, MediaType mediaType, out VideoBox videoBox)
        {
            videoBox = null;
            try
            {
                Monitor.Enter(_items);
                foreach (var item in _items.Values.Where(m => m.VideoBoxType == videoBoxType))
                {
                    if (videoBox == null && item.AccountResource == null)
                    {
                        videoBox = item;
                    }

                    if (item.AccountResource?.AccountModel.AccountId == accountModel.AccountId)
                    {
                        videoBox = item;
                    }
                }
                if (videoBox != null)
                {
                    videoBox.AccountResource = new AccountResource(accountModel, 0, mediaType);
                    videoBox.Sequence = _items.Values.DefaultIfEmpty().Max(v => v.Sequence) + 1;
                }
            }
            finally
            {
                Monitor.Exit(_items);
            }
            return videoBox != null;
        }

        public void Release(int accountId)
        {
            try
            {
                Monitor.Enter(_items);
                foreach (var item in _items.Values.Where(
                    m => m.AccountResource?.AccountModel.AccountId == accountId))
                {
                    _eventAggregator.GetEvent<VideoBoxRemovedEvent>().Publish(item);
                    item.AccountResource = null;
                }
            }
            finally
            {
                Monitor.Exit(_items);
            }
        }

        public void Release(int accountId, VideoBoxType videoBoxType)
        {
            try
            {
                Monitor.Enter(_items);
                foreach (var item in _items.Values.Where(
                    m => m.AccountResource?.AccountModel.AccountId == accountId &&
                    m.VideoBoxType == videoBoxType))
                {
                    _eventAggregator.GetEvent<VideoBoxRemovedEvent>().Publish(item);
                    item.AccountResource = null;
                }
            }
            finally
            {
                Monitor.Exit(_items);
            }
        }

        public IList<IVideoBox> Items => _items.Values.ToList<IVideoBox>();

        public Size Size { get; set; }
    }
}
