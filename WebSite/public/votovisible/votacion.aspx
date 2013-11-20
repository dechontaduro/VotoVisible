<%@ Page Language="C#" AutoEventWireup="true" CodeFile="votacion.aspx.cs" Inherits="public_votovisible_votacion" %>
<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <!-- #include file ="segmentos/head.html" -->
    <title>VotoVisible.Org - Votaci&oacute;n Infograf&iacute;a</title>
	<style> iframe { margin: 0; padding: 0; border: none; width: 400px; height: 300px; } </style>

      <script type="text/javascript">
        var resultados;
        function loadData(){
          resultados = [
           <asp:Repeater id="Repeater1" runat="server" DataSource="<%# votacion.totales %>">
            <ItemTemplate>
                {'decision': '<%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).decision%>',
                 'publicos': <%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos%>,
                 'privados': <%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados%>,
                 'total': <%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos
                                + ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados%>
          },
            </ItemTemplate>
        </asp:Repeater>
            {'decision':'Total', 'publicos':0, 'privados':0,'total':0}];

            resultados.splice(resultados.length - 1, 1);
          }
    </script>
  </head>

  <body onload="loadData();drawResultados();">
    <form id="form1" runat="server">
    <!-- #include file ="segmentos/navigation.html" -->

    <div class="container">
      <div class="row row-offcanvas row-offcanvas-right">

        <div class="col-xs-12 col-sm-9">
          <p class="pull-right visible-xs">
            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">Toggle nav</button>
          </p>
          <div class="jumbotron">
            <h1>VotoVisible.org</h1>
            <h2><%# ( votacion.corporacion ) %> - <%# ( votacion.titulo) %><br />
                <small><%# ( votacion.numero) %>-<%# ( votacion.anio ) %> (<%# ( votacion.finalizado ) %>)</small> </h2>
          </div>
            <div class="row">
                <div class="col-6 col-sm-6 col-lg-4">
                  <h2>Resultados</h2>
                    <div id="svgResultados"></div ><br /><br /><br /><br />
                    <table border="1">
                        <tr><td></td><td>Publicos</td><td>Privados</td><td>Total</td></tr>
                        <asp:Repeater id="rptTotales" runat="server" DataSource="<%# votacion.totales %>">
                        <ItemTemplate>
                            <tr>
                                <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).decision%></td>
                                <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos%></td>
                                <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados%></td>
                                <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos
                                    + ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados%></td>
                                </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
              </div><!--/span-->
              <div class="col-6 col-sm-6 col-lg-4">
                <h2>+Retweet</h2>
                <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ( votacion.mostRetweet.tweetId ) %>"></iframe>
              </div><!--/span-->
              <div class="col-6 col-sm-6 col-lg-4">
                <h2>+Comentado</h2>
                <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ( votacion.mostReply.tweetId ) %>"></iframe>
              </div><!--/span-->
		  </div>
	    </div><!--/span-->
          <div class="col-xs-6 col-sm-3 sidebar-offcanvas" id="votos">
              <h2>Votos</h2>
              <asp:Repeater id="rptVotos" runat="server" DataSource="<%# votacion.votos %>">
                    <ItemTemplate>
                        @<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).twitterAccount%>:
                        <%# (((com.VotoVisible.Entitity.Voto)Container.DataItem).tipo == 0)?((com.VotoVisible.Entitity.Voto)Container.DataItem).decision : "Privado" %><br />
                        (<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).realizado%>)
                        [<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).retweets%>/
                        <%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).replies%>]<br />
                        <%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).comentario%>
                        <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).tweetId%>"></iframe>
                    </ItemTemplate>
              </asp:Repeater>

              <!-- #include file ="segmentos/tlVo_aV.html" -->
			    
          </div>
      </div><!--/row-->

      <!-- #include file ="segmentos/footer.html" -->

    </div><!--/.container-->

    <!-- #include file ="segmentos/footerscripts.html" -->
    <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>
    <script src="http://d3js.org/d3.v3.min.js" charset="utf-8"></script>
    <script src="../js/votacion.js"></script>
    </form>
</body>
</html>
