using System;
using System.Web;
using System.Collections.Generic;
using log4net;

public partial class public_votovisible_Default : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public List<com.VotoVisible.Entitity.Votacion> corporaciones;
    public List<com.VotoVisible.Entitity.Votacion> votacionesUltimas;
    public com.VotoVisible.Entitity.Voto votoAleatorio;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            corporaciones = com.VotoVisible.Manager.Votacion.getCorporacionesAll();
            votoAleatorio = com.VotoVisible.Manager.Voto.getVotoAleatorio();
            votacionesUltimas = com.VotoVisible.Manager.Votacion.getAll(5);
            Page.DataBind();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled) log.Error("main", ex);
        }
    }
}