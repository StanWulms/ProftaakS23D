<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="adinasp.ads_hit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:TextBox ID="tbnaam" runat="server"></asp:TextBox>
        <asp:Label ID="lbldomein" runat="server"></asp:Label>
        <p>
            <asp:TextBox ID="tbwachtwoord" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnlogin" runat="server" OnClick="Button1_Click" Text="log in" />
        </p>
        <p>
            <asp:TextBox ID="tbcheck" runat="server" style="margin-bottom: 44px"></asp:TextBox>
        </p>
        <asp:TextBox ID="tbmailadres" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="btnmail" runat="server" OnClick="btnmail_Click" Text="Button" />
        </p>
    </form>
</body>
</html>
