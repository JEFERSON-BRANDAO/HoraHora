<%@ Page Title="Default" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HoraHora.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </Triggers>
        <ContentTemplate>
            <div class="blocoGrupoCampos">
                <div class="DivPosicaoTextoCentro">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagens/loading.gif" />
                    <br />
                    <asp:Label ID="lbTimer" runat="server" Font-Bold="true" Font-Size="22" Text="0"></asp:Label>
                </div>
                <%-- <div class="DivPosicaoTextoCentro">
                    <asp:Label ID="lbTimer" runat="server" Font-Bold="true" Font-Size="22" Text="0"></asp:Label>
                </div>--%>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server" Interval="1000">
    </asp:Timer>
</asp:Content>
