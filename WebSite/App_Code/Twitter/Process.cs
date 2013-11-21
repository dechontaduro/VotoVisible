using System;
using ProtoBuf;
using ProtoBuf.Serializers;
using ProtoBuf.Compiler;

using log4net;
using System.Collections.Generic;

namespace com.VotoVisible.Twitter
{
    /// <summary>
    /// Descripción breve de Process
    /// </summary>
    public class Process
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Process()
        {
        }

        public static void exec(TweetinCore.Interfaces.ITweet tweet)
        {
            byte[] tweetByte = null; // SerializeProtocalBufferTweet(tweet);
            string tweetStr = null; // com.VotoVisible.Utils.Serializer.SerializeObj2XML(tweet);
            com.VotoVisible.Manager.Tweet.add(tweet, tweetByte, tweetStr);
            processTweet(tweet);
        }


        public static void processTweet(TweetinCore.Interfaces.ITweet tweet)
        {
            if (tweet.Retweeting != null)
                processRetweet(tweet.Retweeting);
            else if (tweet.InReplyToStatusIdStr != null)
                processReplies(tweet.InReplyToStatusIdStr);
            else if (tweet.Favourited != null && tweet.Favourited == true)
                processFavorites(tweet.IdStr);
            else
                processVoto(tweet);
        }

        public static void processRetweet(TweetinCore.Interfaces.ITweet tweet)
        {
            List<com.VotoVisible.Entitity.Voto> votos =
                com.VotoVisible.Manager.Voto.search("", "", "", "", "", tweet.IdStr);

            if (votos.Count > 0)
            {
                com.VotoVisible.Entitity.Voto voto = votos[0];
                voto.retweets = tweet.RetweetCount;
                com.VotoVisible.Manager.Voto.update(voto);
            }
        }

        public static void processReplies(string tweetId)
        {
            List<com.VotoVisible.Entitity.Voto> votos =
                com.VotoVisible.Manager.Voto.search("", "", "", "", "", tweetId);

            if (votos.Count > 0)
            {
                com.VotoVisible.Entitity.Voto voto = votos[0];
                voto.replies++;
                com.VotoVisible.Manager.Voto.update(voto);
            }
        }

        public static void processFavorites(string tweetId)
        {
            List<com.VotoVisible.Entitity.Voto> votos =
                com.VotoVisible.Manager.Voto.search("", "", "", "", "", tweetId);

            if (votos.Count > 0)
            {
                com.VotoVisible.Entitity.Voto voto = votos[0];
                voto.favorites++;
                com.VotoVisible.Manager.Voto.update(voto);
            }
        }

        public static void processVoto(TweetinCore.Interfaces.ITweet tweet)
        {
            List<TweetinCore.Interfaces.IHashTagEntity> hashTags = tweet.Hashtags;

            if (hashTags.Count < 3)
                return;

            try
            {
                string votacionId = hashTags[2].Text;

                int votacionIdD = (int)com.VotoVisible.Utils.Conversion.Base36Decode(votacionId);
                votacionId = votacionIdD.ToString();

                List<com.VotoVisible.Entitity.Voto> votos =
                    com.VotoVisible.Manager.Voto.search(tweet.Creator.ScreenName, votacionId, "", "", "", "");

                string decision = hashTags[1].Text;
                string comentario = tweet.Text;

                com.VotoVisible.Entitity.Voto voto = null;
                if (votos.Count > 0)
                {
                    if (voto.realizado != null)
                        return;

                    voto = votos[0];
                    voto.tipo = 0;
                    voto.decision = decision;
                    voto.comentario = comentario;
                    voto.tweetId = tweet.IdStr;
                    voto.tweet = tweet.Source;
                    voto.retweets = 0;
                    voto.replies = 0;
                    voto.favorites = 0;
                    voto.realizado = tweet.CreatedAt;
                    com.VotoVisible.Manager.Voto.update(voto);
                }
                else
                {
                    voto = new Entitity.Voto(null, votacionIdD, tweet.Creator.ScreenName
                                            , 0, decision, comentario, tweet.IdStr, tweet.Source
                                            , 0, 0, 0, tweet.CreatedAt, tweet.CreatedAt);
                    com.VotoVisible.Manager.Voto.add(voto);
                }
            }
            catch (Exception ex)
            {
                if (log.IsErrorEnabled) log.Error(string.Format("TweetId:{0}", tweet.IdStr), ex);
            }
        }

        public static byte[] SerializeProtocalBufferTweet(TweetinCore.Interfaces.ITweet tweet)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<TweetinCore.Interfaces.ITweet>(ms, tweet);
                return ms.ToArray();
            }
        }

        public static TweetinCore.Interfaces.ITweet DeserializeProtocalBufferTweet(byte[] buffer)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer))
            {
                TweetinCore.Interfaces.ITweet tweet = 
                    ProtoBuf.Serializer.Deserialize<TweetinCore.Interfaces.ITweet>(ms); 
                return tweet;
            }
        }
    }
}