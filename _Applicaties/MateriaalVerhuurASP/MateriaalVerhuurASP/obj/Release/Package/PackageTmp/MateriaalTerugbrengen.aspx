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
    </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="naam: "></asp:Label>
            <asp:Label ID="lblnaamd" runat="server"></asp:Label>
        </p>
        <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
        <asp:Button ID="btnzoeknaam" runat="server" Text="zoek naam" OnClick="btnzoeknaam_Click" />
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="event nummer"></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="tbEventnummer" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnTerugBrengen" runat="server" OnClick="Button3_Click" Text="terug brengen" />
        </p>
    </form>
</body>
</html>
