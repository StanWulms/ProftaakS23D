<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="EventBeheren.aspx.cs" Inherits="IventWeb.SysteembeheerInhoud.EventBeheren" %>
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
    <asp:Label ID="lblEvenementen" runat="server" Text="Alle evenementen:"></asp:Label><br />
    <asp:ListBox ID="ListBox1" runat="server" Height="309px" Width="263px"></asp:ListBox><br /><br />
        <asp:RequiredFieldValidator ID="rfvNaamLocatie" runat="server" ControlToValidate="tbnaamloc" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="LocatieValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblNieuwLocatie" runat="server" Text="Naam locatie: "></asp:Label>
        <asp:TextBox ID="tbnaamloc" runat="server" Width="173px"></asp:TextBox>
    <asp:Label ID="lblNaamLocatieError" runat="server" ForeColor="Red" Text="Niet uniek." Visible="False"></asp:Label>
    <br />
        <asp:RequiredFieldValidator ID="rfvStraat" runat="server" ControlToValidate="Tbstraat" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="LocatieValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblStraat" runat="server" Text="Straat: "></asp:Label>
        <asp:TextBox ID="Tbstraat" runat="server" Width="147px"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvHuisnummer" runat="server" ControlToValidate="Tbnr" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="LocatieValidators">*</asp:RequiredFieldValidator>       
        <asp:Label ID="lblNr" runat="server" Text="Nr: "></asp:Label>
        <asp:TextBox ID="Tbnr" runat="server" Width="41px" MaxLength="5"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revHuisnummer" runat="server" ControlToValidate="Tbnr" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,4})([A-z]{1})?" ValidationGroup="LocatieValidators">Fout formaat. Correct formaat ziet er als volgt uit: 0000x</asp:RegularExpressionValidator><br />
        <asp:RequiredFieldValidator ID="rfvPostcode" runat="server" ControlToValidate="Tbpostcode" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="LocatieValidators">*</asp:RequiredFieldValidator>       
        <asp:Label ID="lblPostcode" runat="server" Text="Postcode: "></asp:Label>
        <asp:TextBox ID="Tbpostcode" runat="server" Width="58px" MaxLength="7"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revPostcode" runat="server" ControlToValidate="Tbpostcode" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="^[1-9][0-9]{3}\s?[a-zA-Z]{2}$" ValidationGroup="LocatieValidators">Ongeldige postcode. Formaat: 0000XX. En mag niet met een 0 beginnen.</asp:RegularExpressionValidator>
    <br />
        <asp:RequiredFieldValidator ID="rfvPlaats" runat="server" ControlToValidate="Tbplaats" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="LocatieValidators">*</asp:RequiredFieldValidator>        
        <asp:Label ID="lblPlaats" runat="server" Text="Plaats: "></asp:Label>
        <asp:TextBox ID="Tbplaats" runat="server" Width="168px"></asp:TextBox><br /><br />
        
        <asp:Button ID="btnaddloc" runat="server" OnClick="btnaddloc_Click" Text="voeg locatie toe" />
    <asp:Label ID="lblLocatieError" runat="server" ForeColor="Red"></asp:Label>
    <br /><br /><br />
        <asp:RequiredFieldValidator ID="rfvEventNaam" runat="server" ControlToValidate="Tbnaame" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="EventValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblEvent" runat="server" Text="Naam event: "></asp:Label>
        <asp:TextBox ID="Tbnaame" runat="server" Width="175px"></asp:TextBox>
    <asp:Label ID="lblNaamEventError" runat="server" ForeColor="Red" Text="Niet uniek." Visible="False"></asp:Label>
    <br />
        <asp:Label ID="lblLocatie" runat="server" Text="Naam locatie: "></asp:Label>
        <asp:DropDownList ID="ddlLocation" runat="server" Width="147px"></asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="rfvDatumStart" runat="server" ControlToValidate="Tbdatumstart" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="EventValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblDatumStart" runat="server" Text="Datumstart: "></asp:Label>
        <asp:TextBox ID="Tbdatumstart" runat="server" Width="91px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Tbdatumstart" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="EventValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br />
        <asp:RequiredFieldValidator ID="rfvDatumEind" runat="server" ControlToValidate="Tbdatumeind" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="EventValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblDatumEind" runat="server" Text="Datumeind: "></asp:Label>
        <asp:TextBox ID="Tbdatumeind" runat="server" Width="91px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Tbdatumeind" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="EventValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br />
        <asp:RequiredFieldValidator ID="rfvMaxBezoekers" runat="server" ControlToValidate="Tbmaxbezoeker" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="EventValidators">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblMaxBezoekers" runat="server" Text="Max bezoekers: "></asp:Label>
        <asp:TextBox ID="Tbmaxbezoeker" runat="server" Width="153px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Tbmaxbezoeker" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,})" ValidationGroup="EventValidators">Vul een getal in.</asp:RegularExpressionValidator>
    <br /><br />
        <asp:Button ID="btnAddEvent" runat="server" Text="voeg event toe" OnClick="btnAddEvent_click" />      
    <asp:Label ID="lblEventError" runat="server" ForeColor="Red"></asp:Label>
        
</asp:Content>
