using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Stores data regarding a user on Twitter
    /// </summary>
    public class TwitterUser : ITwitterUser
    {
        #region Basic Twitter Information 

        /// <summary>
        /// Unique id of user on Twitter
        /// </summary>
        public string TwitterUserId { get; set; }

        /// <summary>
        /// Date User Joined Twitter
        /// </summary>
        public string TwiterUserJoined { get; set; }

        /// Screen Name of the Twitter user
        /// </summary>
        public string TwitterHandle { get; set; }

        /// <summary>
        /// Twitter Name
        /// </summary>
        public string TwitterName { get; set; }

        /// <summary>
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Profile Settings Related

        /// <summary>
        /// Email address of user (if applicable)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  Url to twitter profile
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// If applicable the language of the user
        /// </summary>
        public string LanguageCode { get; set; }

        public bool Following { get; set; }
        public bool Protected { get; set; }
        public bool Verified { get; set; }
        public bool Notifications { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileImageUrlHttps { get; set; }
        public bool FollowRequestSent { get; set; }
        public bool DefaultProfile { get; set; }
        public bool DefaultProfileImage { get; set; }
        public string ProfileSidebarFillColor { get; set; }
        public string ProfileSidebarBorderColor { get; set; }
        public bool ProfileBackgroundTile { get; set; }
        public string ProfileBackgroundColor { get; set; }
        public string ProfileBackgroundImageUrl { get; set; }
        public string ProfileBackgroundImageUrlHttps { get; set; }
        public string ProfileBannerUrl { get; set; }
        public string ProfileTextColor { get; set; }
        public string ProfileLinkColor { get; set; }
        public bool ProfileUseBackgroundImage { get; set; }
        public bool IsTranslator { get; set; }
        public bool ContributorsEnabled { get; set; }
        public string UtcOffset { get; set; }
        public string TimeZone { get; set; }
        public string WithheldInCountries { get; set; }
        public string WithheldScope { get; set; }

        #endregion

        //            "entities"

        #region Geo Related 

        /// <summary>
        /// Location of twitter user if applicable
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// If true, user has Geo Enabled tweets turned on
        /// </summary>
        public bool GeoEnabled { get; set; }

        #endregion

        #region Statistics Related

        public string StatusesCount { get; set; }

        public string FollowersCount { get; set; }
        public string FriendsCount { get; set; }
        public string FavouritesCount { get; set; }
        public string ListedCount { get; set; }

        #endregion

        /// <summary>
        /// List of Tweets published by this twitter user
        /// </summary>
        public List<string> TweetIds { get { return _tweetIds; } }
        private List<string> _tweetIds = new List<string>();

        /// <summary>
        /// List of Tweets liked by this twitter user
        /// </summary>
        public List<string> LikedTweets { get { return _likedTweets; } }
        private List<string> _likedTweets = new List<string>();
    }
}
