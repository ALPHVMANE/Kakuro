<asp:GridView runat="server"></asp:GridView>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gameplay.aspx.cs" Inherits="Kakuro.Gameplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button ID="btnCheckSolution" runat="server" Text="Check" OnClick="btnCheckSolution_Click" />
        <asp:Button ID="btnReset" runat="server" Text=Reset" OnClick="btnReset_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="" />
    </form>
</body>
</html>
