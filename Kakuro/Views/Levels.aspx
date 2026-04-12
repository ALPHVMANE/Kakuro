<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Levels.aspx.cs" Inherits="Kakuro.Views.Levels" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Kakuro - Levels</title>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="../Content/Levels.css?v=1" rel="stylesheet" type="text/css" />
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
        <div class="container body-content">
            <div class="z-stack-container position-relative">
                <asp:Panel ID="pnlLevels" runat="server" CssClass="v-stack-container d-flex flex-column align-items-center gap-2">
                    <asp:Button ID="btn25" runat="server" Text="1" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn26" runat="server" Text="2" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn27" runat="server" Text="3" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn28" runat="server" Text="4" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn29" runat="server" Text="5" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn30" runat="server" Text="6" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn31" runat="server" Text="7" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn32" runat="server" Text="8" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn33" runat="server" Text="9" CssClass="btn-level" OnClick="btn_Click"/>
                    <asp:Button ID="btn34" runat="server" Text="10" CssClass="btn-level" OnClick="btn_Click"/>
                </asp:Panel>
            </div>
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    </asp:PlaceHolder>
</body>
</html>