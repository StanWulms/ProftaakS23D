<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MateriaalToevoegen.aspx.cs" Inherits="MateriaalVerhuurASP.MateriaalToevoegen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Toevoegen"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="product categorie"></asp:Label>
        <asp:DropDownList ID="DDLproductcat" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblSoort" runat="server" Text="serie:"></asp:Label>
        <asp:TextBox ID="tbSoort" runat="server"></asp:TextBox>
        <div></div>
        <asp:Label ID="lblMerk" runat="server" Text="Merk: "></asp:Label>
        <asp:TextBox ID="tbMerk" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="prijs:"></asp:Label>
        <asp:TextBox ID="Tbprijs" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnVoorwerpToevoegen" runat="server" Text="Voorwerp toevoegen" OnClick="btnToevoegen_Click" />
        <br />
        <br />
        <asp:Label ID="lblExemplaar" runat="server" Text="Voeg een exemplaar toe:"></asp:Label>
        <br />
        <asp:Label ID="lblVoorwerp" runat="server" Text="Voorwerp: "></asp:Label>
        <asp:DropDownList ID="ddlSoort" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblBeschrijving" runat="server" Text="Beschrijving: "></asp:Label>
        <asp:TextBox ID="tbBeschrijving" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnExemplaarToevoegen" runat="server" Text="Exemplaar toevoegen" OnClick="btnExemplaarToevoegen_Click" />
    </div>
    </form>
</body>
</html>
