<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="ZoekPersonen.aspx.cs" Inherits="IventWeb.ToegangscontroleInhoud.ZoekPersonen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="Inchecken.aspx">Inchecken</a></li>
            <li><a href="ZoekPersonen.aspx">Zoek personen</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="lblZoekCriteria" runat="server" Text="Zoekcriteria: "></asp:Label>
    <asp:TextBox ID="Tbsearch" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="search" /><br /><br />
    <asp:Label ID="lblAfwezig" runat="server" Text="Afwezige bezoekers: "></asp:Label><br />
    <asp:ListBox ID="lbnietaanwezig" runat="server" Height="96px" Width="203px"></asp:ListBox><br /><br />
    <asp:Label ID="lblAanwezig" runat="server" Text="Aanwezige bezoekers: "></asp:Label><br />
    <asp:ListBox ID="lbaanwezig" runat="server" Height="93px" Width="203px"></asp:ListBox>
</asp:Content>
