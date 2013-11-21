using System;
using System.Data;
using System.Collections.Generic;
using com.VotoVisible.Utils;

namespace com.VotoVisible.Manager
{
    /// <summary>
    /// Descripción breve de Votacion
    /// </summary>
    public class Votacion
    {
        public Votacion()
        {

        }

        private static Entitity.Votacion convert2Votacion(DataRow votacion)
        {
            com.VotoVisible.Entitity.Votacion obj = new Entitity.Votacion(
                (int)Conversion.Obj2Int(votacion["votaId"]),
                (int)Conversion.Obj2Int(votacion["votaTipo"]),
                Conversion.Obj2String(votacion["votaCorporacion"]),
                Conversion.Obj2String(votacion["votaTitulo"]),
                Conversion.Obj2String(votacion["votaNumero"]),
                Conversion.Obj2String(votacion["votaAnio"]),
                Conversion.Obj2String(votacion["votaURL"]),
                Conversion.Obj2String(votacion["twitterAccount"]),
                Conversion.Obj2String(votacion["tweetId"]),
                Conversion.Obj2DateTime(votacion["votaCreado"]),
                Conversion.Obj2DateTime(votacion["votaFinalizado"]));
            return obj;
        }

        public static Entitity.Votacion getById(string id)
        {
            string sql =
                @"SELECT votaId, votaTipo, votaCorporacion, votaTitulo, votaNumero, votaAnio, votaURL
	                , twitterAccount, tweetId
	                , votaCreado, votaFinalizado
                FROM votacion
                WHERE votaId = @id";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@id", id));

            if(dt == null || dt.Rows.Count == 0)
                return null;

            com.VotoVisible.Entitity.Votacion obj = convert2Votacion(dt.Rows[0]);
            return obj;
        }

        public static List<Entitity.Votacion> getAll(int records)
        {
            string sql =
                @"SELECT TOP (@top) votaId, votaTipo, votaCorporacion, votaTitulo, votaNumero, votaAnio, votaURL
	                , twitterAccount, tweetId
	                , votaCreado, votaFinalizado
                FROM votacion
                ORDER BY votaCreado DESC";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@top", records));

            List<Entitity.Votacion> lst = new List<Entitity.Votacion>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2Votacion(dr));

            return lst;
        }

        public static List<Entitity.Votacion> getByCorporacion(int records, string corporacion)
        {
            string sql =
                @"SELECT TOP (@top) votaId, votaTipo, votaCorporacion, votaTitulo, votaNumero, votaAnio, votaURL
	                , twitterAccount, tweetId
	                , votaCreado, votaFinalizado
                FROM votacion
                WHERE votaCorporacion = @votaCorporacion
                ORDER BY votaCreado";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text
                    , gp.GetDBParameter("@top", records)
                    , gp.GetDBParameter("@votaCorporacion", corporacion));

            List<Entitity.Votacion> lst = new List<Entitity.Votacion>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2Votacion(dr));

            return lst;
        }

        public static List<Entitity.Votacion> getCorporacionesAll()
        {
            string sql =
                @"SELECT DISTINCT 
                    0 votaId, 0 votaTipo
                    , votaCorporacion
                    , '' votaTitulo, '' votaNumero, '' votaAnio, '' votaURL
	                , '' twitterAccount, '' tweetId, null votaCreado, null votaFinalizado
                FROM votacion
                ORDER BY votaCorporacion";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text);

            List<Entitity.Votacion> lst = new List<Entitity.Votacion>();

            if (dt == null || dt.Rows.Count == 0)
                return lst;

            foreach (DataRow dr in dt.Rows)
                lst.Add(convert2Votacion(dr));

            return lst;
        }

        public static string add(Entitity.Votacion votacion)
        {
            string sql =
                @"INSERT INTO votacion
                    (votaTipo,votaCorporacion,votaTitulo,votaNumero,votaAnio,votaURL
                        ,twitterAccount,tweetId,votaCreado,votaFinalizado)
                VALUES
                    (@votaTipo,@votaCorporacion,@votaTitulo,@votaNumero,@votaAnio,@votaURL
                        ,@twitterAccount,@tweetId,@votaCreado,@votaFinalizado)

                SELECT @@IDENTITY";

            GenericProvider gp = new GenericProvider("default");
            object result = gp.GetScalar(sql, CommandType.Text
                                , gp.GetDBParameter("@votaTipo", votacion.tipo)
                                , gp.GetDBParameter("@votaCorporacion", votacion.corporacion)
                                , gp.GetDBParameter("@votaTitulo", votacion.titulo)
                                , gp.GetDBParameter("@votaNumero", votacion.numero)
                                , gp.GetDBParameter("@votaAnio", votacion.anio)
                                , gp.GetDBParameter("@votaURL", votacion.url)
                                , gp.GetDBParameter("@twitterAccount", votacion.twitterAccount)
                                , gp.GetDBParameter("@tweetId", votacion.tweetId)
                                , gp.GetDBParameter("@votaCreado", votacion.creado)
                                , gp.GetDBParameter("@votaFinalizado", votacion.finalizado));

            if (result == null)
                return "";

            return result.ToString();
        }

        public static string update(Entitity.Votacion votacion)
        {
            string sql =
                @"UPDATE votacion
                SET votaTipo = @votaTipo
                    ,votaCorporacion = @votaCorporacion
                    ,votaTitulo = @votaTitulo
                    ,votaNumero = @votaNumero
                    ,votaAnio = @votaAnio
                    ,votaURL = @votaURL
                    ,twitterAccount = @twitterAccount
                    ,tweetId = @tweetId
                    ,votaCreado = @votaCreado
                    ,votaFinalizado = @votaFinalizado
                WHERE votaId=@votaId";

            GenericProvider gp = new GenericProvider("default");
            int recs = gp.ExecuteNonQuery(sql, CommandType.Text
                                , gp.GetDBParameter("@votaId", votacion.id)
                                , gp.GetDBParameter("@votaTipo", votacion.tipo)
                                , gp.GetDBParameter("@votaCorporacion", votacion.corporacion)
                                , gp.GetDBParameter("@votaTitulo", votacion.titulo)
                                , gp.GetDBParameter("@votaNumero", votacion.numero)
                                , gp.GetDBParameter("@votaAnio", votacion.anio)
                                , gp.GetDBParameter("@votaURL", votacion.url)
                                , gp.GetDBParameter("@twitterAccount", votacion.twitterAccount)
                                , gp.GetDBParameter("@tweetId", votacion.tweetId)
                                , gp.GetDBParameter("@votaCreado", votacion.creado)
                                , gp.GetDBParameter("@votaFinalizado", votacion.finalizado));

            if (recs == 0)
                return "";

            return votacion.id.ToString();
        }
    }
}