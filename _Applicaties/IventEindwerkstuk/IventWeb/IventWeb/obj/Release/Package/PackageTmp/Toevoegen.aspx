<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Toevoegen.aspx.cs" Inherits="IventWeb.VerhuurInhoud.Toevoegen" %>
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
    <br />
    <asp:Label ID="Label2" runat="server" Text="product categorie"></asp:Label>
    <asp:DropDownList ID="DDLproductcat" runat="server">
    </asp:DropDownList><br /><br />
    <asp:RequiredFieldValidator ID="rfvSerie" runat="server" ControlToValidate="tbSoort" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="VoorwerpToevoegenValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblSoort" runat="server" Text="serie:"></asp:Label>
    <asp:TextBox ID="tbSoort" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="rfvMerk" runat="server" ControlToValidate="tbMerk" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="VoorwerpToevoegenValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblMerk" runat="server" Text="Merk: "></asp:Label>
    <asp:TextBox ID="tbMerk" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="rfvPrijs" runat="server" ControlToValidate="Tbprijs" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="VoorwerpToevoegenValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="Label3" runat="server" Text="prijs:"></asp:Label>
    <asp:TextBox ID="Tbprijs" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revPrijs" runat="server" ControlToValidate="Tbprijs" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="VoorwerpToevoegenValidators">Vul hele euros in.</asp:RegularExpressionValidator>
    <br />
    <asp:Button ID="btnVoorwerpToevoegen" runat="server" Text="Voorwerp toevoegen" OnClick="btnToevoegen_Click" /><br /><br />
    <asp:Label ID="lblExemplaar" runat="server" Text="Voeg een exemplaar toe:"></asp:Label><br />
    <asp:Label ID="lblVoorwerp" runat="server" Text="Voorwerp: "></asp:Label>
    <asp:DropDownList ID="ddlSoort" runat="server"></asp:DropDownList><br />
    <asp:RequiredFieldValidator ID="rfvBeschrijving" runat="server" ControlToValidate="tbBeschrijving" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="ExemplaarToevoegenValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblBeschrijving" runat="server" Text="Beschrijving: "></asp:Label>
    <asp:TextBox ID="tbBeschrijving" runat="server"></asp:TextBox><br />
    <asp:Button ID="btnExemplaarToevoegen" runat="server" Text="Exemplaar toevoegen" OnClick="btnExemplaarToevoegen_Click" />
</asp:Content>
