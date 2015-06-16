<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Reservering.aspx.cs" Inherits="IventWeb.ReservatieInhoud.ReserveringPlaatsen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="Reservering.aspx">Reservering</a></li>
            <li><a href="CampeerplekZoeken.aspx">Campeerplek zoeken</a></li>
        </ul>
    </nav>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvVoornaam" runat="server" ControlToValidate="tbVoornaam" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblVoornaam" runat="server" Text="Voornaam: "></asp:Label>
    <asp:TextBox ID="tbVoornaam" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Label ID="lblTussenvoegsel" runat="server" Text="Tussenvoegsel: "></asp:Label>
    <asp:TextBox ID="tbTussenvoegsel" runat="server"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="rfvAchternaam" runat="server" ControlToValidate="tbAchternaam" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAchternaam" runat="server" Text="Achternaam: "></asp:Label>
    <asp:TextBox ID="tbAchternaam" runat="server"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="tbGebruikersnaam" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblGebruikersnaam" runat="server" Text="Gebruikersnaam: "></asp:Label>
    <asp:TextBox ID="tbGebruikersnaam" runat="server"></asp:TextBox>
    <asp:Label ID="lblHoofdBoekerError" runat="server" ForeColor="Red" Text="Niet uniek." Visible="False"></asp:Label>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <asp:Label ID="lblHoofdBoekerEmailError" runat="server" ForeColor="Red" Text="Niet uniek." Visible="False"></asp:Label>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvStraat" runat="server" ControlToValidate="tbStraat" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblStraat" runat="server" Text="Straat: "></asp:Label>
    <asp:TextBox ID="tbStraat" runat="server"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="rfvHuisnummer" runat="server" ControlToValidate="tbHuisnummer" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblHuisnummer" runat="server" Text="Huisnummer: "></asp:Label>
    <asp:TextBox ID="tbHuisnummer" runat="server" MaxLength="5" Width="45px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revHuisnummer" runat="server" ControlToValidate="tbHuisnummer" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{1,4})([A-z]{1})?" ValidationGroup="AllValidators">Fout formaat. Correct formaat ziet er als volgt uit: 0000x</asp:RegularExpressionValidator><br /><br />
    <asp:RequiredFieldValidator ID="rfvWoonplaats" runat="server" ControlToValidate="tbWoonplaats" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblWoonplaats" runat="server" Text="Woonplaats: "></asp:Label>
    <asp:TextBox ID="tbWoonplaats" runat="server"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="rfvBankrekening" runat="server" ControlToValidate="tbBankrekening" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblBankrekening" runat="server" Text="Bankrekening nummer: "></asp:Label>
    <asp:TextBox ID="tbBankrekening" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revHuisnummer0" runat="server" ControlToValidate="tbBankrekening" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}" ValidationGroup="AllValidators">Ongeldig rekeningnummer. Vul uw correct IBAN in.</asp:RegularExpressionValidator><br /><br />
    <asp:Label ID="lblAantal" runat="server" Text="Aantal: "></asp:Label>
    <asp:DropDownList ID="ddlAantal" runat="server" OnTextChanged="ddlAantal_TextChanged">
        <asp:ListItem Value="1">
            1
        </asp:ListItem>
        <asp:ListItem Value="2">
            2
        </asp:ListItem>
        <asp:ListItem Value="3">
            3
        </asp:ListItem>
        <asp:ListItem Value="4">
            4
        </asp:ListItem>
        <asp:ListItem Value="5">
            5
        </asp:ListItem>
        <asp:ListItem Value="6">
            6
        </asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="refresh" runat="server" Text="Genereer kaartjes" OnClick="refresh_Click" /><br /><br />
    <asp:RequiredFieldValidator ID="rfvAccount1" runat="server" ControlToValidate="tbAccount1" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAccount1" runat="server" Text="Account 1: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbAccount1" runat="server" Visible="False">-1</asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail1" runat="server" ControlToValidate="tbEmail1" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail1" runat="server" Text="     Email: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbEmail1" runat="server" Visible="False">-1</asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail1" runat="server" ControlToValidate="tbEmail1" Display="Dynamic" Enabled="False" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvAccount2" runat="server" ControlToValidate="tbAccount2" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAccount2" runat="server" Text="Account 2: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbAccount2" runat="server" Visible="False">-2</asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail2" runat="server" ControlToValidate="tbEmail2" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail2" runat="server" Text="     Email: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbEmail2" runat="server" Visible="False">-2</asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail2" runat="server" ControlToValidate="tbEmail2" Display="Dynamic" Enabled="False" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvAccount3" runat="server" ControlToValidate="tbAccount3" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAccount3" runat="server" Text="Account 3: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbAccount3" runat="server" Visible="False">-3</asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail3" runat="server" ControlToValidate="tbEmail3" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail3" runat="server" Text="     Email: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbEmail3" runat="server" Visible="False">-3</asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail3" runat="server" ControlToValidate="tbEmail3" Display="Dynamic" Enabled="False" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvAccount4" runat="server" ControlToValidate="tbAccount4" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAccount4" runat="server" Text="Account 4: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbAccount4" runat="server" Visible="False">-4</asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail4" runat="server" ControlToValidate="tbEmail4" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail4" runat="server" Text="     Email: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbEmail4" runat="server" Visible="False">-4</asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail4" runat="server" ControlToValidate="tbEmail4" Display="Dynamic" Enabled="False" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <br /><br />
    <asp:RequiredFieldValidator ID="rfvAccount5" runat="server" ControlToValidate="tbAccount5" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblAccount5" runat="server" Text="Account 5: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbAccount5" runat="server" Visible="False">-5</asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvEmail5" runat="server" ControlToValidate="tbEmail5" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="AllValidators" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:Label ID="lblEmail5" runat="server" Text="     Email: " Visible="False"></asp:Label>
    <asp:TextBox ID="tbEmail5" runat="server" Visible="False">-5</asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail5" runat="server" ControlToValidate="tbEmail5" Display="Dynamic" Enabled="False" ErrorMessage="Ongeldige Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Ongeldig Email-adres</asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblDubbeleUsername" runat="server" Text="Gebruik geen dubbele gebruikersnamen! De gebruikersnaam van de hoofdboeker telt ook mee." ForeColor="Red" Visible="False"></asp:Label>
    <br />
    <asp:Label ID="lblDubbeleEmail" runat="server" Text="Gebruik geen dubbele emailadressen! Het Email-adres van de hoofdboeker telt ook mee." ForeColor="Red" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="btnRegistreer" runat="server" Text="Registreer" OnClick="btnRegistreer_Click" ValidationGroup="AllValidators" CausesValidation="false"/>
    <asp:Label ID="lblValidation" runat="server" Text="" Visible="False"></asp:Label>
</asp:Content>
