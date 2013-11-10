<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infografia.aspx.cs" Inherits="infografia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        iframe {
            margin: 0;
            padding: 0;
            border: none;
            width: 400px;
            height: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enableviewstate="True">
        <h1>VotoVisible.org</h1>
        <asp:DropDownList ID="ddlVotacion" runat="server" AutoPostBack="True" DataSourceID="odsVotacion" DataTextField="Corporacion" DataValueField="Id" AppendDataBoundItems="true">
            <asp:ListItem Value="">Votación...</asp:ListItem>
        </asp:DropDownList>
        <asp:ObjectDataSource ID="odsVotacion" runat="server" SelectMethod="getAll" TypeName="com.VotoVisible.Manager.Votacion"></asp:ObjectDataSource>
        <asp:Panel ID="pnlInfografia" runat="server"  ClientIDMode="Static">
            <asp:Panel ID="pnlVotacion" runat="server" ClientIDMode="Static">
                <h1>Votacion</h1>
                <span>Corporacion:<asp:Label ID="lblCorporacion" runat="server" Text="<%# ( votacion.corporacion ) %>"></asp:Label></span>
                <span>Proyecto:<asp:Label ID="lblTitulo" runat="server" Text="<%# ( votacion.titulo) %>"></asp:Label></span><br />
                <span>Número:<asp:Label ID="lblNumero" runat="server" Text="<%# ( votacion.numero) %>"></asp:Label></span>
                <span>Año:<asp:Label ID="lblAnio" runat="server" Text="<%# ( votacion.anio ) %>"></asp:Label></span>
            </asp:Panel>
            <hr />

            <asp:Panel ID="pnlTotales" runat="server" ClientIDMode="Static">
                <h1>Resultados</h1>
                <table>
                    <tr><td></td><td>Publicos</td><td>Privados</td><td>Total</td></tr>
                <asp:Repeater id="rptTotales" runat="server" DataSource="<%# votacion.totales %>">
                    <ItemTemplate>
                        <tr>
                            <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).decision%></td>
                            <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos%></td>
                            <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados%></td>
                            <td><%# ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPublicos
                                + ((com.VotoVisible.Entitity.VotacionTotales)Container.DataItem).votosPrivados
                                %></td>
                            </tr>
                    </ItemTemplate>
              </asp:Repeater>
                </table>
            </asp:Panel>
            <hr />

            <asp:Panel ID="pnlMostRetweet" runat="server" ClientIDMode="Static">
                 <h1>Más retuiteado</h1>
                <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ( votacion.mostRetweet.tweetId ) %>"></iframe>
            </asp:Panel>
            <hr />

            <asp:Panel ID="pnlMostReply" runat="server" ClientIDMode="Static">
                 <h1>Más respondido</h1>
                <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ( votacion.mostReply.tweetId ) %>"></iframe>
            </asp:Panel>
            <hr />

            <asp:Panel ID="pnlVotos" runat="server" ClientIDMode="Static">
                <h1>Votos</h1>
                <asp:Repeater id="rptVotos" runat="server" DataSource="<%# votacion.votos %>">
                    <ItemTemplate>
                        @<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).twitterAccount%>:
                        <%# (((com.VotoVisible.Entitity.Voto)Container.DataItem).tipo == 0)?((com.VotoVisible.Entitity.Voto)Container.DataItem).decision : "Privado" %><br />
                        (<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).realizado%>)
                        [<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).retweets%>/
                        <%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).replies%>]<br />
                        <%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).comentario%>
                        <iframe src="http://jimwalsh.name/demos/oembed/oembed.php?id=<%# ((com.VotoVisible.Entitity.Voto)Container.DataItem).tweetId%>"></iframe>
                        <hr />
                    </ItemTemplate>
              </asp:Repeater>
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
