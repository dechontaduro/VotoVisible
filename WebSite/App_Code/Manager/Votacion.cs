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
                @"SELECT votaId, votaCorporacion, votaTitulo, votaNumero, votaAnio, votaURL
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
                @"SELECT TOP (@top) votaId, votaCorporacion, votaTitulo, votaNumero, votaAnio, votaURL
	                , twitterAccount, tweetId
	                , votaCreado, votaFinalizado
                FROM votacion
                ORDER BY votaCreado";

            GenericProvider gp = new GenericProvider("default");
            DataTable dt = gp.GetTable(sql, CommandType.Text, gp.GetDBParameter("@top", records));

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
                    0 votaId
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
    }
}