<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Inchecken.aspx.cs" Inherits="IventWeb.ToegangscontroleInhoud.Inchecken" %>
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
    <div>    
        <asp:TextBox ID="tbtag" runat="server"></asp:TextBox>
        <asp:Button ID="btnEnter" runat="server" OnClick="btnenter_Click" Text="Enter" />  
    </div><br />
    <asp:TextBox ID="Tbnaam" runat="server" Height="16px" ReadOnly="True"></asp:TextBox><br /><br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
