<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MateriaalVerhuurASP.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container" align="center">
        <asp:Button ID="btnVerhuur" runat="server" Text="Materiaal verhuren" Width="250px" OnClick="btnVerhuur_Click" />
        <br /><br />
        <asp:Button ID="btnTerugBrengen" runat="server" Text="Materiaal terugbrengen" Width="250px" OnClick="btnTerugBrengen_Click" />
        <br /><br />
        <asp:Button ID="btnToevoegen" runat="server" Text="Materiaal toevoegen" Width="250" OnClick="btnToevoegen_Click" />
    </div>
    </form>
</body>
</html>
