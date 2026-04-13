<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardGenerator.aspx.cs" Inherits="Kakuro.Views.BoardGenerator" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="staticPnl" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="lnkGenerate" runat="server" OnClick="lnkGenerate_Click">Generate Puzzle</asp:LinkButton>
            <asp:Button ID="btnToolBlack" runat="server" Text="Black Tool" OnClick="btnToolBlack_Click" />
            <asp:Button ID="btnToolWhite" runat="server" Text="White Tool" OnClick="btnToolWhite_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            
            </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkGenerate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnToolBlack" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnToolWhite" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

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