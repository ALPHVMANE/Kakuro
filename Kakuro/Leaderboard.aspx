<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="Kakuro.Leaderboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Leaderboard</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="center">
            <div class="top3">
                <div class="two item">
                    <asp:Label CssClass="pos" ID="lbl2nd" runat="server" Text="2"></asp:Label>
                    <asp:Label ID="2ndName" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="2ndScore" runat="server" Text="Label"></asp:Label>
                </div>
                
                <div class="one item">
                    <asp:Label CssClass="pos" ID="lbl1rst" runat="server" Text="1"></asp:Label>
                    <asp:Label ID="1rstName" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="1rstScore" runat="server" Text="Label"></asp:Label>
                </div>

                <div class="three item">
                    <asp:Label CssClass="pos" ID="lbl3rs" runat="server" Text=""></asp:Label>
                    <asp:Label ID="3rdName" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="3rdScore" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="list">
                <asp:Repeater ID="LeaderboardRepeater" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <div class="pos"><%# Eval("Position") %></div>
                            <div class="pic" style="background-image: url('<%# Eval("ImageUrl") %>')"></div>
                            <div class="name"><%# Eval("Name") %></div>
                            <div class="score"><%# Eval("Score") %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                
                <div class="item">
                    <div class="pos">4</div>
                    <div class="pic" style="background-image: url('https://randomuser.me/api/portraits/men/88.jpg')"></div>
                    <div class="name">Clayton Watson</div>
                    <div class="score">5980</div>
                </div>
                </div>
        </div>
    </form>
</body>
</html>