<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Views/Site.Master" CodeBehind="BoardGenerator.aspx.cs" Inherits="Kakuro.Views.BoardGenerator" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="staticPnl">
        <ProgressTemplate>
            <div class="loadingOverlay">
                <div class="loadingBox">
                    <div class="spinner"></div>
                    <p>Generating your puzzle...</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>