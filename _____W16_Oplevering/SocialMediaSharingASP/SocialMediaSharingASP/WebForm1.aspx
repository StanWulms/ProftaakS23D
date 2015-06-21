<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SocialMediaSharingASP.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="text">
        <asp:Label ID="lblNaam" runat="server" Text="Naam: "></asp:Label>
        <asp:Label ID="lblDbNaam" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lbMapNaam" runat="server" Text="Voeg een categorie toe: "></asp:Label>
        <br />
        <asp:TextBox ID="tbMapNaam" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnMapToevoegen" runat="server" Text="Categorie Toevoegen" OnClick="btnMapToevoegen_Click" />
    </div>
    <div id="Mappen">
        <asp:Panel ID="pnlMappen" runat="server" Width="410px">
            <asp:Label ID="Label1" runat="server" Text="." ForeColor="Yellow"></asp:Label>
        </asp:Panel>
    </div>
    <div id="list">
        <asp:ListBox ID="lbBestanden" runat="server" Height="150px" Width="300"></asp:ListBox>
        <br />
        <asp:Button ID="btnShowBestand" runat="server" Text="Upload bestand" OnClick="btnShowBestand_Click" />
        <br />
        <br />
        <br />
        <asp:ListBox ID="lbPosts" runat="server" Width="300px"></asp:ListBox>
        <br />
        <asp:Button ID="btnShowBerichtInhoud" runat="server" Text="Button" OnClick="btnShowBerichtInhoud_Click" />
        <br />
        <asp:Label ID="lblBerichtInhoud" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblBerichtReactie" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnLike" runat="server" Text="Like" Visible="false" OnClick="btnLike_Click" />
        <asp:Label ID="lblLike" runat="server" Text="" ForeColor="Green"></asp:Label>
        <br />
        <asp:Button ID="btnReport" runat="server" Text="Report" Visible="false" OnClick="btnReport_Click" />
        <asp:Label ID="lblReport" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <div id="Post">
        <!-- Dunno if using Panel -->
        <asp:Panel ID="pnlPost" runat="server">
            <!-- Video -->
            <video id="Viodeoo" width="320" height="240" controls="controls" autoplay="autoplay" runat="server">
            <source id="Videoo" src="..\Images\movie.mp4" type="video/mp4"></video>
        </asp:Panel>
        <br />
    </div>
        <br />
        <div id="PostPlaatsen">
            <asp:Label ID="lblPlaatsReactie" runat="server" Text="Plaats een bericht:"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblTitel" runat="server" Text="Titel: "></asp:Label>
            <asp:TextBox ID="tbTitel" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblInhoud" runat="server" Text="Inhoud: "></asp:Label>
            <asp:TextBox ID="tbInhoud" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnPlaatsBericht" runat="server" Text="Plaats Bericht" OnClick="btnPlaatsBericht_Click" />
        </div>
        <!-- IDK how to -->
        <div style="background-color:azure">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnFileUpload" runat="server" Text="Upload Filexxxx" OnClick="btnFileUpload_Click" />
        <br />
        <asp:Image ID="Image1" runat="server" />
        </div>
    </form>
</body>
</html>
