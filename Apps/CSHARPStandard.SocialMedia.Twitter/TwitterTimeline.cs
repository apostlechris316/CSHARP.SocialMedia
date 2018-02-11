using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data Object containing a Twitter Timeline
    /// </summary>
    public class TwitterTimeline : ITwitterTimeline
    {
        /// <summary>
        /// Dictionary filled with tweets and their tweet id
        /// </summary>
        public Dictionary<string, ITweetData> Tweets { get { return _tweets; } }
        private Dictionary<string, ITweetData> _tweets = new Dictionary<string, ITweetData>();

        /// <summary>
        /// List of Twitter Users
        /// </summary>
        public Dictionary<string, ITwitterUser> TwitterUsers { get { return _twitterUsers; } }
        private Dictionary<string, ITwitterUser> _twitterUsers = new Dictionary<string, ITwitterUser>();
    }
}
