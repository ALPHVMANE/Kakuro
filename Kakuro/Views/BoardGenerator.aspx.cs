using Kakuro.Model;
using System;

namespace Kakuro.Views
{
    public partial class BoardGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" +
                    Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");

                SQLManager sqlm = new SQLManager(connStr);
                PuzzleManager pm = new PuzzleManager(sqlm);
                BoardService service = new BoardService(pm);

                try
                {
                    Board board = service.GenerateBoard(Session);
                    Session["BoardGen"] = board;

                    Response.Redirect("~/Views/Gameplay.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('{ex.Message}');</script>");
                }
            }
        }
    }
}