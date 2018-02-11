using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Interface implemented if TweetData is stored with interaction meta to help in tracking if we retweeted or favorited tweet already
    /// </summary>
    interface ITweetInteraction
    { 
        /// <summary>
        /// If retweeted by the owner of the directory this will be the twitter id of that retweet
        /// </summary>
        long OwnerRetweetedId { get; set; }

        /// <summary>
        /// True if owner of the directory already favorited this tweet
        /// </summary>
        bool OwnerFavorited { get; set; }
    }
}
