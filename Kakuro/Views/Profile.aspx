<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Kakuro.Views.Profile" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Profile</title>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

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
        .stat-item {
            text-align: center;
        }
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
        .btn-back:hover {
            background-color: #357ab8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
            <div class="container">
                <span class="navbar-brand text-white" style="pointer-events: none;">Kakuro</span>
                <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Toggle navigation" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation" disabled="disabled">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item"><a class="nav-link" runat="server" href="~/Views/Levels">Levels</a></li>
                        <li class="nav-item"><a class="nav-link" runat="server" href="~/Views/SelectConfigurations">Custom Kakuro</a></li>
                    </ul>
                    <ul class="navbar-nav">
                        <li class="nav-item"><a class="nav-link" runat="server" href="~/Views/Profile">Profile</a></li>
                        <li class="nav-item">
                            <asp:LinkButton ID="btnLogout" runat="server" CssClass="nav-link" OnClick="btnLogout_Click">Logout</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
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
            <asp:Button ID="btnBack" runat="server" Text="Back to Home" CssClass="btn-back" OnClick="btnBack_Click" />
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    </asp:PlaceHolder>
</body>
</html>
