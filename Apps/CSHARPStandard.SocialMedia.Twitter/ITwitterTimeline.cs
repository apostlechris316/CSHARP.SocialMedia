using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Interface implemented by all Twitter Timeline data objects
    /// </summary>
    public interface ITwitterTimeline
    {
        /// <summary>
        /// Dictionary filled with tweets and their tweet id
        /// </summary>
        Dictionary<string, ITweetData> Tweets { get; }

        /// <summary>
        /// List of Twitter Users
        /// </summary>
        Dictionary<string, ITwitterUser> TwitterUsers { get; }
    }
}
