<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MateriaalVerhuren.aspx.cs" Inherits="MateriaalVerhuurASP.MateriaalVerhuren" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>
</head>
<body>

    <form id="form1" runat="server">

    <div id ="text">
        <asp:Label ID="lblNaam" runat="server" Text="Naam: "></asp:Label>
        <asp:Label ID="lblDbNaam" runat="server" Text="dbNaam"></asp:Label>
    </div>
        <div id="list">
            <asp:Label ID="lblSelecteer" runat="server" Text="Selecteer een item uit de listbox om te verhuren."></asp:Label>
            <br />
            <asp:ListBox ID="lbItems" runat="server">
                <asp:ListItem>Item1</asp:ListItem>
            </asp:ListBox>
        </div>
        <div>
            <asp:Button ID="btnVerhuur" runat="server" Text="Verhuur" OnClick="btnVerhuur_Click" />
        </div>
    </form>
</body>
</html>
