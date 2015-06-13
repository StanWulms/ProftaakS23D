<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MateriaalTerugbrengen.aspx.cs" Inherits="MateriaalVerhuurASP.MateriaalTerugbrengen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Terugbrengen"></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="lbTerugbrengen" runat="server"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Terugbrengen" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
