﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="votaciones.aspx.cs" Inherits="public_votovisible_votaciones" %>
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
                        <h2>Votaciones <%# corporacion %></h2>
                    </div>
                </div><!--/cabecera-->
                <div class="row">
                    <div class="col-6 col-sm-6 col-lg-4">
                        <asp:Repeater id="rptVotaciones" runat="server" DataSource="<%# votaciones %>">
                            <ItemTemplate>
                            <p><a href="votacion.aspx?id=<%# com.VotoVisible.Utils.Conversion.Base36Encode((ulong)((com.VotoVisible.Entitity.Votacion)Container.DataItem).id)%>" 
                                class="btn btn-votacion" role="button">
                                <b><%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).corporacion%></b>
                                <br /> 
                                <%# ((com.VotoVisible.Entitity.Votacion)Container.DataItem).titulo%> &raquo;</a>

                            </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div><!--/columna central-->
            <!-- #include file ="segmentos/footer.html" -->
        </div><!--/.container-->
    <!-- #include file ="segmentos/footerscripts.html" -->
    </form>
</body>
</html>
