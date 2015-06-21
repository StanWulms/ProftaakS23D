<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="GenereerAccount.aspx.cs" Inherits="IventWeb.SysteembeheerInhoud.GenereerAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="GenereerAccount.aspx">Genereer account</a></li>
            <li><a href="EventBeheren.aspx">Event beheren</a></li>
            <li><a href="PlekBeheren.aspx">Plek beheren</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:TextBox ID="tbemail" runat="server"></asp:TextBox>
    <asp:Button ID="btnverstuur" runat="server" OnClick="btnverstuur_Click" Text="verstuur email" />
</asp:Content>
