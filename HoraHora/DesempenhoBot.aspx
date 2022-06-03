<%@ Page Title="Desempenho - BOT" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DesempenhoBot.aspx.cs" Inherits="HoraHora.DesempenhoBot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos">
                <div class="blocoGrupoCampos">
                    <div class="blocoeditor" style="border-color: #000000; background-color: #000000;
                        width: 623px; height: 48px">
                        <div style="margin-left: 255px">
                            <asp:Label ID="lbEficiencia" runat="server" Text="EFICIÊNCIA" Font-Bold="true" ForeColor="White"
                                Font-Size="14"></asp:Label>
                        </div>
                        <div style="margin-left: 252px">
                            <asp:Label ID="lbEfficiency" runat="server" Text="EFFICIENCY" Font-Bold="true" ForeColor="White"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                    <div class="blocoeditor" style="border-color: #000000; background-color: #000000;
                        width: 239px; height: 48px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbModelo" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>
                    <%-- <div style="margin-left: 600px">
                        <asp:Label ID="lbEfic" runat="server" Text="Eficiência" Font-Bold="true" ForeColor="Black"
                            Font-Size="22"></asp:Label>
                        <div style="margin-left: 20px">
                            <asp:Label ID="lbEfficiency" runat="server" Text="(Efficiency)" Font-Bold="false"
                                ForeColor="Black" Font-Size="14"></asp:Label>
                        </div>
                    </div>--%>
                </div>
                <div class="blocoGrupoCampos">
                    <div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                        width: 160px; height: 25px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbData" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                    <%--<div class="blocoeditor" style="border-color: #4b6c9e; background-color: #4b6c9e;
                        width: 130px; height: 45px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbTurno" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="22"></asp:Label>
                        </div>
                    </div>--%>
                    <div id="divLado" class="blocoeditor" runat="server" visible="true" style="border-color: #4b6c9e;
                        background-color: #4b6c9e; width: 100px; height: 25px">
                        <div style="margin-left: 10px">
                            <asp:Label ID="lbLado" runat="server" Text="SMT1" Font-Bold="true" ForeColor="White"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                    <div class="blocoeditor" style="border-color: #000000; background-color: #000000;
                        width: 100px; height: 25px">
                        <div style="margin-left: 20px">
                            <asp:Label ID="lbLine" runat="server" Text="-" Font-Bold="true" ForeColor="White"
                                Font-Size="14"></asp:Label>
                        </div>
                    </div>
                    <%--    <div class="blocoeditor">
                        <div id="divCirculo" class="circulo" runat="server" style="margin-left: 10px; border-color: #00FF00;
                            background-color: #00FF00; width: 115px; height: 115px; position: relative; left: 69%;
                            top: 10px; margin-left: 10px; margin-top: -77px;">
                          
                            <div class="blocoeditor" style="margin-left: 10px; margin-top: 30px;">
                                <asp:Label ID="lbPorcentagem" runat="server" Text="0%" Font-Bold="true" ForeColor="Black"
                                    Font-Size="20"></asp:Label>
                            </div>
                        </div>
                    </div>--%>
                    <div class="blocoeditor" style="margin-left: 10px">
                        <asp:GridView ID="gridHoraHora" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black"
                            GridLines="Vertical" Width="886px">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:BoundField DataField="hourperiod" HeaderText="HORA">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="planejado" HeaderText="PLANEJADO  PLAN">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="realizado" HeaderText="REALIZADO  DONE">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="True" />
                                </asp:BoundField>
                                <%--   <asp:BoundField DataField="defeito" HeaderText="FALHAS     -       FAILS">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="True" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="performance" HeaderText="EFICIÊNCIA EFFICIENCY">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="observacao" HeaderText="OBSERVAÇÕES -  NOTES">
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <label>
                                    Sem Registros.</label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                        <div class="blocoeditor" style="margin-top: 1px; margin-left: 0px; border-color: #4b6c9e;
                            background-color: #4b6c9e; width: 87px; height: 25px">
                            <div style="margin-left: 5px">
                                <asp:Label ID="lbTotal" runat="server" Text="TOTAL" Font-Bold="true" ForeColor="White"
                                    Font-Size="12"></asp:Label>
                            </div>
                        </div>
                        <div class="blocoeditor" style="margin-top: 1px; margin-left: -8px; border-color: #4b6c9e;
                            background-color: #4b6c9e; width: 85px; height: 25px">
                            <div style="margin-left: 5px">
                                <asp:Label ID="lbPlanejado" runat="server" Text="0" Font-Bold="true" ForeColor="White"
                                    Font-Size="12"></asp:Label>
                            </div>
                        </div>
                        <div class="blocoeditor" style="margin-top: 1px; margin-left: -8px; border-color: #4b6c9e;
                            background-color: #4b6c9e; width: 85px; height: 25px">
                            <div style="margin-left: 5px">
                                <asp:Label ID="lbRealizado" runat="server" Text="0" Font-Bold="true" ForeColor="White"
                                    Font-Size="12"></asp:Label>
                            </div>
                        </div>
                        <%-- <div class="blocoeditor" style="margin-top: 1px; margin-left: -8px; border-color: #4b6c9e;
                            background-color: #4b6c9e; width: 85px; height: 25px">
                            <div style="margin-left: 5px">
                                <asp:Label ID="lbFalha" runat="server" Text="0" Font-Bold="true" ForeColor="White"
                                    Font-Size="12"></asp:Label>
                            </div>
                        </div>--%>
                        <div id="diPerformace" runat="server" class="blocoeditor" style="margin-top: 1px; margin-left: -8px;
                            border-color: #4b6c9e; background-color: #4b6c9e; width: 86px; height: 25px">
                            <div style="margin-left: 5px">
                                <asp:Label ID="lbPerformance" runat="server" Text="0%" Font-Bold="true" ForeColor="White"
                                    Font-Size="12"></asp:Label>
                            </div>
                        </div>
                        <div class="blocoeditor" style="margin-top: 1px; margin-left: -8px; border-color: #4b6c9e;
                            background-color: #4b6c9e; width: 535px; height: 25px">
                        </div>
                        <div style="margin-left: 635px;">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagens/LogoFoxconn.png" />
                        </div>
                        <asp:Label ID="lbContagem" runat="server" Text="0" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
