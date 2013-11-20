using System;
using ProtoBuf;
using ProtoBuf.Serializers;
using ProtoBuf.Compiler;

using log4net;

namespace com.VotoVisible.Twitter
{
    /// <summary>
    /// Descripción breve de Process
    /// </summary>
    public class Process
    {
        public Process()
        {
        }

        public static void exec(TweetinCore.Interfaces.ITweet tweet)
        {
            byte[] tweetByte = null; // SerializeProtocalBufferTweet(tweet);
            string tweetStr = null; // com.VotoVisible.Utils.Serializer.SerializeObj2XML(tweet);
            com.VotoVisible.Manager.Tweet.add(tweet, tweetByte, tweetStr);
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