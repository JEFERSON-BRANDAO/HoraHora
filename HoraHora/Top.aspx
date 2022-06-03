<%@ Page Title="SMT2 - TOP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Top.aspx.cs" Inherits="HoraHora.Top" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 480px; height: 65px">
                    <div style="margin-left: 50px">
                        <asp:Label ID="lbControle" runat="server" Text="CONTROLE DE PRODUÇÃO" Font-Bold="true"
                            ForeColor="White" Font-Size="22"></asp:Label>
                    </div>
                    <div style="margin-left: 160px">
                        <asp:Label ID="lbControl" runat="server" Text="Production control" Font-Bold="true"
                            ForeColor="White" Font-Size="14"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 115px; height: 65px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbEtapa" runat="server" Text="ETAPA:" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbStage" runat="server" Text="(STAGE)" Font-Bold="True" ForeColor="White"
                            Font-Size="14"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: Gray; background-color: Gray; width: 254px;
                    height: 65px">
                    <div style="margin-left: 10px;">
                        <asp:Label ID="lbProduto" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 120px; height: 45px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbHora" runat="server" Text="HORA:" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 220px; height: 45px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbHoraDeAte" runat="server" Text="- às -" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 189px; height: 45px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbModelo" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                    width: 130px; height: 45px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbLinha" runat="server" Text="LINHA" Font-Bold="true" ForeColor="White"
                            Font-Size="14"></asp:Label>
                    </div>
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbLine" runat="server" Text="(LINE)" Font-Bold="true" ForeColor="White"
                            Font-Size="10"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor" style="border-color: Gray; background-color: Gray; width: 150px;
                    height: 45px">
                    <div style="margin-left: 30px">
                        <asp:Label ID="lbLado" runat="server" Text="SMT2" Font-Bold="true" ForeColor="White"
                            Font-Size="22"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor">
                    <div style="margin-left: 590px">
                        <asp:Label ID="lbMetaYield" runat="server" Text="META FPY FE  - 99,6%" Font-Bold="true"
                            ForeColor="Black" Font-Size="22"></asp:Label>
                        <br />
                        <asp:Label ID="lbMeta" runat="server" Text="(Target Yield)" Font-Bold="true" ForeColor="Black"
                            Font-Size="14"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="border-color: #FFFF00; background-color: #FFFF00;
                    width: 200px; height: 65px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbPlanejado" runat="server" Text="PLANEJADO:" Font-Bold="true" ForeColor="Black"
                            Font-Size="22"></asp:Label>
                        <br />
                        <asp:Label ID="lbPlan" runat="server" Text="PLAN" Font-Bold="true" ForeColor="Black"
                            Font-Size="14"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor">
                    <div class="circulo" style="margin-left: 10px; border-color: #FFFF00; background-color: #FFFF00;
                        width: 60px; height: 60px">
                        <br />
                        <div style="margin-left: 13px;">
                            <asp:Label ID="lbCirculoPlanejado" runat="server" Text="0" Font-Bold="true" ForeColor="Black"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="border-color: #00FF00; background-color: #00FF00;
                    width: 200px; height: 65px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbRealizado" runat="server" Text="REALIZADO:" Font-Bold="true" ForeColor="Black"
                            Font-Size="22"></asp:Label>
                        <br />
                        <asp:Label ID="lbDone" runat="server" Text="DONE" Font-Bold="true" ForeColor="Black"
                            Font-Size="14"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor">
                    <div class="circulo" style="margin-left: 10px; border-color: #00FF00; background-color: #00FF00;
                        width: 60px; height: 60px">
                        <br />
                        <div style="margin-left: 13px;">
                            <asp:Label ID="lbCirculoRalizado" runat="server" Text="0" Font-Bold="true" ForeColor="Black"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="blocoGrupoCampos">
                <div class="blocoeditor" style="border-color: #FF0000; background-color: #FF0000;
                    width: 200px; height: 65px">
                    <div style="margin-left: 10px">
                        <asp:Label ID="lbDefeito" runat="server" Text="DEFEITO:" Font-Bold="true" ForeColor="Black"
                            Font-Size="22"></asp:Label>
                        <br />
                        <asp:Label ID="lbFail" runat="server" Text="FAIL" Font-Bold="true" ForeColor="Black"
                            Font-Size="14"></asp:Label>
                    </div>
                </div>
                <div class="blocoeditor">
                    <div class="circulo" style="margin-left: 10px; border-color: #FF0000; background-color: #FF0000;
                        width: 60px; height: 60px">
                        <br />
                        <div style="margin-left: 13px;">
                            <asp:Label ID="lbCirculoDefeito" runat="server" Text="0" Font-Bold="true" ForeColor="Black"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="blocoGrupoCampos">
                    <div class="blocoeditor">
                        <div style="margin-left: 0px;">
                            <asp:Label ID="lbMissao" runat="server" Text="Missão:" Font-Bold="true" ForeColor="#0000CD"
                                Font-Size="14"></asp:Label>
                        </div>
                        <div style="margin-left: 0px; width: 450px;">
                            <asp:Label ID="lbTexto" runat="server" Text="Oferecer produtos e serviços com qualidade, satisfazendo às necessidades de nossos clientes.
" Font-Bold="true" ForeColor="Black" Font-Size="14"></asp:Label>
                        </div>
                        <div style="margin-left: 600px; position: relative; margin-top: -50px;">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagens/LogoFoxconn.png" />
                        </div>
                    </div>
                </div>
                <div class="blocoeditor">
                    <div id="divCirculo" runat="server" class="circulo" style="margin-left: 10px; border-color: #00FF00;
                        background-color: #00FF00; width: 200px; height: 200px; position: relative; left: 300%;
                        top: 10%; margin-left: 10px; margin-top: -350px;">
                        <br />
                        <div class="DivPosicaoTextoCentro">
                            <asp:Label ID="lbPorcentagem" runat="server" Text="0%" Font-Bold="true" ForeColor="Black"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>
                </div>
                <asp:Label ID="lbContagem" runat="server" Text="0" Visible="false"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
  <%--  <asp:Timer ID="Timer1" runat="server" Interval="1000">
    </asp:Timer>--%>
</asp:Content>
