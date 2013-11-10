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
	                , tweetId, tweet, retweets, replies
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
	                , tweetId, tweet, retweets, replies
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

        public static Entitity.Voto getMostRetweetByVotacion(string votacionId)
        {
            string sql =
                @"SELECT TOP 1 votoId, votaId, twitterAccount, votoTipo, votoDecision, votoComentario
	                , tweetId, tweet, retweets, replies
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId
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
	                , tweetId, tweet, retweets, replies
	                , votoCreado, votoRealizado
                FROM voto
                WHERE votaId = @votacionId
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
	                , tweetId, tweet, retweets, replies
	                , votoCreado, votoRealizado
                FROM voto
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
                WHERE votaId = @votacionId
                GROUP BY votoDecision";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@votacionId", votacionId));

            List<Entitity.VotacionTotales> lst = new List<Entitity.VotacionTotales>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2VotacionTotales(dr));

            return lst;
        }
    }
}