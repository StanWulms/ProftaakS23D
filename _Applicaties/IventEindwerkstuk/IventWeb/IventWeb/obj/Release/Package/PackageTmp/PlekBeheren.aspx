<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="PlekBeheren.aspx.cs" Inherits="IventWeb.Plekbeheren" %>
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
    <br /><br />
    <asp:Label ID="lblEvent" runat="server" Text="Selecteer een event"></asp:Label>
    <asp:DropDownList ID="ddlEvent" runat="server"></asp:DropDownList>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvNummer" runat="server" ControlToValidate="tbNummer" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblNummer" runat="server" Text="Nummer: "></asp:Label>
    <asp:TextBox ID="tbNummer" runat="server" MaxLength="10" Width="110px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revNummer" runat="server" ControlToValidate="tbNummer" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="AllValidators">Alleen hele getallen invoeren.</asp:RegularExpressionValidator>
    <asp:Label ID="lblNummerError" runat="server" ForeColor="Red" Text="Nummer al in gebruik." Visible="False"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="rfvCapaciteit" runat="server" ControlToValidate="tbCapaciteit" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblCapaciteit" runat="server" Text="Max personen: "></asp:Label>
    <asp:TextBox ID="tbCapaciteit" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revCapaciteit" runat="server" ControlToValidate="tbCapaciteit" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="AllValidators">Alleen hele getallen invoeren.</asp:RegularExpressionValidator>
    <br /><br />
    <asp:Label ID="lblExtras" runat="server" Text="Plekspecificaties: "></asp:Label>
    <br /><br />
    <asp:Label ID="lblComfort" runat="server" Text="Comfortplek "></asp:Label>
    <asp:CheckBox ID="cbComfort" runat="server" /><br />
    <asp:Label ID="lblHandicap" runat="server" Text="Handicap geschikt "></asp:Label>
    <asp:CheckBox ID="cbHandicap" runat="server" /><br />
    <asp:Label ID="lblKraanwater" runat="server" Text="Kraan beschikbaar "></asp:Label>
    <asp:CheckBox ID="cbKraanwater" runat="server" /><br /><br />
    <asp:Button ID="btnAdd" runat="server" Text="Maak plaats" OnClick="btnAdd_Click" />
</asp:Content>
