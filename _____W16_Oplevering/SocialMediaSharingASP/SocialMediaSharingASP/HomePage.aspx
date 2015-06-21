<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SocialMediaSharingASP.HomePage" %>

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
    </div>
    <div id="Mappen">
        <asp:Panel ID="pnlMappen" runat="server" Width="410px">
            <asp:Label ID="Label1" runat="server" Text="." ForeColor="Yellow"></asp:Label>
        </asp:Panel>
    </div>
    <div id="list">
        <asp:ListBox ID="ListBox1" runat="server" Height="150px" Width="200px"></asp:ListBox>
    </div>
    <div id="Post">
        <asp:Image ID="Image1" runat="server" />
        <br />
        <asp:Label ID="lblPostNaam" runat="server" Text=""></asp:Label>
        <br />
        <asp:Panel ID="pnlPost" runat="server">

        </asp:Panel>
    </div>
    </form>
</body>
</html>
