<%@ Page Title="Login" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Kakuro.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">

                <div class="card shadow">
                    <div class="card-body p-4">

                        <h3 class="text-center mb-4">Login</h3>

                        <!--Email / Username-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Email or Username:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtLoginUser" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 420px;"
                                    placeholder="Enter Email or Username" />

                                <asp:RequiredFieldValidator ID="reqLoginUser" runat="server"
                                    ControlToValidate="txtLoginUser"
                                    ErrorMessage="Please enter a valid Email or Username."
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />
                            </div>
                        </div>

                        <!--Password-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Password:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtLoginPw" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 400px;"
                                    TextMode="Password"
                                    placeholder="Enter Password" />

                                <asp:RequiredFieldValidator ID="reqLoginPw" runat="server"
                                    ControlToValidate="txtLoginPw"
                                    ErrorMessage="Password is required!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />
                            </div>
                        </div>

                        <!--Errors-->
                        <asp:Label ID="lblError"
                            runat="server"
                            Font-Bold="true"
                            ForeColor="Red"
                            CssClass="d-block mb-2 text-center">
                        </asp:Label>

                        <div class="mx-auto" style="max-width: 450px;">
                            <asp:ValidationSummary ID="ValidationSummary1"
                                runat="server"
                                Font-Bold="true"
                                ForeColor="Red" />
                        </div>

                        <!--Login Button-->
                        <div class="d-flex justify-content-center mt-3">
                            <asp:Button ID="btnLogin"
                                runat="server"
                                Text="Login"
                                CssClass="btn btn-primary"
                                Style="width: 420px;"
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

</asp:Content>