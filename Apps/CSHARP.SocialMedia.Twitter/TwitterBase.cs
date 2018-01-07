
namespace CSHARP.SocialMedia.Twitter
{
    /// <summary>
    /// Base Functionality Necessary for all Twitter Interactions
    /// </summary>
    public class TwitterBase
    {
        #region Rating Limit Related Settings

        /// <summary>
        /// If, true uses Microsoft Enterprise Caching for twitter queries
        /// </summary>
        public virtual bool EnableCache { get; set; }

        /// <summary>
        /// How long in miliseconds to wait between each call to stay within API Limit
        /// </summary>
        public virtual int PauseBetweenSteps { get; set; }

        /// <summary>
        /// Number of calls before pausing
        /// </summary>
        public virtual int NumberOfCallsPerStep { get; set; }

        #endregion

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
    }
}
