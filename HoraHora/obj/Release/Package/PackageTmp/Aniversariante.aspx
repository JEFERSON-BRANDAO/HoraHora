<%@ Page Title="Aniversariante" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Aniversariante.aspx.cs" Inherits="HoraHora.Aniversariante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 10px; width: 900px;">
                <div style="margin-left: 5px;">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/imagens/aniversariantes.png" Height="173px"
                        Width="446px" />
                    <asp:Image ID="imgBalao" runat="server" ImageUrl="~/imagens/feliz_aniversario_recados_orkut5.gif" Height="173px"
                        Width="430px" />
                </div>
                <div class="blocoGrupoCampos">
                    <div class="blocoGrupoCampos">
                        <div id="divListaMeio" runat="server" visible="false" class="blocoeditor">
                            <div style="margin-left: 100px; position: relative; left: 5%; margin-left: 100px;
                                margin-top: -80px;">
                                <div class="DivPosicaoTextoCentro">
                                    <asp:Label ID="lbListaMeio" runat="server" Font-Bold="true" ForeColor="#FF6600" Font-Size="XX-Large"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divListaEsquerda" runat="server" visible="false" class="blocoeditor">
                        <div style="margin-left: -5px; position: relative; left: 0%; margin-left: -5px; margin-top: -40px;">
                            <div class="DivPosicaoTextoCentro">
                                <asp:Label ID="lbListaEsquerda" runat="server" Font-Bold="True" ForeColor="#FF6600"
                                    Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="divListaDireita" runat="server" visible="false" class="blocoeditor">
                        <div style="margin-left: -5px; position: relative; left: 30%; margin-left: -5px;
                            margin-top: -40px;">
                            <div class="DivPosicaoTextoCentro">
                                <asp:Label ID="lbListaDireita" runat="server" Font-Bold="True" ForeColor="#FF6600"
                                    Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="blocoGrupoCampos">
                    <asp:Label ID="lbInformacao" runat="server" Text="O time Foxconn lhes deseja muitas felicidades, vocês fazem parte e são muito importantes para esse time. Parabéns!
" Font-Size="Large" ForeColor="#000099" Font-Bold="True"></asp:Label>
                </div>
            </div>
            <asp:Label ID="lbContagem" runat="server" Text="0" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
