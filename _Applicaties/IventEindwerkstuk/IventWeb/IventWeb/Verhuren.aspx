<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Verhuren.aspx.cs" Inherits="IventWeb.VerhuurInhoud.Verhuren" %>
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
    <div id="list">
        <asp:Label ID="lblSelecteer" runat="server" Text="Selecteer een item uit de listbox om te verhuren."></asp:Label><br />
        <asp:ListBox ID="lbItems" runat="server" Height="232px"></asp:ListBox><br />
    </div>
    <div>
        <div id ="text">
            <asp:Label ID="lblNaam" runat="server" Text="Naam: "></asp:Label>
            <asp:Label ID="lblDbNaam" runat="server"></asp:Label><br/>
        </div>
    </div>
        <asp:RequiredFieldValidator ID="rfvNaamVerhuur" runat="server" ControlToValidate="tbBarcode" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="NaamZoekValidators">*</asp:RequiredFieldValidator>
        <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox><br/>
        <asp:Button ID="BtnZoek" runat="server" Text="naam zoeken" OnClick="BtnZoek_Click" /><br/><br />
        <asp:Label ID="Label1" runat="server" Text="exemplaarnummer"></asp:Label><br />
        <asp:RequiredFieldValidator ID="rfvVerhuur" runat="server" ControlToValidate="tbExemplaarnummer" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="VerhuurValidators">*</asp:RequiredFieldValidator>
        <asp:TextBox ID="tbExemplaarnummer" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revExemplaarNummer" runat="server" ControlToValidate="tbExemplaarnummer" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="VerhuurValidators">Vul alleen getallen in.</asp:RegularExpressionValidator>
    <br />
        <asp:Button ID="btnVerhuur" runat="server" Text="Verhuur" OnClick="btnVerhuur_Click" />
</asp:Content>
