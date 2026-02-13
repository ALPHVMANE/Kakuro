<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Kakuro.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
          rel="stylesheet" />

</head>

<body>

    <form id="form1" runat="server">

        <!--Navigation Bar-->
        <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
            <div class="container-fluid">
                <a class="navbar-brand text-decoration-underline">Kakuro</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul class="navbar-nav">

                        <li class="nav-item">
                            <a class="nav-link" href="">Home</a>
                        </li>

                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown">
                                Authentication
                            </a>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="">Login</a></li>
                                <li><a class="dropdown-item" href="">Register</a></li>
                            </ul>
                        </li>

                    </ul>
                </div>
            </div>
        </nav>

        <!--Register-->
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5">

                    <div class="card shadow">
                        <div class="card-body p-4">

                            <h3 class="text-center mb-4">Create an Account</h3>

                            <!--Email-->
                            <div class="mb-3">
                                <label class="form-label">Email address:</label>
                                <asp:TextBox ID="txtEmail" runat="server"
                                    CssClass="form-control"
                                    TextMode="Email"
                                    placeholder="Enter email" />
                            </div>

                            <!--Username-->
                            <div class="mb-3">
                                <label class="form-label">Username:</label>
                                <asp:TextBox ID="txtUsername" runat="server"
                                    CssClass="form-control"
                                    placeholder="Enter username" />
                            </div>

                            <!--Password-->
                            <div class="mb-3">
                                <label class="form-label">Password:</label>
                                <asp:TextBox ID="txtPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    placeholder="Enter password" />
                            </div>

                            <!--Confirm Password-->
                            <div class="mb-3">
                                <label class="form-label">Confirm Password:</label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    placeholder="Confirm password" />
                            </div>

                            <!--Register Button-->
                            <div class="d-grid">
                                <asp:Button ID="btnRegister"
                                    runat="server"
                                    Text="Register"
                                    CssClass="btn btn-primary"
                                    OnClick="btnRegister_Click" />
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>

    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>