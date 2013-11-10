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

        public static string add(TweetinCore.Interfaces.ITweet tweet)
        {
            string sql =
                @"INSERT INTO tweet
                    (twAccount,twTweetId,twTweet,twRetweets,twReplies,twCreated,twStatus,twHashtags)
                VALUES
                    (@twAccount,@twTweetId,@twTweet,@twRetweets,@twReplies,@twCreated,@twStatus,@twHashtags)

                SELECT @@IDENTITY";

            GenericProvider gp = new GenericProvider("default");
            object result = gp.GetScalar(sql, CommandType.Text
                                , gp.GetDBParameter("@twAccount", tweet.Creator.ScreenName)
                                , gp.GetDBParameter("@twTweetId", tweet.Id)
                                , gp.GetDBParameter("@twTweet", tweet.Text)
                                , gp.GetDBParameter("@twRetweets", tweet.RetweetCount)
                                , gp.GetDBParameter("@twReplies", 0)
                                , gp.GetDBParameter("@twCreated", tweet.CreatedAt)
                                , gp.GetDBParameter("@twStatus", 0)
                                , gp.GetDBParameter("@twHashtags", getHashtagString(tweet.Hashtags)));

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