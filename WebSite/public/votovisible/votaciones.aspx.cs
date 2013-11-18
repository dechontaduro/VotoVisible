using System;
using System.Web;
using System.Collections.Generic;
using log4net;

public partial class public_votovisible_votaciones : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public List<com.VotoVisible.Entitity.Votacion> votaciones;
    public string corporacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            corporacion = Request["corp"];

            if (log.IsInfoEnabled) log.InfoFormat("Corp:", corporacion);

            if (corporacion == null || corporacion == "")
                votaciones = com.VotoVisible.Manager.Votacion.getAll(100);
            else
                votaciones = com.VotoVisible.Manager.Votacion.getByCorporacion(100, corporacion);
            Page.DataBind();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled) log.Error("main", ex);
        }

    }
}