using System;
using System.Web;
using log4net;

public partial class public_votovisible_votacion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public com.VotoVisible.Entitity.Votacion votacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        string votacionId = null;
        try
        {
            votacionId = getVotacionIdRequest();
            votacion = bindVotacion(votacionId);
            Page.DataBind();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled) log.Error(String.Format("VotaId:", votacionId), ex);
        }
    }

    protected com.VotoVisible.Entitity.Votacion bindVotacion(string votacionId)
    {
        com.VotoVisible.Entitity.Votacion obj = com.VotoVisible.Manager.Votacion.getById(votacionId);
        obj.votos = com.VotoVisible.Manager.Voto.getByVotacion(votacionId);
        obj.mostReply = com.VotoVisible.Manager.Voto.getMostReplyByVotacion(votacionId);
        obj.mostRetweet = com.VotoVisible.Manager.Voto.getMostRetweetByVotacion(votacionId);
        obj.totales = com.VotoVisible.Manager.Voto.getTotalesByVotacion(votacionId);
        return obj;
    }

    protected string getVotacionIdRequest()
    {
        string reqId = Request["id"];

        if (log.IsInfoEnabled) log.InfoFormat("VotaId:", reqId);
        
        long lid = com.VotoVisible.Utils.Conversion.Base36Decode(reqId);
        string id = lid.ToString();
        return id;
    }
}