<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="Configurations.aspx.cs" Inherits="Kakuro.Views.Configurations" %>

<asp:Content ID="CustomContent" ContentPlaceHolderID="MainContent" runat="server">
        <link href="../Content/Configurations.css" runat="server" rel="stylesheet" type="text/css" />


          <div class="header">
            <h1>Kaku<span>ro</span></h1>
            <p>Puzzle Builder</p>
          </div>

          <asp:Panel ID="pnlConfig" runat="server" CssClass="config-panel">

            <div>
              <div class="section-label">Grid size</div>
              <div class="grid-options" id="gridOptions" runat="server"></div>
            </div>

            <div>
              <div class="section-label">Difficulty</div>
              <div class="diff-options">

                <asp:LinkButton ID="lnkEasy" OnClick="SelectDiff_Click" runat="server" CssClass="diff-btn" CommandArgument="easy">
                  <div class="diff-name">Easy</div>
                  <div class="diff-desc">More white cells, gentle clue density</div>
                  <div class="diff-pips">
                    <div class="pip filled"></div><div class="pip"></div><div class="pip"></div>
                  </div>
                </asp:LinkButton>

                <asp:LinkButton ID="lnkMedium" runat="server" CssClass="diff-btn"
                  OnClick="SelectDiff_Click" CommandArgument="medium">
                  <div class="diff-name">Medium</div>
                  <div class="diff-desc">Balanced layout, moderate constraint</div>
                  <div class="diff-pips">
                    <div class="pip filled"></div><div class="pip filled"></div><div class="pip"></div>
                  </div>
                </asp:LinkButton>

                <asp:LinkButton ID="lnkHard" runat="server" CssClass="diff-btn"
                  OnClick="SelectDiff_Click" CommandArgument="hard">
                  <div class="diff-name">Hard</div>
                  <div class="diff-desc">Dense clues, tight constraint zones</div>
                  <div class="diff-pips">
                    <div class="pip filled"></div><div class="pip filled"></div><div class="pip filled"></div>
                  </div>
                </asp:LinkButton>

              </div>
            </div>

            <asp:Panel ID="pnlSummary" runat="server" CssClass="summary-bar">
              <div class="summary-tags">
                <asp:Label ID="tagSize" runat="server" CssClass="tag"
                  ClientIDMode="Static" Text="No size selected" />
                <asp:Label ID="tagDiff" runat="server" CssClass="tag"
                  ClientIDMode="Static" Text="No difficulty" />
              </div>

              <asp:LinkButton ID="lnkGenerate" runat="server" CssClass="gen-btn"
                Enabled="false" OnClick="GenerateGrid_Click">
                Custom Grid
              </asp:LinkButton>

               <asp:LinkButton ID="lnkSkip" runat="server" CssClass="gen-btn"
                    OnClick="lnkSkip_Click" Enabled="false">
                    Skip Custom Creation
                    <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                        <path d="M2 7h10M8 3l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                </asp:LinkButton>

            </asp:Panel>


          </asp:Panel>

            <asp:ScriptManager ID="ScriptManager1" runat="server" />
          <asp:UpdatePanel ID="staticPnl" runat="server">
               <ContentTemplate>
                    <asp:Panel ID="previewWrap" runat="server" CssClass="grid-preview-wrap" Visible="false">
                        <div class="grid-preview-header">
                          <div class="grid-preview-title" id="previewTitle" runat="server"></div>
                          <div class="legend">
                              <div class="legend-item"><div class="legend-box black"></div> clue</div>
                              <div class="legend-item"><div class="legend-box white"></div> entry</div>
                          </div>
                      </div>

                      <div class="toolbar">
                          <asp:Button ID="btnToolBlack" runat="server" Text="Paint black" CssClass="tool-btn active" OnClick="SetPaintMode_Click" CommandArgument="black" />
                          <asp:Button ID="btnToolWhite" runat="server" Text="Paint white" CssClass="tool-btn" OnClick="SetPaintMode_Click" CommandArgument="white" />
                      </div>

                      <asp:Panel ID="pnlKakuroGrid" runat="server" CssClass="kakuro-grid" />

                      <div class="grid-note">Click any cell to toggle between white (entry) and black (block)</div>

                      <div class="action-row">
                        
                          <asp:LinkButton ID="btnCustom" runat="server" CssClass="action-btn primary" OnClick="PlayCustom_Click">Play Custom Grid</asp:LinkButton>
                      </div>
                    </asp:Panel>
               </ContentTemplate>
          </asp:UpdatePanel>
 
        </div>
</asp:Content>