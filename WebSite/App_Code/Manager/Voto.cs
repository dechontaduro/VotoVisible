using System;
using System.Data;
using System.Collections.Generic;
using com.VotoVisible.Utils;

namespace com.VotoVisible.Manager
{
    /// <summary>
    /// Descripción breve de Voto
    /// </summary>
    public class Voto
    {
        public Voto()
        {

        }

        private static Entitity.Voto convert2Voto(DataRow voto)
        {
            Entitity.Voto obj = new Entitity.Voto(
                (int)Conversion.Obj2Int(voto["votoId"]),
                (int)Conversion.Obj2Int(voto["votaId"]),
                Conversion.Obj2String(voto["twitterAccount"]),
                Conversion.Obj2Int(voto["votoTipo"]),
                Conversion.Obj2String(voto["votoDecision"]),
                Conversion.Obj2String(voto["votoComentario"]),
                Conversion.Obj2String(voto["tweetId"]),
                Conversion.Obj2String(voto["tweet"]),
                Conversion.Obj2Int(voto["retweets"]),
                Conversion.Obj2Int(voto["replies"]),
                Conversion.Obj2Int(voto["favorites"]),
                Conversion.Obj2DateTime(voto["votoCreado"]),
                Conversion.Obj2DateTime(voto["votoRealizado"]));
            return obj;
        }

        private static Entitity.VotacionTotales convert2VotacionTotales(DataRow votacionTotales)
        {
            Entitity.VotacionTotales obj = new Entitity.VotacionTotales(
                Conversion.Obj2String(votacionTotales["votoDecision"]),
                (int)Conversion.Obj2Int(votacionTotales["Publico"]),
                (int)Conversion.Obj2Int(votacionTotales["Privado"]));
            return obj;
        }

        public static Entitity.Voto getById(string id)
        {
            string sql =
                @"SELECT votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votoId = @id";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@id", id));

            if (dt == null || dt.Rows.Count == 0)
                return null;

            Entitity.Voto obj = convert2Voto(dt.Rows[0]);
            return obj;
        }

        public static List<Entitity.Voto> getByVotacion(string votacionId)
        {
            string sql =
                @"SELECT votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId
                ORDER BY votoRealizado";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            List<Entitity.Voto> lst = new List<Entitity.Voto>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach(DataRow dr in dt.Rows)
                lst.Add(convert2Voto(dr));

            return lst;
        }

        public static List<Entitity.Voto> getRealizadosByVotacion(string votacionId)
        {
            string sql =
                @"SELECT votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId AND votoRealizado IS NOT NULL
                ORDER BY votoRealizado";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            List<Entitity.Voto> lst = new List<Entitity.Voto>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2Voto(dr));

            return lst;
        }

        public static Entitity.Voto getMostRetweetByVotacion(string votacionId)
        {
            string sql =
                @"SELECT TOP 1 votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId AND tweetId IS NOT NULL
                ORDER BY ISNULL(retweets, 0) DESC";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            if (dt == null || dt.Rows.Count == 0)
                return null;

            Entitity.Voto obj = convert2Voto(dt.Rows[0]);
            return obj;
        }

        public static Entitity.Voto getMostReplyByVotacion(string votacionId)
        {
            string sql =
                @"SELECT TOP 1 votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId AND tweetId IS NOT NULL
                ORDER BY ISNULL(replies, 0) DESC";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            if (dt == null || dt.Rows.Count == 0)
                return null;

            Entitity.Voto obj = convert2Voto(dt.Rows[0]);
            return obj;
        }

        public static Entitity.Voto getVotoAleatorio()
        {
            string sql =
                @"SELECT TOP 1 votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE tweetId IS NOT NULL
                ORDER BY NEWID()";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            Entitity.Voto obj = convert2Voto(dt.Rows[0]);
            return obj;
        }

        public static List<Entitity.VotacionTotales> getTotalesByVotacion(string votacionId)
        {
            string sql =
                @"SELECT votoDecision
	                , SUM(CASE WHEN votoTipo = 0 THEN 1 ELSE 0 END) Publico
	                , SUM(CASE WHEN votoTipo = 1 THEN 1 ELSE 0 END) Privado
                FROM voto
                WHERE votaId = @votacionId AND votoDecision IS NOT NULL
                GROUP BY votoDecision
                ORDER BY SUM(CASE WHEN votoTipo = 0 THEN 1 ELSE 0 END)+SUM(CASE WHEN votoTipo = 1 THEN 1 ELSE 0 END) DESC ";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            List<Entitity.VotacionTotales> lst = new List<Entitity.VotacionTotales>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2VotacionTotales(dr));

            return lst;
        }

        public static int delete(int votacionId, bool onlyEmpty)
        {
            string sql =
                @"DELETE FROM voto WHERE votaId = @votacionId ";
            if (onlyEmpty)
                sql += "AND votoRealizado IS NULL";

            GenericProvider gp = new GenericProvider("default");
            int recs = gp.ExecuteNonQuery(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));
            return recs;
        }

        public static string add(Entitity.Voto voto)
        {
            string sql =
                @"INSERT INTO voto
                (votaId,twitterAccount,votoTipo,votoDecision,votoComentario
                    ,tweetId,tweet,retweets,replies,favorites,votoCreado,votoRealizado)
                VALUES
                (@votaId, @twitterAccount, @votoTipo, @votoDecision, @votoComentario
                    , @tweetId, @tweet, @retweets, @replies, @favorites, @votoCreado, @votoRealizado)

                SELECT @@IDENTITY";

            GenericProvider gp = new GenericProvider("default");
            object result = gp.GetScalar(sql, CommandType.Text
                                , gp.GetDBParameter("@votaId", voto.votacionId)
                                , gp.GetDBParameter("@twitterAccount", voto.twitterAccount)
                                , gp.GetDBParameter("@votoTipo", voto.tipo)
                                , gp.GetDBParameter("@votoDecision", voto.decision)
                                , gp.GetDBParameter("@votoComentario", voto.comentario)
                                , gp.GetDBParameter("@tweetId", voto.tweetId)
                                , gp.GetDBParameter("@tweet", voto.tweet)
                                , gp.GetDBParameter("@retweets", voto.retweets)
                                , gp.GetDBParameter("@replies", voto.replies)
                                , gp.GetDBParameter("@favorites", voto.favorites)
                                , gp.GetDBParameter("@votoCreado", voto.creado)
                                , gp.GetDBParameter("@votoRealizado", voto.realizado));

            if (result == null)
                return "";

            return result.ToString();
        }

        public static string update(Entitity.Voto voto)
        {
            string sql =
                @"UPDATE voto
                SET 
                  twitterAccount = @twitterAccount
                  --,votaId = @votaId
                  ,votoTipo = @votoTipo
                  ,votoDecision = @votoDecision
                  ,votoComentario = @votoComentario
                  ,tweetId = @tweetId
                  ,tweet = @tweet
                  ,retweets = @retweets
                  ,replies = @replies
                  ,favorites = @favorites
                  ,votoCreado = @votoCreado
                  ,votoRealizado = @votoRealizado
                 WHERE votoId = @votoId";

            GenericProvider gp = new GenericProvider("default");
            int recs= gp.ExecuteNonQuery(sql, CommandType.Text
                                , gp.GetDBParameter("@votoId", voto.id)
                                //, gp.GetDBParameter("@votaId", voto.votacionId)
                                , gp.GetDBParameter("@twitterAccount", voto.twitterAccount)
                                , gp.GetDBParameter("@votoTipo", voto.tipo)
                                , gp.GetDBParameter("@votoDecision", voto.decision)
                                , gp.GetDBParameter("@votoComentario", voto.comentario)
                                , gp.GetDBParameter("@tweetId", voto.tweetId)
                                , gp.GetDBParameter("@tweet", voto.tweet)
                                , gp.GetDBParameter("@retweets", voto.retweets)
                                , gp.GetDBParameter("@replies", voto.replies)
                                , gp.GetDBParameter("@favorites", voto.favorites)
                                , gp.GetDBParameter("@votoCreado", voto.creado)
                                , gp.GetDBParameter("@votoRealizado", voto.realizado));
            if(recs != 1)
                return "";

            return voto.id.ToString();
        }

        public static List<Entitity.Voto> 
                    search(string twitterAccount, string votacionId
                        , string corporacion, string numero, string anio
                        , string tweetId)
        {
            //TODO: implementar otras búsquedas
            string sql =
                @"SELECT votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies, favorites
	                , votoCreado, votoRealizado
                FROM voto
                WHERE {0}
                ORDER BY votoRealizado";

            GenericProvider gp = new GenericProvider("default");

            List<System.Data.Common.DbParameter> pars = new List<System.Data.Common.DbParameter>();

            if (tweetId != "")
            {
                sql = String.Format(sql, "tweetId = @tweetId");
                pars.Add(gp.GetDBParameter("@tweetId", tweetId));
            }
            else if (twitterAccount != "" && votacionId != "")
            {
                sql = String.Format(sql, "votaId = @votacionId AND twitterAccount = @twitterAccount");
                pars.Add(gp.GetDBParameter("@votacionId", votacionId));
                pars.Add(gp.GetDBParameter("@twitterAccount", twitterAccount));
            }

            DataTable dt = gp.GetTable(sql, CommandType.Text, pars.ToArray());

            List<Entitity.Voto> lst = new List<Entitity.Voto>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2Voto(dr));

            return lst;
        }
    }
}