using System;
using System.Collections.Generic;
using System.IO;
using Tweetinvi;
using Tweetinvi.Json;
using Tweetinvi.Models;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Uses Twitter Open Auth to read from Twitter.
    /// </summary>
    /// <remarks>Uses Tweetinvi API: https://github.com/linvi/tweetinvi
    /// </remarks>
    public class TwitterWriter
    {
        #region Twitter API Credentials

        /// <summary>
        /// Open Authentication token (OAuth)
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Open Authentication token secret (OAuth)
        /// </summary>
       public string OAuthTokenSecret { get; set; }

        /// <summary>
        /// Twitter API Consumer Key
        /// </summary>
      public string ConsumerKey { get; set; }

        /// <summary>
        /// Twitter Consumer Shared Secret Necessary for some API calls
        /// </summary>
       public string ConsumerSharedSecret { get; set; }

        #endregion

        private IAuthenticatedUser AuthenticateWithTwitter()
        { 
            Auth.SetUserCredentials(ConsumerKey, ConsumerSharedSecret, OAuthToken, OAuthTokenSecret);

            TweetinviEvents.QueryBeforeExecute += (sender, args) => { Console.WriteLine(args.QueryURL); };

            return User.GetAuthenticatedUser();
        }

        #region Direct Messages 

        /// <summary>
        /// Publishes a direct message
        /// </summary>
        /// <param name="twitterHandle"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string PublishMessage(string twitterHandle, string message)
        {
            // Authenticate with Twitter first
            var authenticatedUser = AuthenticateWithTwitter();
            if (authenticatedUser == null) throw new Exception("EROR Authenticating for Twitter");

            return MessageJson.PublishMessage(message, twitterHandle);
        }

        #endregion

        #region Publishing Tweets

        /// <summary>
        /// Retweets a tweet on twitter
        /// </summary>
        /// <param name="tweet"></param>
        public ITweet PublishRetweetToTwitter(long tweetId)
        {
            // Authenticate with Twitter first
            var authenticatedUser = AuthenticateWithTwitter();
            if (authenticatedUser == null) throw new Exception("EROR Authenticating for Twitter");

            // Tweet the messages with no image
            var reTweet = Tweet.PublishRetweet(tweetId);
            if (reTweet.IsTweetPublished == false) throw new Exception("ERROR Publishing retweet failed.");

            return reTweet;
        }

        /// <summary>
        /// Retweets a tweet on twitter
        /// </summary>
        /// <param name="tweet"></param>
        public ITweet PublishRetweetToTwitter(ITweet tweet)
        {
            // Authenticate with Twitter first
            var authenticatedUser = AuthenticateWithTwitter();
            if (authenticatedUser == null) throw new Exception("EROR Authenticating for Twitter");

            // Tweet the messages with no image
            var reTweet = Tweet.PublishRetweet(tweet);
            if (reTweet.IsTweetPublished == false) throw new Exception("ERROR Publishing retweet failed.");

            return reTweet;
        }

        /// <summary>
        /// Publishes To Twitter using credentials assigned to the class
        /// </summary>
        /// <param name="message">Message to tweet</param>
        /// <returns></returns>
        public ITweet PublishToTwitter(string message)
        {
            // Authenticate with Twitter first
            var authenticatedUser = AuthenticateWithTwitter();
            if (authenticatedUser == null) throw new Exception("EROR Authenticating for Twitter");

            // Tweet the messages with no image
            var tweet = Tweet.PublishTweet(message);
            if (tweet.IsTweetPublished == false) throw new Exception("ERROR Publishing text only tweet");

            return tweet;
        }

        /// <summary>
        /// Publishes To Text and Image to Twitter using credentials assigned to the class
        /// </summary>
        /// <param name="message">Message to tweet</param>
        /// <param name="imageFullPath">Full path to image on local file system</param>
        /// <returns></returns>
        public ITweet PublishToTwitterWithLocalImage(string message, string imageFullPath)
        {
            byte[] file1 = File.ReadAllBytes(imageFullPath);
            return PublishToTwitterWithLocalImage(message, file1);
        }

        /// <summary>
        /// Publishes To Text and Image to Twitter using credentials assigned to the class
        /// </summary>
        /// <param name="message">Message to tweet</param>
        /// <param name="imageFile1">Bytes representing the image to publish</param>
        /// <returns></returns>
        public ITweet PublishToTwitterWithLocalImage(string message, byte [] imageFile1)
        {
            var tweet = Tweet.PublishTweetWithImage(message, imageFile1);
            if (tweet.IsTweetPublished == false) throw new Exception("ERROR Publishing tweet with image");
            return tweet;
        }

        /// <summary>
        /// Publish a tweet in reply to another tweet
        /// </summary>
        /// <param name="message">tweet message to add</param>
        /// <param name="tweetId">Id of tweet to reply to</param>
        /// <returns></returns>
        public ITweet PublishTweetInReplyTo(string message, long tweetId)
        {
            var tweet = Tweet.PublishTweetInReplyTo(message,tweetId);
            if (tweet == null) return null;

            if (tweet.IsTweetPublished == false) throw new Exception("ERROR Publishing reply to tweet");
            return tweet;
            
        }

        #endregion
    }
}
