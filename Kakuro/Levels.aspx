<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Levels.aspx.cs" Inherits="Kakuro.Levels" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="level-container">
            <asp:Repeater ID="lsLevels" runat="server" OnItemCommand="rptLevels_ItemCommand">
                <ItemTemplate>
                    
                    <asp:Button ID="btnLevel" runat="server" OnClick="btnLevel_Click"
                    Text='<%# "Level " + Container.DataItem %>'
                    CommandArgument='<%# Container.DataItem %>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
