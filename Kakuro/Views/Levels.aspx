<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="Levels.aspx.cs" Inherits="Kakuro.Views.Levels" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Levels.css?v=1" rel="stylesheet" type="text/css" />


    <div class="z-stack-container position-relative">
        <asp:Panel ID="pnlLevels" runat="server" CssClass="v-stack-container d-flex flex-column align-items-center gap-2">
            <asp:Button ID="btn25" runat="server" Text="1" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn26" runat="server" Text="2" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn27" runat="server" Text="3" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn28" runat="server" Text="4" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn29" runat="server" Text="5" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn30" runat="server" Text="6" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn31" runat="server" Text="7" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn32" runat="server" Text="8" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn33" runat="server" Text="9" CssClass="btn-level" OnClick="btn_Click"/>
            <asp:Button ID="btn34" runat="server" Text="10" CssClass="btn-level" OnClick="btn_Click"/>
        </asp:Panel>
    </div>
</asp:Content>