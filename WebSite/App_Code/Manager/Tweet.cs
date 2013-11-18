using System;
using System.Data;
using System.Collections.Generic;

namespace com.VotoVisible.Manager
{
    /// <summary>
    /// Descripción breve de Tweet
    /// </summary>
    public class Tweet
    {
        public Tweet()
        {

        }
        
        //http://www.codeproject.com/Articles/32216/How-to-store-and-fetch-binary-data-into-a-file-str
        public static string add(TweetinCore.Interfaces.ITweet tweet, byte[] tweetByte, string tweetStr)
        {
            string sql =
                @"INSERT INTO tweet
                    (twAccount,twTweetId,twText,twRetweets,twReplies,twCreated,twStatus,twHashtags,twTweet)
                VALUES
                    (@twAccount,@twTweetId,@twText,@twRetweets,@twReplies,@twCreated,@twStatus,@twHashtags,@twTweet)

                SELECT @@IDENTITY";

            GenericProvider gp = new GenericProvider("default");
            object result = gp.GetScalar(sql, CommandType.Text
                                , gp.GetDBParameter("@twAccount", tweet.Creator.ScreenName)
                                , gp.GetDBParameter("@twTweetId", tweet.Id)
                                , gp.GetDBParameter("@twText", tweet.Text)
                                , gp.GetDBParameter("@twRetweets", tweet.RetweetCount)
                                , gp.GetDBParameter("@twReplies", 0)
                                , gp.GetDBParameter("@twCreated", tweet.CreatedAt)
                                , gp.GetDBParameter("@twStatus", 0)
                                , gp.GetDBParameter("@twHashtags", getHashtagString(tweet.Hashtags))
                                , gp.GetDBParameter("@twTweetBin", tweetByte)
                                , gp.GetDBParameter("@twTweet", tweetStr)
                                );

            if (result == null)
                return "";

            return result.ToString();
        }

        private static string getHashtagString(List<TweetinCore.Interfaces.IHashTagEntity> hashtags)
        {
            string shashtags = "";
            foreach (TweetinCore.Interfaces.IHashTagEntity hashtag in hashtags)
            {
                shashtags += "¬" + hashtag.Text;
            }
            return shashtags;
        }
    }
}