<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="IventWeb.SMS" %>
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
    Dit is SMS
</asp:Content>
