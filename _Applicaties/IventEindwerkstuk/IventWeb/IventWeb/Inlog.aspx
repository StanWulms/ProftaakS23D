<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Inlog.aspx.cs" Inherits="IventWeb.Inlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label2" runat="server" Text="Username: "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br />
    <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br /><br />
    <asp:Button ID="Button1" runat="server" Text="Login" />
</asp:Content>