<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master"
    CodeBehind="CustomKakuro.aspx.cs" Inherits="Kakuro.Views.CustomKakuro" %>

<asp:Content ID="CustomKakuroContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .kakuro-table { border-collapse: collapse; }
        .kakuro-table td { width:40px; height:40px; text-align:center; vertical-align:middle; border:1px solid #bbb; padding:0; }
        .entry-cell input { width:100%; height:100%; border: none; text-align:center; font-size:16px; }
        .clue-cell { background: #f3f4f6; position: relative; }
        .clue-cell .h-clue { position:absolute; right:4px; top:2px; font-size:11px; color:#111; }
        .clue-cell .v-clue { position:absolute; left:4px; bottom:2px; font-size:11px; color:#111; }
        .black-cell { background:#1e293b; }
        .toggle-hint { font-size:12px; color:#444; margin-top:8px; }
        .btn { padding:6px 10px; margin-right:6px; }
    </style>

    <script type="text/javascript">
        function toggleCell(x, y) {
            var typeId = 'celltype_' + x + '_' + y;
            var valId = 'cell_' + x + '_' + y;
            var typeEl = document.getElementById(typeId);
            var valEl = document.getElementById(valId);
            if (!typeEl || !valEl) return;
            if (typeEl.value === '1') {
                typeEl.value = '0';
                valEl.value = '';
                valEl.disabled = true;
                valEl.parentElement.className = 'black-cell';
            } else {
                typeEl.value = '1';
                valEl.disabled = false;
                valEl.parentElement.className = 'entry-cell';
                valEl.focus();
            }
        }
    </script>

    <div style="max-width:980px;margin:12px;">
        <h2>Create Custom Kakuro — Interactive Editor</h2>
        <label>Size X:</label>
        <asp:TextBox ID="TxtSizeX" runat="server" Width="40" Text="5" />
        <label>Size Y:</label>
        <asp:TextBox ID="TxtSizeY" runat="server" Width="40" Text="5" />
        <asp:Button ID="BtnGenerate" runat="server" Text="Generate Editor" 
            OnClick="BtnGenerate_Click" CssClass="btn" />
        <asp:Label ID="ResultLabel" runat="server" Text="" EnableViewState="false" />
        <div class="toggle-hint">
            Click a cell to toggle between black (clue) and entry. Fill entries with 1-9. 
            When done click Save & Play.
        </div>
        <asp:Panel ID="EditorPanel" runat="server" Visible="false" Style="margin-top:12px;">
            <asp:Table ID="EditorTable" runat="server" CssClass="kakuro-table"></asp:Table>
            <br />
            <asp:Button ID="BtnSavePlay" runat="server" Text="Save & Play" 
                OnClick="BtnSavePlay_Click" CssClass="btn btn-success" />
            <asp:Button ID="BtnPreview" runat="server" Text="Preview (compute clues)" 
                OnClick="BtnPreview_Click" CssClass="btn" />
        </asp:Panel>
    </div>
</asp:Content>