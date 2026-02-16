<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gameplay.aspx.cs" Inherits="Kakuro.Gameplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="puzzleform" runat="server" >

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upPuzzle" runat="server"> <%--Update Panel => timer--%>
            <ContentTemplate>
               <%-- <asp:Timer ID="timerPuzzle" runat="server" OnTick="timerPuzzle_Tick" Interval="1000" />
                <asp:Label ID="TimeLabel" runat="server" CssClass="h4" Text="Time Elapsed: 0 seconds" />--%>
            
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
    </form>
</body>
</html>
