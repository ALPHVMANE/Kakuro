<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Kakuro.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
        <!--Register-->
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5">

                    <div class="card shadow">
                        <div class="card-body p-4">
                                
                            <h3 class="text-center mb-4">Register an Account</h3>

                            <!--Email-->
                            <div class="mb-3">
                                <label class="form-label">Email address:</label>
                                <asp:TextBox ID="txtEmail" runat="server"
                                    CssClass="form-control"
                                    TextMode="Email"
                                    placeholder="Enter Email" />
                            </div>

                            <!--Username-->
                            <div class="mb-3">
                                <label class="form-label">Username:</label>
                                <asp:TextBox ID="txtUsername" runat="server"
                                    CssClass="form-control"
                                    placeholder="Enter Username" />
                            </div>

                            <!--Password-->
                            <div class="mb-3">
                                <label class="form-label">Password:</label>
                                <asp:TextBox ID="txtPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    placeholder="Enter Password" />
                            </div>

                            <!--Confirm Password-->
                            <div class="mb-3">
                                <label class="form-label">Confirm Password:</label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    placeholder="Confirm Password" />
                            </div>

                            <!--Error Message-->
                            <asp:Label ID="lblError" runat="server" />

                            <!--Register Button-->
                            <div class="d-grid">
                                <asp:Button ID="btnRegister"
                                    runat="server"
                                    Text="Register"
                                    CssClass="btn btn-primary"
                                    OnClick="btnRegister_Click" />
                            </div>

                            <!--Login Link-->
                            <div class="text-center mt-3">
                                <small>
                                    <a href="Login.aspx">Already have an account? Login here!</a>
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
