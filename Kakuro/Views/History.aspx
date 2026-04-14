<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="History.aspx.cs" Inherits="Kakuro.Views.History" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Levels.css?v=1" rel="stylesheet" type="text/css" />

    <div class="container mt-4">
        <asp:Panel ID="pnlHistory" runat="server" CssClass="card shadow-sm p-4">
            <h2 class="mb-4 text-center">Game History</h2>
            
            <asp:ListView ID="ListView" runat="server" DataKeyNames="SessionID">
                <LayoutTemplate>
                    <div class="table-responsive">
                        <table class="table table-hover table-striped align-middle">
                            <thead class="table-dark">
                                <tr>
                                    <th>Session</th>
                                    <th>Board</th>
                                    <th>Status</th>
                                    <th>Score</th>
                                    <th>Errors</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </tbody>
                        </table>
                    </div>
                    
                    <div class="d-flex justify-content-center mt-3">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="10" CssClass="btn-group">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" 
                                    ButtonCssClass="btn btn-outline-secondary btn-sm" />
                                <asp:NumericPagerField ButtonCount="5" 
                                    NumericButtonCssClass="btn btn-outline-secondary btn-sm" 
                                    CurrentPageLabelCssClass="btn btn-primary btn-sm disabled" 
                                    NextPreviousButtonCssClass="btn btn-outline-secondary btn-sm" />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" 
                                    ButtonCssClass="btn btn-outline-secondary btn-sm" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </LayoutTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("SessionID") %></td>
                        <td><span class="badge bg-info text-dark">#<%# Eval("BoardID") %></span></td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Eval("Status") %>' Enabled="false" CssClass="form-check-input" />
                            <span class="ms-2 small text-muted"><%# (bool)Eval("Status") ? "Completed" : "In Progress" %></span>
                        </td>
                        <td class="fw-bold text-success"><%# Eval("Score") %></td>
                        <td class="text-danger"><%# Eval("Errors") %></td>
                        <td>
                            <asp:LinkButton ID="btnResume" runat="server" 
                                OnClick="btnResume_Click" 
                                CommandArgument='<%# Eval("BoardID") %>' 
                                CssClass="btn btn-sm btn-outline-primary me-1"
                                Visible='<%# !(bool)Eval("Status") %>'>
                                <i class="bi bi-play-fill"></i> Resume
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnReplay" runat="server" 
                                OnClick="btnReplay_Click" 
                                CommandArgument='<%# Eval("BoardID") %>' 
                                CssClass="btn btn-sm btn-outline-success">
                                <i class="bi bi-arrow-repeat"></i> Replay
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate> 

                <EmptyDataTemplate>
                    <div class="alert alert-warning text-center">
                        No game history found for this user.
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
    </div>
</asp:Content>