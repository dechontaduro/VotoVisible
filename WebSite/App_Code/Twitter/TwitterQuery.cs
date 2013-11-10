using System;
using System.Configuration;
using System.Linq;
using Tweetinvi;
using TweetinCore.Interfaces.StreamInvi;
using TweetinCore.Interfaces.TwitterToken;
using TweetinCore.Enum;
using TwitterToken;
using Streaminvi;
using log4net;

/// <summary>
/// Descripción breve de TwitterQuery
/// </summary>
public class TwitterQuery
{
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IToken token;

	public TwitterQuery()
	{
        token = new Token(
               ConfigurationManager.AppSettings["token_AccessToken"],
               ConfigurationManager.AppSettings["token_AccessTokenSecret"],
               ConfigurationManager.AppSettings["token_ConsumerKey"],
               ConfigurationManager.AppSettings["token_ConsumerSecret"]);
        TokenSingleton.Token = token;
        //GetRateLimit(token);
	}

    public string getProfileImage(string alias)
    {
        string path = ConfigurationManager.AppSettings["twitter_imageprofilepath"];
        User user = new User(alias, this.token);
        string filePath = user.DownloadProfileImage(ImageSize.original, path);
        return filePath;
    }

    public void getUserList(string alias)
    {
        //TODO: implementar
    }
}