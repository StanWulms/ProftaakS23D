<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Betaling.aspx.cs" Inherits="IventWeb.Betaling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="Reservering.aspx">Reservering</a></li>
            <li><a href="CampeerplekZoeken.aspx">Campeerplek zoeken</a></li>
            <li><a href="Betaling.aspx">Betaling</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br /><br />
    Niet betaald&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Betaald<br />
    <asp:ListBox ID="lbNietBetaald" runat="server" Height="158px" Width="236px"></asp:ListBox>
    <asp:Label ID="lblInvis1" runat="server" Text="LABEL" Visible="False"></asp:Label>
    <asp:Button ID="btnBetaal" runat="server" Text="Betaal" Height="28px" OnClick="btnBetaal_Click" />
    <asp:Label ID="lblInvis2" runat="server" Text="LABEL" Visible="False"></asp:Label>
    <asp:ListBox ID="lbWelBetaald" runat="server" Height="158px" Width="236px"></asp:ListBox><br />
    <asp:Label ID="lblBetaalError" runat="server" Text="Selecteer een bezoeker." ForeColor="Red" Visible="False"></asp:Label>
    </asp:Content>
