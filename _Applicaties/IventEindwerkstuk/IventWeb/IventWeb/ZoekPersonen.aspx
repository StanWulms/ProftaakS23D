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
    <asp:TextBox ID="Tbsearch" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="search" />
    <p><asp:ListBox ID="lbnietaanwezig" runat="server"></asp:ListBox></p>
    <asp:ListBox ID="lbaanwezig" runat="server" Height="69px"></asp:ListBox>
</asp:Content>
