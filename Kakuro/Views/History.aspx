<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Master" CodeBehind="History.aspx.cs" Inherits="Kakuro.Views.History" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Levels.css?v=1" rel="stylesheet" type="text/css" />

        <asp:Panel ID="pnlHistory" runat="server" CssClass="v-stack-container d-flex flex-column align-items-center gap-2">
            <asp:ListView ID="ListView" runat="server" DataSourceID="KakuroGameDB" DataKeyNames="SessionID">
                <AlternatingItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label ID="SessionIDLabel" runat="server" Text='<%# Eval("SessionID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="BoardIDLabel" runat="server" Text='<%# Eval("BoardID") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Eval("Status") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:Label ID="ScoreLabel" runat="server" Text='<%# Eval("Score") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ErrorsLabel" runat="server" Text='<%# Eval("Errors") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                        <td>
                            <asp:Label ID="SessionIDLabel1" runat="server" Text='<%# Eval("SessionID") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="BoardIDTextBox" runat="server" Text='<%# Bind("BoardID") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Bind("Status") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ScoreTextBox" runat="server" Text='<%# Bind("Score") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ErrorsTextBox" runat="server" Text='<%# Bind("Errors") %>' />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="BoardIDTextBox" runat="server" Text='<%# Bind("BoardID") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Bind("Status") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ScoreTextBox" runat="server" Text='<%# Bind("Score") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ErrorsTextBox" runat="server" Text='<%# Bind("Errors") %>' />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label ID="SessionIDLabel" runat="server" Text='<%# Eval("SessionID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="BoardIDLabel" runat="server" Text='<%# Eval("BoardID") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Eval("Status") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:Label ID="ScoreLabel" runat="server" Text='<%# Eval("Score") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ErrorsLabel" runat="server" Text='<%# Eval("Errors") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                    <tr runat="server" style="">
                                        <th runat="server">SessionID</th>
                                        <th runat="server">BoardID</th>
                                        <th runat="server">Status</th>
                                        <th runat="server">Score</th>
                                        <th runat="server">Errors</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="">
                        <td>
                            />
                        </td>
                        <td>
                            <asp:Label ID="BoardIDLabel" runat="server" Text='<%# Eval("BoardID") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="StatusCheckBox" runat="server" Checked='<%# Eval("Status") %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:Label ID="ScoreLabel" runat="server" Text='<%# Eval("Score") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ErrorsLabel" runat="server" Text='<%# Eval("Errors") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="KakuroGameDB" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [SessionID], [BoardID], [Status], [Score], [Errors] FROM [GameState] WHERE ([UserID] = @UserID)">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="UserID" SessionField="MemberID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:Panel>
</asp:Content>
