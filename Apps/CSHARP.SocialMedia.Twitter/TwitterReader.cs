using Twitterizer;
using Twitterizer.Streaming;

namespace CSHARP.SocialMedia.Twitter
{
    /// <summary>
    /// General TwitterStream class built on Twitterizer.
    /// NOTE: You must override callbacks in order to process messages.
    /// </summary>
    public class TwitterReader
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

        protected TwitterStream stream = null;

        /// <summary>
        /// Starts collecting the twitter data
        /// </summary>
        /// <param name="track">Tilde delimeted list of items to track</param>
        /// <returns></returns>
        public void StartCollecting(string userAgent, string track, bool includeRawJsonCallback)
        {
            #region Prepare Auth Tokens for Twitter

            OAuthTokens tokens = new OAuthTokens()
            {
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSharedSecret,
                AccessToken = OAuthToken,
                AccessTokenSecret = OAuthTokenSecret
            };
            
            #endregion

            #region Set up items we want to track

            StreamOptions options = new StreamOptions();

            string[] trackItems = track.Split('~');
            foreach (string trackItem in trackItems)
            {
                options.Track.Add(trackItem);
            }

            #endregion

            stream = new TwitterStream(tokens, userAgent, options);

            if (includeRawJsonCallback)
            {
                stream.StartUserStream(StreamInit, StreamStopped, NewTweet, DeletedTweet, NewDirectMessage, DeletedDirectMessage, OtherEvent, RawJson);
            }
            else
            {
                stream.StartUserStream(StreamInit, StreamStopped, NewTweet, DeletedTweet, NewDirectMessage, DeletedDirectMessage, OtherEvent, null);
            }
        }

        /// <summary>
        /// Ends the collection of twitter data
        /// </summary>
        /// <param name="reason"></param>
        public void StopCollecting(string reason)
        {
            stream.EndStream(StopReasons.StoppedByRequest, reason);
        }

        #region Callbacks 

        /// <summary>
        /// When stream starts pushing content
        /// </summary>
        /// <param name="friends"></param>
        /// <remarks>Example override: string message = string.Format("{0} friends reported.", friends.Count));</remarks>
        public virtual void StreamInit(TwitterIdCollection friends)
        {
            string message = string.Format("{0} friends reported.", friends.Count);
        }

        /// <summary>
        /// If the stream stopped pushing content
        /// </summary>
        /// <param name="reason"></param>
        /// <remarks>Example override: string message = string.Format("The stream has stopped. Reason: {0}", reason.ToString();</remarks>
        public virtual void StreamStopped(StopReasons reason)
        {
            string message = string.Format("The stream has stopped. Reason: {0}", reason.ToString());
        }

        /// <summary>
        /// If a user added a tweet
        /// </summary>
        /// <param name="tweet"></param>
        /// <remarks>Example override: string message = string.Format("New tweet: @{0}: {1}", tweet.User.ScreenName, tweet.Text);</remarks>
        public virtual void NewTweet(TwitterStatus tweet)
        {
            string message = string.Format("New tweet: @{0}: {1}", tweet.User.ScreenName, tweet.Text);
        }

        /// <summary>
        /// If a user deleted a tweet
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>Example override: string message = string.Format("Deleted tweet: Id: {0}; UserId: {1}", e.Id, e.UserId);</remarks>
        public virtual void DeletedTweet(TwitterStreamDeletedEvent e)
        {
            string message = string.Format("Deleted tweet: Id: {0}; UserId: {1}", e.Id, e.UserId);
        }

        /// <summary>
        /// If your twitter account received a direct message
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>Example override: string result = string.Format("New message from {0}", message.SenderScreenName);</remarks>
        public virtual void NewDirectMessage(TwitterDirectMessage message)
        {
            string result = string.Format("New message from {0}", message.SenderScreenName);
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>Example override: string message = string.Format("Deleted message: {0}", e.UserId);</remarks>
        public virtual void DeletedDirectMessage(TwitterStreamDeletedEvent e)
        {
            string message = string.Format("Deleted message: {0}", e.UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>Example override: string message = string.Format("Other event. Type: {0}; From: {1}; {2}", e.EventType, e.Source.ScreenName, e.TargetObject);</remarks>
        public virtual void OtherEvent(TwitterStreamEvent e)
        {
            string message = string.Format("Other event. Type: {0}; From: {1}; {2}", e.EventType, e.Source.ScreenName, e.TargetObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <remarks>Example override: string message = json;</remarks>
        public virtual void RawJson(string json)
        {
            string message = json;
        }
        
        #endregion
    }
}
