package com.votovisible.twitter;

/**
 * Created by jgonzalez on 18/11/13.
 */
public class VotoTweet {
    private String id;
    private String votacionId;
    private String twitterAccount;
    private String tipo;
    private String decision;
    private String comentario;
    private String tweetId;
    private String tweet;
    private String retweets;
    private String replies;
    private String creado;
    private String realizado;

    public String getId() { return id; }
    public void setId(String id) { this.id = id; }
    public String getVotacionId() { return votacionId; }
    public void setVotacionId(String votacionId) { this.votacionId = votacionId; }
    public String getTwitterAccount() { return twitterAccount; }
    public void setTwitterAccount(String twitterAccount) { this.twitterAccount = twitterAccount; }
    public String getTipo() { return tipo; }
    public void setTipo(String tipo) { this.tipo = tipo; }
    public String getDecision() { return decision; }
    public void setDecision(String decision) { this.decision = decision; }
    public String getComentario() { return comentario; }
    public void setComentario(String comentario) { this.comentario = comentario; }
    public String getTweetId() { return tweetId; }
    public void setTweetId(String tweetId) { this.tweetId = tweetId; }
    public String getTweet() { return tweet; }
    public void setTweet(String tweet) { this.tweet = tweet; }
    public String getRetweets() { return retweets; }
    public void setRetweets(String retweets) { this.retweets = retweets; }
    public String getReplies() { return replies; }
    public void setReplies(String replies) { this.replies = replies; }
    public String getCreado() { return creado; }
    public void setCreado(String creado) { this.creado = creado; }
    public String getRealizado() { return realizado; }
    public void setRealizado(String realizado) { this.realizado = realizado; }

    public VotoTweet(String id, String votacionId,
                     String twitterAccount, String tipo, String decision,
                     String comentario, String tweetId, String tweet, String retweets, String replies,
                     String creado, String realizado)
    {
        setId(id);
        setVotacionId(votacionId);
        setTwitterAccount(twitterAccount);
        setTipo(tipo);
        setDecision(decision);
        setComentario(comentario);
        setTweetId(tweetId);
        setTweet(tweet);
        setRetweets(retweets);
        setReplies(replies);
        setCreado(creado);
        setRealizado(realizado);
    }
}
