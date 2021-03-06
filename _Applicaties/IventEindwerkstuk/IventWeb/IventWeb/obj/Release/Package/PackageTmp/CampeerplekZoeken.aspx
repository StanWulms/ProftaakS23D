﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="CampeerplekZoeken.aspx.cs" Inherits="IventWeb.ReservatieInhoud.CampeerplekZoeken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Campeerplekzoeken.css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav id="nav">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <li><a href="Inlog.aspx">Uitloggen</a></li>
            <li><a href="Reservering.aspx">Reservering</a></li>
            <li><a href="CampeerplekZoeken.aspx">Campeerplek zoeken</a></li>
            <li><a href="Betaling.aspx">Betaling</a></li>
        </ul>
    </nav>
    <!-- Java script om de barcode pop-up aan te roepen. 
        Hiermee wordt voor ieder account een apparte barcode gescant -->
    <script type="text/javascript">
        function barcodeScanner(naam) {
            var barcode = prompt(naam);            

            if (barcode != null) {
                document.getElementById('<%= Hidden1.ClientID %>').value = barcode;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br /><br />
    <asp:Label ID="lblOmschrijving" runat="server" Text="Reservatiehouder"></asp:Label>
    <asp:Button ID="btnSelecteerReservatieHouder" runat="server" OnClick="btnSelecteerReservatieHouder_Click" Text="Selecteer" Width="76px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Bezoekers
    <asp:Button ID="btnSelecteerBezoeker" runat="server" OnClick="btnSelecteerBezoeker_Click" Text="Selecteer" Width="77px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Geselecteerde personen
    <asp:Button ID="btnDeSelecteerBezoeker" runat="server" OnClick="btnDeSelecteerBezoeker_Click" Text="De-Selecteer" Width="102px" />
    <br />
    <asp:ListBox ID="lbReservatieHouder" runat="server" Height="160px" Width="280px"></asp:ListBox>
    <asp:ListBox ID="lbBezoeker" runat="server" Height="160px" Width="277px" Margin-left="100px"></asp:ListBox>
    <asp:ListBox ID="lbGeselecteerdePersonen" runat="server" Height="160px" Width="277px" Margin-left="100px"></asp:ListBox><br />
    <asp:Label ID="lblReservatiehouderTekst" runat="server" Text="Reservatiehouder: "></asp:Label>
    <asp:Label ID="lblReservatiehouder" runat="server" Text=""></asp:Label><br /><br />
    <asp:Label ID="lblPlek" runat="server" Text="Plek"></asp:Label><br />
    <asp:ListBox ID="lbPlek" runat="server" Height="144px" Width="278px"></asp:ListBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNext" runat="server" BackColor="#66FF33" ForeColor="Black" Height="70px" OnClick="btnNext_Click" Text="Next" Visible="False" Width="129px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <asp:Label ID="lblBeginDatum" runat="server" Text="Begindatum: "></asp:Label>
    <asp:TextBox ID="tbBeginDatum" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbBeginDatum" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="AllValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblEindDatum" runat="server" Text="Einddatum: "></asp:Label>
    <asp:TextBox ID="tbEindDatum" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbEindDatum" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="([0-9]{2})(-|/)([A-Z]){3}(-|/)([0-9]{4})" ValidationGroup="AllValidators">Ongeldige datum. Formaat: 00-XXX-0000</asp:RegularExpressionValidator>
    <br /><br />
    <asp:Button ID="btnPlaatsReservering" runat="server" Text="Reserveer Plaats" OnClick="btnPlaatsReservering_Click" />
    <asp:Label ID="lblReserveerError" runat="server" ForeColor="Red"></asp:Label><br /><br />
    <div>
        <div id="Campeerplaats">
            <img src ="Images/CampingPlattegrond.png" width="700px" height="700px"/>
        </div>
    </div>
    <!-- Tijdelijke opslag voor de barcode -->
    <!--<p id="NotesSiteName" runat="server"></p> -->
    <!-- <asp:Label ID="lblTijdelijkeBarcode" runat="server" Text=""></asp:Label> -->
    <asp:HiddenField ID="Hidden1" runat="server" />
    <br /><br />
</asp:Content>
