using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Data object to store information about a user of Twitter
    /// </summary>
    public interface ITwitterUser
    {
        #region Basic Twitter User Information

        /// <summary>
        /// Unique id of user on Twitter
        /// </summary>
        string TwitterUserId { get; set; }

        /// <summary>
        /// Date Twitter User Joined 
        /// </summary>
        string TwiterUserJoined { get; set; }

        /// <summary>
        /// Screen Name of the Twitter user
        /// </summary>
        string TwitterHandle { get; set; }

        /// <summary>
        /// Twitter Name
        /// </summary>
        string TwitterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; set; }

        string Email { get; set; }

        string LanguageCode { get; set; }



        #endregion

        #region Geo Related 

        /// <summary>
        /// Location of twitter user if applicable
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// If true, user has Geo Enabled tweets turned on
        /// </summary>
        bool GeoEnabled { get; set; }

        #endregion

        #region Statistics Related

        string StatusesCount { get; set; }

        string FollowersCount { get; set; }
        string FriendsCount { get; set; }
        string FavouritesCount { get; set; }
        string ListedCount { get; set; }

        #endregion

        /// <summary>
        /// List of Tweets published by this twitter user
        /// </summary>
        List<string> TweetIds { get; }

        /// <summary>
        /// List of Tweets liked by this twitter user
        /// </summary>
        List<string> LikedTweets { get; }
    }
}
