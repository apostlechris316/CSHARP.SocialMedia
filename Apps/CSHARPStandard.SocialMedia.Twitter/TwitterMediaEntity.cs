using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data related to a media entity in twitter eg. picture
    /// </summary>
    public class TwitterMediaEntity : ITwitterEntity
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public string ExpandedUrl { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlHttps { get; set; }
        public string MediaType { get; set; }
        public string VideoInfo { get; set; }
    }
}
