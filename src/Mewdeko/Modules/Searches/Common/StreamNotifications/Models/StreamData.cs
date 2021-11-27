﻿using Mewdeko.Services.Database.Models;

namespace Mewdeko.Modules.Searches.Common.StreamNotifications.Models
{
    public class StreamData
    {
        public FollowedStream.FType StreamType { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public int Viewers { get; set; }
        public string Title { get; set; }
        public string Game { get; set; }
        public string Preview { get; set; }
        public bool IsLive { get; set; }
        public string StreamUrl { get; set; }
        public string AvatarUrl { get; set; }

        public StreamDataKey CreateKey()
        {
            return new StreamDataKey(StreamType, UniqueName.ToLower());
        }
    }
}