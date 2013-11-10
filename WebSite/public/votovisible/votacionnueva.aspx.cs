using System;
using System.Web;

public partial class public_votovisible_votacionnueva : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnQR_Click(object sender, EventArgs e)
    {
        com.VotoVisible.Entitity.Votacion votacion = new com.VotoVisible.Entitity.Votacion();
        votacion.corporacion = corporacion.Text;
        votacion.titulo = proyecto.Text;
        votacion.numero = numero.Text;
        votacion.anio = anio.Text;
        votacion.url = url.Text;
        votacion.creado = DateTime.Today;
        votacion.finalizado = com.VotoVisible.Utils.Conversion.Str2DateTime(fin.Text+" "+finhora.Text, "dd/MM/yyyy HH:mm");

        votacion.tipo = (publica.Checked) ? 0 : 1;

        string id = votid.Text;
        if (id == "")
        {
            id = com.VotoVisible.Manager.Votacion.add(votacion);
            votid.Text = id;
        }

        votacion.id = int.Parse(id);

        if (votacion.tipo == 0)
        {
            com.VotoVisible.Manager.Voto.delete(votacion.id, true);
            string[] avotantes = votantes.Text.Split(',');
            foreach (string votante in avotantes)
            {
                com.VotoVisible.Entitity.Voto voto = new com.VotoVisible.Entitity.Voto();
                voto.votacionId = votacion.id;
                voto.twitterAccount = votante;
                voto.creado = DateTime.Today;
                com.VotoVisible.Manager.Voto.add(voto);
            }
        }
    }
}