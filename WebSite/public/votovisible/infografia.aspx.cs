using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class infografia : System.Web.UI.Page
{
    public com.VotoVisible.Entitity.Votacion votacion;
    protected void Page_Load(object sender, EventArgs e)
    {

        string votacionId = "";

        /*
        if ()
        {
            phInfografia.Visible = false;
            phVotacion.Visible = false;
            return;
        }
        */
        votacionId = ddlVotacion.SelectedValue;
        if (!IsPostBack || votacionId == "")
        {
            pnlInfografia.Visible = false;
            pnlVotacion.Visible = false;
            return;
        }
       
        votacion = bindVotacion(votacionId);
        pnlInfografia.Visible = true;
        pnlVotacion.Visible = true;

        //ddlVotacion.AppendDataBoundItems = false;
        Page.DataBind();
        //drawInfografia(votacion);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
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

    protected void drawInfografia(com.VotoVisible.Entitity.Votacion votacion)
    {
        //lblCorporacion.Text = votacion.Corporacion;
    }
}