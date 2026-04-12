<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" 
    CodeBehind="Profile.aspx.cs" Inherits="Kakuro.Views.Profile" %>

<asp:Content ID="ProfileContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />
    <style>
        .profile-container {
            max-width: 500px;
            margin: 80px auto;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            padding: 40px;
            text-align: center;
        }
        .profile-avatar {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            background-color: #4a90d9;
            color: #fff;
            font-size: 48px;
            line-height: 100px;
            margin: 0 auto 20px;
        }
        .profile-username {
            font-size: 24px;
            font-weight: bold;
            color: #333;
            margin-bottom: 5px;
        }
        .profile-email {
            font-size: 14px;
            color: #888;
            margin-bottom: 30px;
        }
        .stats-grid {
            display: flex;
            justify-content: center;
            gap: 40px;
            margin-bottom: 30px;
        }
        .stat-item { text-align: center; }
        .stat-value {
            font-size: 36px;
            font-weight: bold;
            color: #4a90d9;
        }
        .stat-label {
            font-size: 14px;
            color: #666;
            margin-top: 5px;
        }
        .btn-back {
            display: inline-block;
            padding: 10px 30px;
            background-color: #4a90d9;
            color: #fff;
            border: none;
            border-radius: 5px;
            font-size: 14px;
            cursor: pointer;
            text-decoration: none;
        }
        .btn-back:hover { background-color: #357ab8; }
    </style>

    <div class="profile-container">
        <div class="profile-avatar">
            <asp:Label ID="lblInitial" runat="server" />
        </div>
        <div class="profile-username">
            <asp:Label ID="lblUsername" runat="server" />
        </div>
        <div class="profile-email">
            <asp:Label ID="lblEmail" runat="server" />
        </div>
        <div class="stats-grid">
            <div class="stat-item">
                <div class="stat-value">
                    <asp:Label ID="lblLevelsCompleted" runat="server" Text="0" />
                </div>
                <div class="stat-label">Levels Completed</div>
            </div>
            <div class="stat-item">
                <div class="stat-value">
                    <asp:Label ID="lblScore" runat="server" Text="0" />
                </div>
                <div class="stat-label">Total Score</div>
            </div>
        </div>
    </div>
</asp:Content>