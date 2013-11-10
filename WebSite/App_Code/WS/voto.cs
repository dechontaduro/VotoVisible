using System;
using System.Web.Services;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de voto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class voto : System.Web.Services.WebService {

    public voto () {

    }

    [WebMethod]
    public string put(
            string tweeterAccount, string votacionId
            , string corporacion, string titulo, string url, string numero, string anio
            , string tweedId, string tipo, string decision, string comentario)
    {
        com.VotoVisible.Entitity.Voto voto;
        int ivotacionId = (int)com.VotoVisible.Utils.Conversion.Base36Decode(votacionId);
        //TODO: verificar que el voto esté de acuerdo con el tipo de votación
        List<com.VotoVisible.Entitity.Voto> busqueda = com.VotoVisible.Manager.Voto.search(tweeterAccount, ivotacionId.ToString(), null, null, null);
        if (busqueda.Count == 1)
            voto = busqueda[0];
        else
        {
            voto = new com.VotoVisible.Entitity.Voto();
            voto.votacionId = ivotacionId;
            voto.twitterAccount = tweeterAccount;
        }

        voto.tweetId = tweedId;
        voto.tipo = int.Parse(tipo);
        voto.tweetId = tweedId;
        voto.decision = decision;
        voto.comentario = comentario;
        voto.realizado = DateTime.Today;

        string id = "";
        if(voto.id == null)
            id = com.VotoVisible.Manager.Voto.add(voto);
        else
            id = com.VotoVisible.Manager.Voto.update(voto);

        return id;
    }
    
}
