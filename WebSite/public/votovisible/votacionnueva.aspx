<%@ Page Language="C#" AutoEventWireup="true" CodeFile="votacionnueva.aspx.cs" Inherits="public_votovisible_votacionnueva" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <!-- #include file ="segmentos/head.html" -->
    <title>VotoVisible.Org - Nueva Votaci&oacute;n</title>
  </head>
<body>
    <form id="form1" runat="server">
    <!-- #include file ="segmentos/navigation.html" -->

    <div class="container">
      <div class="row row-offcanvas row-offcanvas-right">
        <div class="col-xs-12 col-sm-9">
          <p class="pull-right visible-xs">
            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"></button>
          </p>
          <div class="jumbotron">
            <h1>VotoVisible.org</h1><h2>Nueva Votaci&oacute;n</h2>
          </div>
          <div class="row">
            <div class="col-6 col-sm-6 col-lg-4">
                <br />
              Corporaci&oacute;n
            <input type="text" id="corporacion" value="senadoco" /><br />
            Proyecto
            <input type="text" id="proyecto" value="Matrimonio Igualitario" /><br />
            N&uacute;mero
            <input type="text" id="numero" value="47" /><br />
            A&ntilde;o
            <input type="text" id="anio" value="2012" /><br />
            URL Documento
            <input type="text" id="url" value="http://www.imprenta.gov.co/gacetap/gaceta.mostrar_documento?p_tipo=05&p_numero=47&p_consec=33584" /><br />
            <a href="#" onclick="genQR();" class="btn btn-lg btn-info" role="button">Generar QR&raquo;</a>
            </div><!--/span-->
            <div class="col-6 col-sm-6 col-lg-4">
                <img id="qr" width="300" height="300" />
        </div>
		  </div>
	    </div><!--/span-->
      </div><!--/row-->
        <!-- #include file ="segmentos/footer.html" -->
        </div><!--/.container-->
        <!-- #include file ="segmentos/footerscripts.html" -->
        <script src="../js/votacionnueva.js"></script>
    </form>
</body>
</html>
