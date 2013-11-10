using System;

namespace com.VotoVisible.Entitity
{
    /// <summary>
    /// Descripción breve de Voto
    /// </summary>
    public class Voto
    {
        public int? id;
        public int votacionId;
        public string twitterAccount;
        public int? tipo;
        public string decision;
        public string comentario;
        public string tweetId;
        public string tweet;
        public int? retweets;
        public int? replies;
        public DateTime? creado;
        public DateTime? realizado;

        public Voto() { }
        public Voto(int? id,
                    int votacionId,
                    string twitterAccount,
                    int? tipo,
                    string decision,
                    string comentario,
                    string tweetId,
                    string tweet,
                    int? retweets,
                    int? replies,
                    DateTime? creado,
                    DateTime? realizado)
        {
            this.id = id;
            this.votacionId = votacionId;
            this.twitterAccount = twitterAccount;
            this.tipo = tipo;
            this.decision = decision;
            this.comentario = comentario;
            this.tweetId = tweetId;
            this.tweet = tweet;
            this.retweets = retweets;
            this.replies = replies;
            this.creado = creado;
            this.realizado = realizado;
            
        }
    }
}