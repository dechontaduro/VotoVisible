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
                        <h1>VotoVisible.org</h1>
                        <h2>Nueva Votaci&oacute;n</h2>
                    </div>
                    <div class="row">
                        <div class="col-6 col-sm-6 col-lg-4">
                            <br />
                            Corporaci&oacute;n:<asp:TextBox ID="corporacion" runat="server" Text="senadoco"></asp:TextBox><br />
                            Proyecto:
                            <asp:TextBox ID="proyecto" Text="Matrimonio Igualitario" runat="server"></asp:TextBox><br />
                            N&uacute;mero:<asp:TextBox ID="numero" Text="47" runat="server"></asp:TextBox><br />
                            A&ntilde;o:<asp:TextBox ID="anio" runat="server" Text="2012"></asp:TextBox><br />
                            URL Documento:<asp:TextBox ID="url" Text="http://www.imprenta.gov.co/gacetap/gaceta.mostrar_documento?p_tipo=05&p_numero=47&p_consec=33584" runat="server"></asp:TextBox><br />
                            Fecha Fin:<asp:TextBox ID="fin" Text="10/11/2013" runat="server"></asp:TextBox><br />
                            Hora Fin:<asp:TextBox ID="finhora" Text="11:00" runat="server"></asp:TextBox><br />
                            P&uacute;blica:<asp:CheckBox ID="publica" Checked="true" runat="server" /><br />
                            Votantes:<asp:TextBox ID="votantes" Text="dechontaduro,votovisible" runat="server"></asp:TextBox><br />
                            Id:<asp:TextBox ID="votid" Text="" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="btnQR" CssClass="btn btn-lg btn-info" runat="server" OnClick="btnQR_Click">Generar QR&raquo;</asp:LinkButton>
                        </div>
                        <!--/span-->
                        <div class="col-6 col-sm-6 col-lg-4">
                            <img id="qr" width="300" height="300" />
                        </div>
                    </div>
                </div>
                <!--/span-->
            </div>
            <!--/row-->
            <!-- #include file ="segmentos/footer.html" -->
        </div>
        <!--/.container-->
        <!-- #include file ="segmentos/footerscripts.html" -->
        <script src="../js/votacionnueva.js"></script>
    </form>
</body>
</html>
