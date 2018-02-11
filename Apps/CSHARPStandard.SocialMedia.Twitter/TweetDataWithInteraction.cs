using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data Object storing Tweet Data with additional meta regarding whether it was retweeted or favorited.
    /// </summary>
    /// <remarks>The interaction meta help with processing as it allows us to skip tweets we have already retweeted or favorited</remarks>
    public class TweetDataWithInteraction : TweetData, ITweetInteraction
    {
        /// <summary>
        /// Copies an existing Tweet Data into One with Interactions.
        /// </summary>
        /// <param name="newTweet"></param>
        /// <remarks>Only copies ITweetData fields</remarks>
        public TweetDataWithInteraction(ITweetData newTweet) : base(newTweet)
        {
        }

        /// <summary>
        /// If retweeted by the owner of the directory this will be that retweet
        /// </summary>
        public long OwnerRetweetedId { get; set; }

        /// <summary>
        /// True if owner of the directory already favorited this tweet
        /// </summary>
        public bool OwnerFavorited { get; set; }
    }
}
