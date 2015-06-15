<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eventbeheersysteem.aspx.cs" Inherits="eventbeheersysteemasp.eventbeheersysteem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:ListBox ID="ListBox1" runat="server" Height="309px" Width="223px"></asp:ListBox>
        <p>
            <asp:Label ID="Label1" runat="server" Text="naam locatie:"></asp:Label>
            <asp:TextBox ID="tbnaamloc" runat="server" Width="147px"></asp:TextBox>
        </p>
        <asp:Label ID="Label2" runat="server" Text="straat"></asp:Label>
        <asp:TextBox ID="Tbstraat" runat="server" Width="147px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="nr"></asp:Label>
        <asp:TextBox ID="Tbnr" runat="server" Width="147px"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="postcode"></asp:Label>
        <asp:TextBox ID="Tbpostcode" runat="server" Width="147px"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="plaats"></asp:Label>
        <asp:TextBox ID="Tbplaats" runat="server" Width="147px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnaddloc" runat="server" OnClick="btnaddloc_Click" Text="voeg locatie toe" />
        <br />
        <asp:Label ID="Label7" runat="server" Text="naam event"></asp:Label>
        <asp:TextBox ID="Tbnaame" runat="server" Width="147px"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" Text="naam locatie"></asp:Label>
        <asp:TextBox ID="Tbnaamlocatie" runat="server" Width="147px"></asp:TextBox>
        <br />
        <asp:Label ID="Label9" runat="server" Text="datumstart"></asp:Label>
        <asp:TextBox ID="Tbdatumstart" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label10" runat="server" Text="datumeind"></asp:Label>
        <asp:TextBox ID="Tbdatumeind" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label11" runat="server" Text="max bezoekers"></asp:Label>
        <asp:TextBox ID="Tbmaxbezoeker" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="btnAddEvent" runat="server" Text="voeg event toe" OnClick="btnAddEvent_click" />
        </p>
    </form>
</body>
</html>
