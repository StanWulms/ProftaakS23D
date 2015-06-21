<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="TerugBrengen.aspx.cs" Inherits="IventWeb.VerhuurInhoud.TerugBrengen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="Verhuren.aspx">Verhuren</a></li>
            <li><a href="TerugBrengen.aspx">Terug brengen</a></li>
            <li><a href="Toevoegen.aspx">Toevoegen</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>    
        <br /><asp:Label ID="Label1" runat="server" Text="Geleende Items"></asp:Label><br />
        <asp:ListBox ID="lbTerugbrengen" runat="server" Height="124px"></asp:ListBox><br /><br />
    </div>
        <asp:RequiredFieldValidator ID="rfvZoekNaam" runat="server" ControlToValidate="tbBarcode" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="ZoekNaamValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="Label2" runat="server" Text="Naam: "></asp:Label>
        <asp:Label ID="lblnaamd" runat="server"></asp:Label>
        <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
        <asp:Button ID="btnzoeknaam" runat="server" Text="Zoek naam" OnClick="btnzoeknaam_Click" /><br /><br />
        <asp:RequiredFieldValidator ID="rfvTerugBrengen" runat="server" ControlToValidate="tbEventnummer" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="TerugBrengenValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="Label3" runat="server" Text="Event nummer"></asp:Label>
        <asp:TextBox ID="tbEventnummer" runat="server"></asp:TextBox>
        <asp:Button ID="btnTerugBrengen" runat="server" OnClick="Button3_Click" Text="Terug brengen" />
        <asp:RegularExpressionValidator ID="revTerugBrengen" runat="server" ControlToValidate="tbEventnummer" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="TerugBrengenValidators">Vul alleen nummers in.</asp:RegularExpressionValidator><br />
        <asp:Label ID="lblTerugBrengenError" runat="server" ForeColor="Red" Text="Scan eerst een bezoeker" Visible="False"></asp:Label>
</asp:Content>
