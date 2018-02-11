using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data related to a hash tag
    /// </summary>
    public class TwitterHashTagEntity : ITwitterEntity
    {
        public string Id { get; set; }
        public string Text { get; set;}
    }
}
