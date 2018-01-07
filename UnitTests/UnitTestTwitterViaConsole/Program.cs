using CSHARP.SocialMedia.Twitter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestTwitterViaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the Twitter Helper
            var twitter = new TwiterWriter
            {
                // Read the Twitter OAuth Values
                OAuthToken = ConfigurationManager.AppSettings["TwitterOAuthToken"],
                OAuthTokenSecret = ConfigurationManager.AppSettings["TwitterOAuthTokenSecret"],
                ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                ConsumerSharedSecret = ConfigurationManager.AppSettings["TwitterConsumerSharedSecret"]
            };

            // Tweet the messages with no image
            twitter.PublishToTwitter("@RPeplau hope you are having a good year so far");

            // Tweet with image
//            twitter.PublishToTwitter("@RPeplau hope you are having a good year so far. This was the beard last year. I will post the new one later", "C:\\Users\\chris\\Pictures\\DIuqzTxUEAAD9ME.jpg");
        }
    }
}
