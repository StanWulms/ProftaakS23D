﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MateriaalVerhuren.aspx.cs" Inherits="MateriaalVerhuurASP.MateriaalVerhuren" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>
</head>
<body>

    <form id="form1" runat="server">

        <div id="list">
            <asp:Label ID="lblSelecteer" runat="server" Text="Selecteer een item uit de listbox om te verhuren."></asp:Label>
            <br />
            <asp:ListBox ID="lbItems" runat="server" Height="232px"></asp:ListBox>
            
        </div>
        <div>

    <div id ="text">
        <asp:Label ID="lblNaam" runat="server" Text="Naam: "></asp:Label>
        <asp:Label ID="lblDbNaam" runat="server"></asp:Label><br/>
    </div>
        </div>
        <p>
            <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
            <br/>
            <asp:Button ID="BtnZoek" runat="server" Text="naam zoeken" OnClick="BtnZoek_Click" />
        </p>
        <p>
            <br/>
            <asp:Label ID="Label1" runat="server" Text="exemplaarnummer"></asp:Label>
            <br />
        </p>
            
        <asp:TextBox ID="tbExemplaarnummer" runat="server"></asp:TextBox>
        <br />
            <asp:Button ID="btnVerhuur" runat="server" Text="Verhuur" OnClick="btnVerhuur_Click" />
    </form>
</body>
</html>
