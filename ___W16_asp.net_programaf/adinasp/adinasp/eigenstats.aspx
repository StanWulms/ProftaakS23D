<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eigenstats.aspx.cs" Inherits="adinasp.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblUsernameDisplay" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblFirstname" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblMiddleName" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblLastName" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblEmailId" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblCompany" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblCity" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblPostal" runat="server" Text="Label"></asp:Label><br/>
        <asp:Label ID="lblTelephone" runat="server" Text="Label"></asp:Label>
    
    </div>
        <p>
            <asp:Button ID="btnaanpas" runat="server" OnClick="btnaanpas_Click" Text="Gegevens aanpassen" />
        </p>
    </form>
</body>
</html>
