<%@ Page Title="Grafico - Fe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Fe.aspx.cs" Inherits="HoraHora.graficos.Fe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ WebHandler Language="C#" class="exibi_imagem" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos">
                <div class="blocoGrupoCampos">
                    <div class="blocoeditor" style="border-color: #000000; background-color: #000000;
                        width: 652px; height: 48px">
                        <div style="margin-left: 255px">
                            <asp:Label ID="lbYiledRate" runat="server" Text="YIELD RATE" Font-Bold="true" ForeColor="White"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>
                    <div class="blocoeditor" style="border-color: #000000; background-color: #000000;
                        width: 190px; height: 48px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbPartNumberDesc" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="blocoGrupoCampos">
                    <%--<div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                        width: 130px; height: 45px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbTurno" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>--%>
                    <div class="blocoGrupoCampos">
                        <asp:Image ID="imgGrafico" runat="server" Width="886px" />
                    </div>
                </div>
                <div style="margin-left: 610px;">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagens/LogoFoxconn.png" />
                </div>
            </div>
            <asp:Label ID="lbContagem" runat="server" Text="0" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
