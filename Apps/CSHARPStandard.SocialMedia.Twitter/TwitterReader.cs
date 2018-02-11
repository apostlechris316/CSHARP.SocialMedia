using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Tweetinvi;
using Tweetinvi.Json;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// General TwitterStream class built on Twitterizer.
    /// NOTE: You must override callbacks in order to process messages.
    /// </summary>
    public class TwitterReader : TweetParser
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

        /// <summary>
        /// Authenticate With Twitter
        /// </summary>
        /// <returns></returns>
        private IAuthenticatedUser AuthenticateWithTwitter()
        {
            Auth.SetUserCredentials(ConsumerKey, ConsumerSharedSecret, OAuthToken, OAuthTokenSecret);

            TweetinviEvents.QueryBeforeExecute += (sender, args) => { Console.WriteLine(args.QueryURL); };

            return User.GetAuthenticatedUser();
        }

        /// <summary>
        /// Get the Unique Id in twitter for the TwiterHandle passed in.
        /// </summary>
        /// <param name="userName">Twitter Handle</param>
        /// <returns></returns>
        public long GetUserProfileId(string userName)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            var user = User.GetUserFromScreenName(userName);
            return user.Id;
        }

        public string GetUserProfile(string userName)
        {
            throw new NotImplementedException("Birthday is not currently supported. Will work on twitter profile later.");
//            var user = User.GetUserFromScreenName(userName);
//            user.Id
//            user.CreatedAt
//                user.Location
//                user.
        }

        public string GetTweets()
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            throw new NotImplementedException(); 
        }

        #region Get Users

        /// <summary>
        /// Gets a list of users whom are following us
        /// </summary>
        /// <param name="screenName"></param>
        public List<IUser> GetFollowers(string screenName)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            List<IUser> followers = new List<IUser>();
            followers.AddRange(User.GetFollowers(screenName));
            return followers;
        }

        /// <summary>
        /// Gets a list of users we are following
        /// </summary>
        /// <param name="screenName"></param>
        public List<IUser> GetFollowing(string screenName)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            List<IUser> following = new List<IUser>();
            following.AddRange(User.GetFriends(screenName));
            return following;
        }

        /// <summary>
        /// Get followers
        /// </summary>
        /// <returns></returns>
        public string GetFollowersAsJson()
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();
            return Account.GetUsersRequestingFriendship().ToJson();
        }

        /// <summary>
        /// Get those you are following
        /// </summary>
        /// <returns></returns>
        public string GetFollowingAsJson()
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            return Account.GetUsersYouRequestedToFollow().ToJson();
        }

        #endregion

        #region Direct Messages

        /// <summary>
        /// Get Latest Messages Received
        /// </summary>
        /// <returns></returns>
        public string GetLatestMessagesReceived()
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            return MessageJson.GetLatestMessagesReceived();
        }

        /// <summary>
        /// Get Latest Messages Received
        /// </summary>
        /// <returns></returns>
        public string GetLatestMessagesSent()
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            return MessageJson.GetLatestMessagesSent();
        }

        #endregion

        #region Get Tweets

        /// <summary>
        /// Get the tweets for a user and returns them as JSON
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="excludeReplies"></param>
        /// <param name="includeRetweets"></param>
        /// <param name="trimUser"></param>
        /// <returns></returns>
        public string GetTweetsAsJson(string screenName, bool excludeReplies, bool includeRetweets, bool trimUser)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            // TO DO: We need to ensure screenName is not empty

            IUser user = User.GetUserFromScreenName(screenName);

            // Create a parameter for queries with specific parameters
            var timelineParameter = Timeline.CreateUserTimelineParameter();
            timelineParameter.ExcludeReplies = true;
            timelineParameter.TrimUser = true;
            timelineParameter.IncludeRTS = false;

//            Tweetinvi.Trends

            return Timeline.GetUserTimeline(user, timelineParameter).ToJson();
        }
        #endregion

        #region List Related

        // TO DO: Create a list 
        public void CreateList(string listName, string listDescription, string listMode)
        {

            throw new NotImplementedException();
            // var list = TwitterList.CreateList("<name>", PrivacyMode.Public, "<description>");

            // TO DO: Add people to the list

            // TO DO: Get the tweets for the list
        }

        /// <summary>
        /// Gets a list based on Twitter list id
        /// </summary>
        /// <param name="listId">Unique id from twitter for the list</param>
        /// <returns></returns>
        public ITwitterList GetList(long listId)
        {
            return TwitterList.GetExistingList(listId);
        }

        /// <summary>
        /// Get a list based on list name and Twitter screen name of owner
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="screenName"></param>
        /// <returns></returns>
        public ITwitterList GetList(string listName, string screenName)
        {
            return TwitterList.GetExistingList(listName, screenName);
        }

        /// <summary>
        /// Get a list based on list name and Unique Twitter Id of owner
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ITwitterList GetList(string listName, long userId)
        {
            return TwitterList.GetExistingList(listName, userId);
        }

        /// <summary>
        /// Returns the tweets for list members as a JSON string
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetTweetsFromListAsJson(ITwitterList list)
        {
            // Authenticate with Twitter first
            AuthenticateWithTwitter();

            // From a list object
            return list.GetTweets().ToJson();
        }

        /// <summary>
        /// Returns the tweets for list members as a JSON string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="includeRetweets"></param>
        /// <param name="includeEntities"></param>
        /// <param name="sinceId"></param>
        /// <param name="maxId"></param>
        /// <returns></returns>
        public string GetTweetsFromListAsJson(ITwitterList list, bool includeRetweets, bool includeEntities, long sinceId, long maxId)
        {
            // From static TwitterList
            //var tweets = TwitterList.GetTweetsFromList();


            var getTweetsParameters = new GetTweetsFromListParameters()
            {
                MaximumNumberOfTweetsToRetrieve = 100,
                IncludeRetweets = includeRetweets,
                IncludeEntities = includeEntities,
                SinceId = sinceId,
                MaxId = maxId
            };

            return list.GetTweets(getTweetsParameters).ToJson();
        }

        public void UpdateList()
        {
            //var updateParameter = new TwitterListUpdateParameters()
            //{
            //    Name = "<new_name>",
            //    Description = "<new_description>",
            //    PrivacyMode = PrivacyMode.Public
            //};

            //// From a List object
            //var success = list.Update(updateParameter);

            //// From Static TwitterList
            //var success = TwitterList.UpdateList(list, updateParameter);
        }

        #endregion

    }
}
