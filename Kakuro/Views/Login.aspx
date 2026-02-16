<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Views/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Kakuro.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />
</head>

<body>

    <form id="form1" runat="server">

        <!--Login-->
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5">

                    <div class="card shadow">
                        <div class="card-body p-4">

                            <h3 class="text-center mb-4">Login</h3>

                            <!--Email-->
                            <div class="mb-3">
                                <label class="form-label">Email:</label>
                                <asp:TextBox ID="txtLoginEmail"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Enter Username or Email" />
                            </div>

                            <!--Password-->
                            <div class="mb-3">
                                <label class="form-label">Password:</label>
                                <asp:TextBox ID="txtLoginPw"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    placeholder="Enter Password" />
                            </div>

                            <!--Error Message-->
                            <asp:Label ID="lblError" 
                                runat="server" 
                                ForeColor="Red" 
                                CssClass="d-block mb-3 text-center" />

                            <!--Login Button-->
                            <div class="d-grid">
                                <asp:Button ID="btnLogin"
                                    runat="server"
                                    Text="Login"
                                    CssClass="btn btn-primary" 
                                    OnClick="btnLogin_Click" />
                            </div>

                            <!--Register Link-->
                            <div class="text-center mt-3">
                                <small>
                                    <a href="Register.aspx">Don't have an account? Register here!</a>
                                </small>
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
