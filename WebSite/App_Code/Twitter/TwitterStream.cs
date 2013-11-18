using System;
using System.Configuration;
using System.Linq;
using Tweetinvi;
using TweetinCore.Interfaces.StreamInvi;
using TweetinCore.Interfaces.TwitterToken;
using TwitterToken;
using Streaminvi;
using log4net;

namespace com.VotoVisible.Twitter
{
    /// <summary>
    /// Descripción breve de Stream
    /// </summary>
    public static class Stream
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Rate-Limit
        /// <summary>
        /// Enable you to Get all information from Token and how many query you can execute
        /// Each time a query is executed the XRateLimitRemaining is updated.
        /// To improve efficiency, the other values are NOT.
        /// If you need these please call the function GetRateLimit()
        /// </summary>
        private static void GetRateLimit(IToken token)
        {
            ITokenRateLimits tokenLimits = token.GetRateLimit();
            Console.WriteLine("Remaning Requests for GetRate : {0}", tokenLimits.ApplicationRateLimitStatusLimit.Remaining);
            Console.WriteLine("Total Requests Allowed for GetRate : {0}", tokenLimits.ApplicationRateLimitStatusLimit.Limit);
            Console.WriteLine("GetRate limits will reset at : {0} local time", tokenLimits.ApplicationRateLimitStatusLimit.ResetDateTime.ToLongTimeString());
        }
        #endregion

        // Track Keywords
        private static void StreamFilterBasicTrackExample(IToken token, string hashtag)
        {
            IFilteredStream stream = new FilteredStream();

            stream.StreamStarted += (sender, args) => { Console.WriteLine("Stream has started!"); if (log.IsDebugEnabled) log.DebugFormat("Stream has started!"); };
            stream.AddTrack(hashtag);

            stream.LimitReached += (sender, args) =>
            {
                if (log.IsDebugEnabled) log.DebugFormat("You have missed {0} tweets because you were retrieving more than 1% of tweets", args.Value);
                Console.WriteLine("You have missed {0} tweets because you were retrieving more than 1% of tweets", args.Value);
            };

            TwitterContext context = new TwitterContext();
            if (!context.TryInvokeAction(() => stream.StartStream(token, tweet =>
                    {
                        if (log.IsDebugEnabled) log.DebugFormat("Tweet:{0}", tweet);
                        //TODO: Función delegada
                        Process.exec(tweet);
                        Console.WriteLine(tweet);
                    }
                )))
            {
                Console.WriteLine("An Exception occured : '{0}'", context.LastActionTwitterException.TwitterWebExceptionErrorDescription);
                if (log.IsErrorEnabled) log.Error(String.Format("An Exception occured : '{0}'", context.LastActionTwitterException.TwitterWebExceptionErrorDescription));
            }
        }


        public static void start()
        {
            IToken token = new Token(
                   ConfigurationManager.AppSettings["token_AccessToken"],
                   ConfigurationManager.AppSettings["token_AccessTokenSecret"],
                   ConfigurationManager.AppSettings["token_ConsumerKey"],
                   ConfigurationManager.AppSettings["token_ConsumerSecret"]);

            TokenSingleton.Token = token;
            //GetRateLimit(token);
            StreamFilterBasicTrackExample(token, "#Vo_aV");
        }

    }
}