<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="Leaderboard.aspx.cs" Inherits="Kakuro.Leaderboard" %>

<asp:Content ID="LeaderboardContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .leaderboard {
            max-width: 1000px;
            margin: 60px auto;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            padding: 30px;
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th {
            background: #4a90d9;
            color: #fff;
            padding: 10px;
            text-align: left;
        }

        td {
            padding: 10px;
            border-bottom: 1px solid #ccc;
        }

        tr:last-child td {
            border-bottom: none;
        }
    </style>

    <div class="leaderboard">
        <h2>Top 5 Leaderboard</h2>

        <table>
            <tr>
                <th>Rank</th>
                <th>Username</th>
                <th>Score</th>
            </tr>

            <asp:Repeater ID="rptLeaderboard" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td><%# Eval("Username") %></td>
                        <td><%# Eval("Score") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
