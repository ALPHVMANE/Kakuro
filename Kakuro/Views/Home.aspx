<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Kakuro.Views.Home" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Kakuro - Home</title>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
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
                        <li class="nav-item"><a class="nav-link" runat="server" href="~/Views/SelectConfiguration">Custom Kakuro</a></li>
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
        <div class="container body-content">
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    </asp:PlaceHolder>
</body>
</html>