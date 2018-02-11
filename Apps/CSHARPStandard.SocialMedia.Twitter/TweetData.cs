using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Contains basic data about a Tweet.
    /// </summary>
    public class TweetData : ITweetData
    {
        public TweetData()
        {
        }

        public TweetData(ITweetData tweetData)
        {
            Coordinates = tweetData.Coordinates;
            CurrentUserRetweet = tweetData.CurrentUserRetweet;
            InReplyToScreenName = tweetData.InReplyToScreenName;
            InReplyToStatusId = tweetData.InReplyToStatusId;
            InReplyToUserId = tweetData.InReplyToUserId;
            IsTruncated = tweetData.IsTruncated;
            Language = tweetData.Language;
            Owner = tweetData.Owner;
            Place = tweetData.Place;
            QuotedStatus = tweetData.QuotedStatus;
            QuotedStatusId = tweetData.QuotedStatusId;
            RetweetedStatus = tweetData.RetweetedStatus;
            Source = tweetData.Source;
            TweetCreatedDate = tweetData.TweetCreatedDate;
            TweetFavoriteCount = tweetData.TweetFavoriteCount;
            TweetFullText = tweetData.TweetFullText;
            TweetId = tweetData.TweetId;
            TweetRetweetCount = tweetData.TweetRetweetCount;
            TweetText = tweetData.TweetText;

            TwitterEntities.AddRange(tweetData.TwitterEntities);
        }

        #region Basic Tweet Information

        /// <summary>
        /// Unique id representing tweet on twitter
        /// </summary>
        public string TweetId { get; set; }

        /// <summary>
        /// Date and time the tweet occurred
        /// </summary>
        public string TweetCreatedDate { get; set; }

        /// <summary>
        /// If true then text was longer than the tweet limit so TweetText is truncated but TweetFullText will contain the full text.
        /// </summary>
        public bool IsTruncated { get; set; }

        /// <summary>
        /// The text of the tweet (may be truncated)
        /// </summary>
        public string TweetText { get; set; }

        /// <summary>
        /// If truncated then this will contain the full text
        /// </summary>
        public string TweetFullText { get; set; }

        /// <summary>
        /// This is the twitter user whom did the actual tweeting
        /// </summary>
        /// <remarks>May be null as it will be assigned externally from the class. This allows for extended twitter users to be assigned to the tweet.</remarks>
        public ITwitterUser Owner { get; set; }

        /// <summary>
        /// Language the tweet is in
        /// </summary>
        public string Language { get; set; }

        #endregion

        #region Geolocation Related

        /// <summary>
        /// Co-ordinates of where the tweet occurred
        /// </summary>
        public string Coordinates { get; set; }

        /// <summary>
        /// A known location?
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// What device a user tweeted from
        /// </summary>
        public string Source { get; set; }

        #endregion

        #region Retweet or Quote Related

        /// <summary>
        /// If this is a retweet of another tweet then this is the additional text added to the tweet.
        /// </summary>
        public ITweetData RetweetedStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentUserRetweet { get; set; }

        /// <summary>
        /// If this tweet is a reply then this would be to tweet that it was in reply to
        /// </summary>
        public string InReplyToStatusId { get; set; }

        /// <summary>
        /// If this is a reply then this is the twitter user id that was being replied to
        /// </summary>
        public string InReplyToUserId { get; set; }

        /// <summary>
        /// If this is a reply then this is the twitter screen name that is being replied to
        /// </summary>
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// If this is a quote of another tweet then this is the id of the original tweet.
        /// </summary>
        public string QuotedStatusId { get; set; }

        /// <summary>
        /// If this is a quote of another tweet then this is the additional text added to the tweet.
        /// </summary>
        public ITweetData QuotedStatus { get; set; }

        #endregion 

        #region Tweet Statistics

        /// <summary>
        /// Number of time someone liked the tweet
        /// </summary>
        public int TweetFavoriteCount { get; set; }

        /// <summary>
        /// Number of times someone retweeted the tweet
        /// </summary>
        public int TweetRetweetCount { get; set; }

        #endregion

        /// <summary>
        /// Entities related to this tweet
        /// </summary>
        public List<ITwitterEntity> TwitterEntities { get { return _twitterEntities; } }
        private List<ITwitterEntity> _twitterEntities = new List<ITwitterEntity>();
    }
}
