using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Parses a tweet in JSON format into objects to work with
    /// </summary>
    public class TweetParser
    {
        /// <summary>
        /// Global Collection of Twitter Users
        /// </summary>
       public Dictionary<string, ITwitterUser> TwitterUserDirectory { get; set; }

        /// <summary>
        /// Global Diretory of Tweets
        /// </summary>
        public Dictionary<string, ITweetData> TweetDirectory { get; set; }

        #region Manage Tweets in JSON Format Related

        /// <summary>
        /// Parses the timeline json into an object model we can manipulate
        /// </summary>
        /// <param name="timelineJson">Json containing timeline information returned from Twitter</param>
        /// <returns></returns>
        public ITwitterTimeline ParseTimelineInJsonFormat(string timelineJson)
        {
            var twitterTimeline = new TwitterTimeline();

            // Use JObject to parse it
            var o = JObject.Parse(timelineJson);

            var tweetData = new TweetData();

            // For each tweet
            foreach (JToken tweetToken in o.First.First.Children())
            {
                // parse the tweet and add to the timeline
                ITweetData newTweet = ParseTweetInJsonFormat(tweetToken);
                twitterTimeline.Tweets.Add(newTweet.TweetId, newTweet);

                // if tweet is not in the global tweet directory then add it
                if (TweetDirectory != null) if (TweetDirectory.ContainsKey(newTweet.TweetId) == false) TweetDirectory.Add(newTweet.TweetId, new TweetDataWithInteraction(newTweet));

                // if there is no owner then it is ours so go to the next one
                if (newTweet.Owner == null) continue;
                var newTwitterUserId = newTweet.Owner.TwitterUserId;

                // Ensure Twitter User is included in TwitterUsers Dictionary
                if (TwitterUserDirectory != null)
                    if (TwitterUserDirectory.ContainsKey(newTwitterUserId) == false)
                        TwitterUserDirectory.Add(newTwitterUserId, newTweet.Owner);

                // New Twitter User so add to collection
                if (twitterTimeline.TwitterUsers.ContainsKey(newTwitterUserId) == false)
                    twitterTimeline.TwitterUsers.Add(newTwitterUserId, newTweet.Owner);

                // New tweet gathered so add to user collection
                if (twitterTimeline.TwitterUsers[newTwitterUserId].TweetIds.Contains(newTweet.TweetId) == false)
                {
                    twitterTimeline.TwitterUsers[newTwitterUserId].TweetIds.Add(newTweet.TweetId);
                }
            }

            return twitterTimeline;
        }

        /// <summary>
        /// Parses a tweet section of a Json to retrieve properties and any child objects and properties
        /// </summary>
        /// <param name="tweetToken">The token object representing the tweet in JSON</param>
        /// <returns></returns>
        public ITweetData ParseTweetInJsonFormat(JToken tweetToken)
        {
            var tweetData = new TweetData();

            #region Collect all the tweet data

            foreach (var token in tweetToken)
            {
                // Get the field name without the quotes
                string[] tokenParts = token.ToString().Split(':');
                string fieldName = tokenParts[0].Replace("\"", string.Empty);
                string fieldValue = tokenParts[1].Replace("\"", string.Empty);

                switch (fieldName)
                {
                    #region Basic Tweet Information

                    case "id":
                        // Store the tweet id as it is needed by other fields
                        tweetData.TweetId = fieldValue;
                        break;
                    case "created_at":
                        tweetData.TweetCreatedDate = fieldValue;
                        break;
                    case "truncated":
                        tweetData.IsTruncated = Convert.ToBoolean(fieldValue);
                        break;
                    case "text":
                        tweetData.TweetText = fieldValue;
                        break;
                    case "display_text_range":
                        break;
                    case "extended_tweet":
                        break;
                    case "full_text":
                        tweetData.TweetFullText = fieldValue;
                        break;

                    case "lang":
                        tweetData.Language = fieldValue;
                        // 42 = English
                        // We can use this for translation if necessary
                        break;

                    case "user":
                        tweetData.Owner = ParseUserSectionOfTweetInJsonFormat(token);
                        break;

                    #endregion

                    #region Tweet Statistics

                    case "favorited":
                        break;
                    case "favorite_count":
                        tweetData.TweetFavoriteCount = Convert.ToInt32(fieldValue);
                        break;
                    case "retweet_count":
                        tweetData.TweetRetweetCount = Convert.ToInt32(fieldValue);
                        break;

                    #endregion

                    #region Geolocation Related

                    case "coordinates":
                        tweetData.Coordinates = fieldValue;
                        break;

                    case "place":
                        tweetData.Place = fieldValue;
                        break;

                    case "source":
                        tweetData.Source = fieldValue;
                        break;

                    #endregion

                    #region Retweet or Quote Related

                    case "retweeted":
                        break;
                    case "retweeted_status":
                        tweetData.RetweetedStatus = ParseTweetInJsonFormat(token);
                        break;
                    case "current_user_retweet":
                        tweetData.CurrentUserRetweet = fieldValue;
                        break;
                    case "in_reply_to_status_id":
                        tweetData.InReplyToStatusId = fieldValue;
                        break;
                    case "in_reply_to_user_id":
                        tweetData.InReplyToUserId = fieldValue;
                        break;
                    case "in_reply_to_screen_name":
                        tweetData.InReplyToScreenName = fieldValue;
                        break;
                    case "quoted_status_id":
                        tweetData.QuotedStatusId = fieldValue;
                        break;
                    case "quoted_status":
                        tweetData.QuotedStatus = ParseTweetInJsonFormat(token);
                        break;

                    #endregion

                    case "entities":
                        tweetData.TwitterEntities.AddRange(ParseEntitySectionOfTweetInJsonFormat(token));
                        break;
                    case "extended_entities":
                        tweetData.TwitterEntities.AddRange(ParseEntitySectionOfTweetInJsonFormat(token));
                        break;

                    case "possibly_sensitive":
                        // TO DO: If true then DAIN will ignore the tweet.
                        break;
                    case "contributorsIds":
                        break;
                    case "contributors":
                        break;
                    case "scopes":
                        break;
                    case "filter_level":
                        break;
                    case "withheld_copyright":
                        break;
                    case "withheld_in_countries":
                        break;
                    case "withheld_scope":
                        break;
                    default:
                        string newField = fieldName;
                        break;
                }
            }

            #endregion

            return tweetData;
        }

        /// <summary>
        /// Parses the entity section into entities list
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public List<ITwitterEntity> ParseEntitySectionOfTweetInJsonFormat(JToken token)
        {
            var entities = new List<ITwitterEntity>();

            // TO DO: These are users related to the tweet. We can look them up to see if they are in the database.
            foreach (var mediaToken in token.First.Children())
            {
                // Get the field name without the quotes
                string[] mediaTokenParts = mediaToken.ToString().Split(':');
                string mediaFieldName = mediaTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                string mediaFieldValue = mediaTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                switch (mediaFieldName.ToUpperInvariant())
                {
                    case "MEDIA":

                        if (mediaToken.First.First == null) continue;

                        // TO DO: These are pictures so they would be downloaded to a media folder locally so they can be reused.
                        var mediaEntity = new TwitterMediaEntity();

                        foreach(var mediaItemToken in mediaToken.First.First.Children())
                        {
                            // Get the field name without the quotes
                            string[] mediaItemTokenParts = mediaItemToken.ToString().Split(':');
                            string mediaItemFieldName = mediaItemTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                            string mediaItemFieldValue = mediaTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                            switch (mediaItemFieldName.ToUpperInvariant())
                            {
                                case "ID":
                                    mediaEntity.Id = mediaItemFieldValue;
                                    break;
                                case "URL":
                                    mediaEntity.Url = mediaItemFieldValue;
                                    break;
                                case "DISPLAY_URL":
                                    mediaEntity.DisplayUrl = mediaItemFieldValue;
                                    break;
                                case "EXPANDED_URL":
                                    mediaEntity.ExpandedUrl = mediaItemFieldValue;
                                    break;
                                case "MEDIA_URL":
                                    mediaEntity.MediaUrl = mediaItemFieldValue;
                                    break;
                                case "MEDIA_URL_HTTPS":
                                    mediaEntity.MediaUrlHttps = mediaItemFieldValue;
                                    break;
                                case "TYPE":
                                    mediaEntity.MediaType = mediaItemFieldValue;
                                    break;
                                case "VIDEO_INFO":
                                    mediaEntity.VideoInfo = mediaItemFieldValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        entities.Add(mediaEntity);
                        break;
                    case "URLS":

                        if (mediaToken.First.First == null) continue;

                        // TO DO: This is likely going to be assets
                        var urlEntity = new TwitterUrlEntity();

                        foreach (var urlItemToken in mediaToken.First.First.Children())
                        {
                            // Get the field name without the quotes
                            string[] urlItemTokenParts = urlItemToken.ToString().Split(':');
                            string urlItemFieldName = urlItemTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                            string urlItemFieldValue = urlItemTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                            switch (urlItemFieldName.ToUpperInvariant())
                            {
                                case "URL":
                                    urlEntity.Id = urlItemFieldValue;
                                    urlEntity.Url = urlItemFieldValue;
                                    break;
                                case "DISPLAY_URL":
                                    urlEntity.DisplayUrl = urlItemFieldValue;
                                    break;
                                case "EXPANDED_URL":
                                    urlEntity.ExpandedUrl = urlItemFieldValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        entities.Add(urlEntity);
                        break;
                    case "USER_MENTIONS":

                        if (mediaToken.First.First == null) continue;

                        // These are users mentioned in the tweet so are possible contacts
                        var userMention = new TwitterUserMentionEntity();

                        foreach (var userMentionItemToken in mediaToken.First.First.Children())
                        {
                            // Get the field name without the quotes
                            string[] userMentionItemTokenParts = userMentionItemToken.ToString().Split(':');
                            string userMentionItemFieldName = userMentionItemTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                            string userMentionItemFieldValue = userMentionItemTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                            switch (userMentionItemFieldName.ToUpperInvariant())
                            {
                                case "ID":
                                    userMention.Id = userMentionItemFieldValue;
                                    break;
                                case "SCREEN_NAME":
                                    userMention.ScreenName = userMentionItemFieldValue;
                                    break;
                                case "NAME":
                                    userMention.Name = userMentionItemFieldValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        entities.Add(userMention);
                        break;
                    case "HASHTAGS":

                        if (mediaToken.First.First == null) continue;

                        // TO DO: These are possible keywords
                        var hashTag = new TwitterHashTagEntity();

                        foreach (var hashTagItemToken in mediaToken.First.First.Children())
                        {
                            // Get the field name without the quotes
                            string[] hashTagItemTokenParts = hashTagItemToken.ToString().Split(':');
                            string hashTagItemFieldName = hashTagItemTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                            string hashTagItemFieldValue = hashTagItemTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                            switch (hashTagItemFieldName.ToUpperInvariant())
                            {
                                case "TEXT":
                                    hashTag.Id = hashTagItemFieldValue;
                                    hashTag.Text = hashTagItemFieldValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        entities.Add(hashTag);
                        break;
                    case "SYMBOLS":

                        if (mediaToken.First.First == null) continue;

                        // TO DO: Not sure what these are

                        foreach (var symbolItemToken in mediaToken.First.First.Children())
                        {
                            // Get the field name without the quotes
                            string[] symbolItemTokenParts = symbolItemToken.ToString().Split(':');
                            string symbolItemFieldName = symbolItemTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                            string symbolItemFieldValue = symbolItemTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                            switch (symbolItemFieldName)
                            {
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        string fieldValue = mediaFieldValue;
                        break;
                }
            }

            return entities;
        }

        public ITwitterUser ParseUserSectionOfTweetInJsonFormat(JToken token)
        {
            // TO DO: For now twitter user is returned as standard twitter user.
            var twitterUser = new TwitterUser();

            // TO DO: These are users related to the tweet. We can look them up to see if they are in the database.
            foreach (var userToken in token.First.Children())
            {
                // Get the field name without the quotes
                string[] userTokenParts = userToken.ToString().Split(':');
                string userFieldName = userTokenParts[0].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("{", string.Empty).Trim();
                string userFieldValue = userTokenParts[1].Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim();

                switch (userFieldName.ToUpperInvariant())
                {
                    #region Basic Twitter Information 

                    case "ID":
                        twitterUser.TwitterUserId = userFieldValue;
                        break;
                    case "CREATED_AT":
                        twitterUser.TwiterUserJoined = userFieldValue;
                        break;
                    case "SCREEN_NAME":
                        twitterUser.TwitterHandle = userFieldValue;
                        break;
                    case "NAME":
                        twitterUser.TwitterName = userFieldValue;
                        break;
                    case "DESCRIPTION":
                        twitterUser.Description = userFieldValue;
                        break;

                    #endregion

                    #region Profile Settings Related

                    case "EMAIL":
                        twitterUser.Email = userFieldValue;
                        break;
                    case "LANG":
                        twitterUser.LanguageCode = userFieldValue;
                        break;
                    case "URL":
                        twitterUser.Url = userFieldValue;
                        break;

                    #endregion

                    #region Geolocation Related 

                    case "LOCATION":
                        twitterUser.Location = userFieldValue;
                        break;
                    case "GEO_ENABLED":
                        twitterUser.GeoEnabled = Convert.ToBoolean(userFieldValue);
                        break;

                    #endregion

                    default:
                        var myFieldName = userFieldName;
                        //{ "name":null,"status":null,"description":null,"created_at":"0001-01-01T00:00:00","location":null,"geo_enabled":false,"url":null,"lang":0,
                        //"email":null,"statuses_count":0,"followers_count":0,"friends_count":0,"following":false,"protected":false,"verified":false,
                        //"entities":null,"notifications":false,"profile_image_url":null,"profile_image_url_https":null,"follow_request_sent":false,
                        //"default_profile":false,"default_profile_image":false,"favourites_count":null,"listed_count":null,
                        //"profile_sidebar_fill_color":null,"profile_sidebar_border_color":null,"profile_background_tile":false,"profile_background_color":null,"profile_background_image_url":null,"profile_background_image_url_https":null,"profile_banner_url":null,"profile_text_color":null,"profile_link_color":null,"profile_use_background_image":false,"is_translator":false,"contributors_enabled":false,"utc_offset":null,"time_zone":null,"withheld_in_countries":null,"withheld_scope":null,
                        //"Id":951151093790269441,"id_str":"951151093790269441","screen_name":null
                        break;
                }
            }

            return twitterUser;
        }

        #endregion
    }
}
