<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changestats.aspx.cs" Inherits="adinasp.changestats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Username: "></asp:Label>
        <asp:Label ID="lblnaam" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="tbfirstname" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="Middle Name"></asp:Label>
        <asp:TextBox ID="tbmiddlename" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="tblastname" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label5" runat="server" Text="Email-adres"></asp:Label>
        <asp:TextBox ID="tbemail" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label6" runat="server" Text="Title"></asp:Label>
        <asp:TextBox ID="tbtitle" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label7" runat="server" Text="Company"></asp:Label>
        <asp:TextBox ID="tbcompany" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label8" runat="server" Text="City"></asp:Label>
        <asp:TextBox ID="tbcity" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label9" runat="server" Text="State"></asp:Label>
        <asp:TextBox ID="tbstate" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label10" runat="server" Text="Country"></asp:Label>
        <asp:TextBox ID="tbcountry" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label11" runat="server" Text="Postal Code"></asp:Label>
        <asp:TextBox ID="tbpostalcode" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label12" runat="server" Text="Telephone No."></asp:Label>
        <asp:TextBox ID="tbtelefoon" runat="server"></asp:TextBox>
    
    </div>
        <p>
            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" />
        </p>
    </form>
</body>
</html>
