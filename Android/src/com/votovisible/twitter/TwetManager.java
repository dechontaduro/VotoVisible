package com.votovisible.twitter;

import twitter4j.Twitter;
import twitter4j.TwitterFactory;
import twitter4j.auth.AccessToken;
import twitter4j.auth.RequestToken;
import twitter4j.conf.Configuration;
import twitter4j.conf.ConfigurationBuilder;

/**
 * Created by Cristian Cantillo on 6/11/13.
 */
public class TwetManager {

    Twitter twitter = null;
    private AccessToken token = null;
    private RequestToken mRequestToken = null;
    Preferences settings = null;
    public String TW_ACCTOKEN = "tw_acces_token";
    public String TW_ACCTOKEN_SECRET = "tw_acces_token_secret";


    public TwetManager(Preferences settings){
        this.settings = settings;
        twitter = new TwitterFactory().getInstance();
        twitter.setOAuthConsumer(Constantes.CONSUMER_KEY, Constantes.CONSUMER_SECRET);

        try{
            mRequestToken = twitter.getOAuthRequestToken(Constantes.CALLBACK_URL);
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public String getUrlAutenticacion(){
        return mRequestToken.getAuthenticationURL();
    }

    public void setTokens(String oAuth){
        try{
            token = twitter.getOAuthAccessToken(mRequestToken, oAuth);
            twitter.setOAuthAccessToken(token);

            //settings.addValor(TW_ACCTOKEN, token.getToken());
            //settings.addValor(TW_ACCTOKEN_SECRET, token.getToken());
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public void setTokens(){
        String accessToken = settings.getValor(TW_ACCTOKEN);
        String accessTokenSecret = settings.getValor(TW_ACCTOKEN_SECRET);

        Configuration conf = new ConfigurationBuilder()
                .setOAuthConsumerKey(Constantes.CONSUMER_KEY)
                .setOAuthConsumerSecret(Constantes.CONSUMER_SECRET)
                .setOAuthAccessToken(accessToken)
                .setOAuthAccessTokenSecret(accessTokenSecret).build();

        try{
            twitter = new TwitterFactory(conf).getInstance();
        }catch(Exception e){
            e.printStackTrace();
        }
    }

    public boolean enviarTwet(String texto){
        try{
            twitter.updateStatus(texto);
            return true;
        }catch (Exception e){
            e.printStackTrace();
            return false;
        }
    }

    public boolean existenKeys(){
        if (settings.isExist(TW_ACCTOKEN))
        {
            if (settings.isExist(TW_ACCTOKEN_SECRET)){
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
    }
}
