using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data related to a url entity in twitter
    /// </summary>
    public class TwitterUrlEntity : ITwitterEntity
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public string ExpandedUrl { get; set; }
    }
}
