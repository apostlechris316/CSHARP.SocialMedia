using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Interface implemented by all tweets. Some tweets may contain additional interfaces for extended information regarding the tweet.
    /// </summary>
    public interface ITweetData
    {
        #region Basic Tweet Information

        /// <summary>
        /// Unique id representing tweet on twitter
        /// </summary>
        string TweetId { get; set; }

        /// <summary>
        /// Date and time the tweet occurred
        /// </summary>
        string TweetCreatedDate { get; set; }

        /// <summary>
        /// If true then text was longer than the tweet limit so TweetText is truncated but TweetFullText will contain the full text.
        /// </summary>
        bool IsTruncated { get; set; }

        /// <summary>
        /// The text of the tweet (may be truncated)
        /// </summary>
        string TweetText { get; set; }

        /// <summary>
        /// If truncated then this will contain the full text
        /// </summary>
        string TweetFullText { get; set; }

        /// <summary>
        /// This is the twitter user whom did the actual tweeting
        /// </summary>
        /// <remarks>May be null as it will be assigned externally from the class. This allows for extended twitter users to be assigned to the tweet.</remarks>
        ITwitterUser Owner { get; set; }

        /// <summary>
        /// Language the tweet is in
        /// </summary>
        string Language { get; set; }

        #endregion

        #region Geolocation Related

        /// <summary>
        /// Co-ordinates of where the tweet occurred
        /// </summary>
        string Coordinates { get; set; }

        /// <summary>
        /// A known location?
        /// </summary>
        string Place { get; set; }

        /// <summary>
        /// What device a user tweeted from
        /// </summary>
        string Source { get; set; }

        #endregion

        #region Retweet or Quote Related

        /// <summary>
        /// IF retweeted, this would have the tweet info for the tweet.
        /// </summary>
        ITweetData RetweetedStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CurrentUserRetweet { get; set; }

        /// <summary>
        /// If this tweet is a reply then this would be to tweet that it was in reply to
        /// </summary>
        string InReplyToStatusId { get; set; }

        /// <summary>
        /// If this is a reply then this is the twitter user id that was being replied to
        /// </summary>
        string InReplyToUserId { get; set; }

        /// <summary>
        /// If this is a reply then this is the twitter screen name that is being replied to
        /// </summary>
        string InReplyToScreenName { get; set; }

        /// <summary>
        /// If this is a quote of another tweet then this is the id of the original tweet.
        /// </summary>
        string QuotedStatusId { get; set; }

        /// <summary>
        /// If this is a quote of another tweet then this is the additional text added to the tweet.
        /// </summary>
        ITweetData QuotedStatus { get; set; }

        #endregion 

        #region Tweet Statistics

        /// <summary>
        /// Number of time someone liked the tweet
        /// </summary>
        int TweetFavoriteCount { get; set; }

        /// <summary>
        /// Number of times someone retweeted the tweet
        /// </summary>
        int TweetRetweetCount { get; set; }

        #endregion

        /// <summary>
        /// Entities related to this tweet
        /// </summary>
        List<ITwitterEntity> TwitterEntities { get; }

    }
}
