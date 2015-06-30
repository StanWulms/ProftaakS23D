<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Kampeerplaats.aspx.cs" Inherits="Reservering_Reparatie.Campeerplaats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Reservering.aspx">Reservering</a></li>
            <li><a href="Kampeerplaats.aspx">Kampeerplaats</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <asp:ListBox ID="lbAccounts" runat="server" Width="200"></asp:ListBox>
    <asp:Button ID="btnSelecteer" runat="server" Text="Selecteer Account" OnClick="btnSelecteer_Click" />
    <asp:ListBox ID="lbSelectedAccounts" runat="server" Width="200"></asp:ListBox>
    <br />
    <asp:ListBox ID="lbVrijePlaatsen" runat="server" Width="200"></asp:ListBox>
    <br />
    <br />
    <asp:Label ID="lblBeginDatum" runat="server" Text="Begindatum: "></asp:Label>
    <asp:TextBox ID="tbBeginDatum" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbBeginDatum" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="AllValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblEindDatum" runat="server" Text="Einddatum: "></asp:Label>
    <asp:TextBox ID="tbEindDatum" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbEindDatum" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="AllValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br />
    <asp:Button ID="btnKampeerplaatsReserveren" runat="server" Text="Kampeerplaats Reserveren" OnClick="btnKampeerplaatsReserveren_Click" />
</asp:Content>
