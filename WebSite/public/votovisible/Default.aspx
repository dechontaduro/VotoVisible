<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="public_votovisible_Default" %>
<html lang="en">
  <head>
    <!-- #include file ="segmentos/head.html" -->
    <title>VotoVisible.Org</title>
	<style>
		.btn-votacion { text-align: left;}
	</style>
  </head>

  <body>
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
            <p>¡Es el tiempo de que nuestros representantes nos justifiquen c&oacute;mo votan!<br />
				<a href="#" class="btn btn-lg btn-info" role="button">iPhone &raquo;</a>
				<a href="#" class="btn btn-lg btn-info" role="button">Android &raquo;</a>
			</p>
          </div>
		  <div class="row">
              <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ( votoAleatorio.tweetId ) %>"></iframe>
		  </div>
		  <div class="row">
              <ul>
			  <li>Estrategia de comunicaciòn con sitios clave</li>
			  </ul>
		  </div>
          <div class="row">
            <div class="col-6 col-sm-6 col-lg-4">
              <h2>Votaciones</h2>
			  <p>Ac&aacute; encontrar&aacute;s infograf&iacute;as sobre las votaciones creadas</p>
                <asp:Repeater id="Repeater1" runat="server" DataSource="<%# votacionesUltimas %>">
                <ItemTemplate>
                    <p><a href="votacion.aspx?id=<%# com.VotoVisible.Utils.Conversion.Base36Encode((ulong)((com.VotoVisible.Entitity.Votacion)Container.DataItem).id)%>" 
                        class="btn btn-votacion" role="button"><b><%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).corporacion%></b> <br /> <%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).titulo%> &raquo;</a></p>
                </ItemTemplate>
              </asp:Repeater>
                <p><a href="votaciones.aspx" class="btn btn-default" role="button">Verlas todas &raquo;</a></p>
                </div><!--/span-->
            <div class="col-6 col-sm-6 col-lg-4">
              <h2>Nueva votaci&oacute;n</h2>
              <p>Puedes crear una nueva votaci&oacute;n y estar enterado de todo lo que sucede alrededor de esta</p>
              <p><a href="votacionnueva.aspx" class="btn btn-default" role="button">Crear una nueva&raquo;</a></p>
            </div><!--/span-->
            <div class="col-6 col-sm-6 col-lg-4">
              <h2>Manifiesto</h2>
              <p>La transparencia de nuestros representantes y facilitarles rendirnos cuentas es...</p>
              <p><a href="manifiesto.aspx" class="btn btn-default" role="button">Ver el manifiesto &raquo;</a></p>
            </div><!--/span-->
          </div><!--/row-->
        </div><!--/span-->

        <div class="col-xs-6 col-sm-3 sidebar-offcanvas" id="sidebar" role="navigation">
          <div class="list-group">
		        <a href="votaciones.aspx" class="list-group-item active">Corporaciones en forma de nube</a>
              <asp:Repeater id="rptCorporaciones" runat="server" DataSource="<%# corporaciones %>">
                <ItemTemplate>
                    <a href="votaciones.aspx?corp=<%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).corporacion%>" class="list-group-item"><%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).corporacion%></a>
                </ItemTemplate>
              </asp:Repeater>
          </div>
          <div>
			<ul>
			<li>TIMELINE DE Tweets</li>
            <li>Tweets</li>
			<li>Tweets</li>
			<li>Tweets</li>
			</ul>
          </div>
        </div><!--/span-->
      </div><!--/row-->

      <!-- #include file ="segmentos/footer.html" -->

    </div><!--/.container-->
    <!-- #include file ="segmentos/footerscripts.html" -->
    </form>
</body>
</html>
