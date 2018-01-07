using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace CSHARP.SocialMedia.Twitter
{
    /// <summary>
    /// Uses Twitter Open Auth to read from Twitter.
    /// </summary>
    /// <remarks>Uses Tweetinvi API: https://github.com/linvi/tweetinvi
    /// </remarks>
    public class TwiterWriter
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

        /// <summary>
        /// Publishes To Twitter using credentials assigned to the class
        /// </summary>
        /// <param name="message">Message to tweet</param>
        /// <returns></returns>
        public ITweet PublishToTwitter(string message)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

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
            var tweet = Tweet.PublishTweetWithImage("@RPeplau This was the beard last year (Tweetsharp) I trimmed it to use @TweetinviApi which is much better", file1);
            if (tweet.IsTweetPublished == false) throw new Exception("ERROR Publishing tweet with image");
            return tweet;
        }
    }
}
