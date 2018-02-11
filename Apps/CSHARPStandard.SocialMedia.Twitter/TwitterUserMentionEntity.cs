using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data related to a mention of a user in a tweet
    /// </summary>
    public class TwitterUserMentionEntity : ITwitterEntity
    {
        public string Id { get; set; }
        public string ScreenName { get; set; }
        public string Name { get; set; }
    }
}
