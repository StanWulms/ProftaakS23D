<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="toegangsform.aspx.cs" Inherits="toegangscontrole_asp.toegangsform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 205px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
            <asp:TextBox ID="tbtag" runat="server"></asp:TextBox>
            <asp:Button ID="btnEnter" runat="server" OnClick="btnenter_Click" Text="Enter" />
    
    </div>
        <p>
            <asp:TextBox ID="Tbnaam" runat="server" Height="16px" ReadOnly="True"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
        <asp:TextBox ID="Tbsearch" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="search" />
        <p>
    
        <asp:ListBox ID="lbnietaanwezig" runat="server"></asp:ListBox>
    
        </p>
        <asp:ListBox ID="lbaanwezig" runat="server" Height="69px">
        </asp:ListBox>
    </form>
</body>
</html>
