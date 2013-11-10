<%@ Page Language="C#" AutoEventWireup="true" CodeFile="votaciones.aspx.cs" Inherits="public_votovisible_votaciones" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <!-- #include file ="segmentos/head.html" -->
    <title>VotoVisible.Org - Votaciones</title>
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
            <h2>Votaciones</h2>
          </div>
		  <div class="row">
              <ul>
			  <li>todas o filtradas por corporaciòn</li>
			  <li>Solo aparece si se ha hecho 1 voto</li>
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
