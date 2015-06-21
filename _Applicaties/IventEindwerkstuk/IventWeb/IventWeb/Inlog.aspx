<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Inlog.aspx.cs" Inherits="IventWeb.Inlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Inloggen</a></li>
        </ul>
    </nav>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="lblUsername" runat="server" Text="Username: "></asp:Label>
    <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox><br /><br />
    <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
    <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox><br /><br />
    <asp:Label ID="lblApplicatie" runat="server" Text="Applicatie: "></asp:Label>
    <asp:DropDownList ID="ddlApplicatie" runat="server">
        <asp:ListItem Value="1">
            Event beheer systeem
        </asp:ListItem>
        <asp:ListItem Value="2">
            Materiaalverhuur
        </asp:ListItem>
        <asp:ListItem Value="3">
            Reservering
        </asp:ListItem>
        <asp:ListItem Value="4">
            SMS
        </asp:ListItem>
        <asp:ListItem Value="5">
            Toegangscontrole
        </asp:ListItem>
    </asp:DropDownList><br /><br /><br />
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
</asp:Content>