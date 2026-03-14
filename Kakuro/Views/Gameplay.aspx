<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="Gameplay.aspx.cs" Inherits="Kakuro.Gameplay" %>

<asp:Content ID="GameplayContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="upPuzzle" runat="server"> <%--Update Panel => timer--%>
        <ContentTemplate>

            <hr />
                <asp:Table ID="KakuroTable" runat="server" CssClass="table table-bordered text-center kakuro-grid">
                </asp:Table>
            </div>
            
            <div class="mt-3">
                <asp:Button ID="CheckSolutionButton" runat="server" Text="Check Solution" 
                    CssClass="btn btn-primary" OnClick="btnCheckSolution_Click" />
                <asp:Label ID="ResultLabel" runat="server" CssClass="ml-3 font-weight-bold" Text=""/>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel> 
</asp:Content>
