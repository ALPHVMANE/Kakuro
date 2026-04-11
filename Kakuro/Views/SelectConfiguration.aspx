<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master"
    CodeBehind="SelectConfiguration.aspx.cs" Inherits="Kakuro.Views.SelectConfiguration" %>

<asp:Content ID="ConfigContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .size-card {
            cursor: pointer;
            transition: transform 0.15s, box-shadow 0.15s;
            border: 2px solid transparent;
            text-decoration: none;
            display: block;
            color: inherit;
        }
        .size-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 6px 18px rgba(0,0,0,.15);
        }
        .size-card.selected {
            border-color: #0d6efd;
            background-color: #e7f1ff;
            transform: translateY(-4px);
            box-shadow: 0 6px 18px rgba(0,0,0,.15);
        }
        .btn-selected {
            transform: scale(1.05);
            box-shadow: 0 0 0 0.2rem rgba(13,110,253,.25);
        }
    </style>

    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">
                    <div class="card-body p-4">

                        <h3 class="text-center mb-2">Puzzle Configuration</h3>
                        <p class="text-muted text-center mb-4">Choose a board size and difficulty.</p>

                        <h5 class="mb-3">1. Select Board Size</h5>

                        <div class="row row-cols-2 row-cols-sm-3 row-cols-md-4 g-3 mb-4">
                            <div class="col">
                                <asp:LinkButton ID="btnSize4" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="4x4" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">4×4</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize5" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="5x5" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">5×5</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize6" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="6x6" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">6×6</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize7" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="7x7" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">7×7</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize8" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="8x8" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">8×8</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize9" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="9x9" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">9×9</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col">
                                <asp:LinkButton ID="btnSize10" runat="server" CssClass="card size-card text-center p-3"
                                    CommandArgument="10x10" OnClick="SelectSize">
                                    <span class="fs-4 fw-bold">10×10</span>
                                </asp:LinkButton>
                            </div>
                        </div>

                        <h5 class="mb-3">2. Select Difficulty</h5>

                        <div class="d-flex flex-wrap gap-2 mb-4">
                            <asp:Button runat="server" ID="btnEasy" Text="Easy"
                                CssClass="btn btn-outline-success"
                                CommandArgument="Easy" OnClick="SelectDifficulty" />
                            <asp:Button runat="server" ID="btnMedium" Text="Medium"
                                CssClass="btn btn-outline-warning"
                                CommandArgument="Medium" OnClick="SelectDifficulty" />
                            <asp:Button runat="server" ID="btnHard" Text="Hard"
                                CssClass="btn btn-outline-danger"
                                CommandArgument="Hard" OnClick="SelectDifficulty" />
                        </div>

                        <div class="d-flex justify-content-center mt-3">
                            <asp:Button runat="server" CssClass="btn btn-primary"
                                Style="width: 420px;" ID="btnCreate"
                                OnClick="btnCreate_Click" Text="Create Puzzle" />
                        </div>

                        <asp:HiddenField ID="hfSizeX" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfSizeY" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfDiff" runat="server" ClientIDMode="Static" />

                        <div class="text-center mt-3">
                            <asp:Label ID="lblError" runat="server"
                                Text="Please Select a Size and Difficulty"
                                ForeColor="Red" Visible="false" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>