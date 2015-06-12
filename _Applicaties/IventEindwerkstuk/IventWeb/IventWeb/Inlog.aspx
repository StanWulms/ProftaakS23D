<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Inlog.aspx.cs" Inherits="IventWeb.Inlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Registreren.aspx">Home</a></li>
            <li><a href="Hoofdpagina.aspx">Inloggen</a></li>
        </ul>
    </nav>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Username: "></asp:Label>
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br /><br />
    <asp:Label ID="Label4" runat="server" Text="Password: "></asp:Label>
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br /><br /><br />
    <asp:Button ID="Button2" runat="server" Text="Login" />
</asp:Content>