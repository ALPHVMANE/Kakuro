<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Kakuro.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">

                <div class="card shadow">
                    <div class="card-body p-4">

                        <h3 class="text-center mb-4">Register an Account</h3>

                        <!--Email-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Email address:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtEmail" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 420px;"
                                    TextMode="Email"
                                    placeholder="Enter Email" />

                                <asp:RequiredFieldValidator ID="reqEmail" runat="server"
                                    ControlToValidate="txtEmail"
                                    ErrorMessage="Email is required!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />
                            </div>
                        </div>

                        <!--Username-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Username:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtUsername" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 420px;"
                                    placeholder="Enter Username" />

                                <asp:RequiredFieldValidator ID="reqUser" runat="server"
                                    ControlToValidate="txtUsername"
                                    ErrorMessage="Username is required!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />
                            </div>
                        </div>

                        <!--Password-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Password:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtPassword" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 400px;"
                                    TextMode="Password"
                                    placeholder="Enter Password" />

                                <asp:RequiredFieldValidator ID="reqPwd" runat="server"
                                    ControlToValidate="txtPassword"
                                    ErrorMessage="Password is required!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />

                                <asp:RegularExpressionValidator ID="valPwdLength" runat="server"
                                    ControlToValidate="txtPassword"
                                    ValidationExpression="^.{7,}$"
                                    ErrorMessage="Password must be at least 7 characters long!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />
                            </div>
                        </div>

                        <!--Confirm Password-->
                        <div class="mb-3">
                            <label class="form-label d-block mx-auto" style="max-width: 420px;">Confirm Password:</label>

                            <div class="d-flex justify-content-center gap-2">
                                <asp:TextBox ID="txtConfirmPassword" runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 400px;"
                                    TextMode="Password"
                                    placeholder="Confirm Password" />

                                <asp:RequiredFieldValidator ID="reqPwd2" runat="server"
                                    ControlToValidate="txtConfirmPassword"
                                    ErrorMessage="Confirm password is required!"
                                    Text="*"
                                    Font-Bold="true"
                                    ForeColor="Red" />

                                <asp:CompareValidator ID="cmpPwd" runat="server"
                                    ControlToValidate="txtConfirmPassword"
                                    ControlToCompare="txtPassword"
                                    ErrorMessage="Passwords do not match!"
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

                        <!--Register button-->
                        <div class="d-flex justify-content-center mt-3">
                            <asp:Button ID="btnRegister"
                                runat="server"
                                Text="Register"
                                CssClass="btn btn-primary"
                                Style="width: 420px;"
                                OnClick="btnRegister_Click" />
                        </div>

                        <!--Login Link-->
                        <div class="text-center mt-3">
                            <small>
                                <a href="Login.aspx">Already have an account? Login here!
                                </a>
                            </small>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
