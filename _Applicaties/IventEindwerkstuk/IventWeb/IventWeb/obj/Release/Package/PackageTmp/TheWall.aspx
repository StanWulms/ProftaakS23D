<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="TheWall.aspx.cs" Inherits="IventWeb.SMSInhoud.TheWall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="TheWall.aspx">The Wall</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="text">
        <asp:Label ID="lblNaam" runat="server" Text="Categorie: "></asp:Label>
        <asp:Label ID="lblDbNaam" runat="server" Text=""></asp:Label>
    </div>
    <div id="Mappen">
        <asp:Panel ID="pnlMappen" runat="server" Width="410px">
        </asp:Panel>
    </div>
</asp:Content>
