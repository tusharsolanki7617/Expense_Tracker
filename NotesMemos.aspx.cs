using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class NotesMemos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                BindNotes();
            }
        }

        private void LoadUserInfo()
        {
            string userName = Session["UserName"] as string;
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "tushar";
            }

            litUserName.Text = userName;
            litUserInitial.Text = (!string.IsNullOrEmpty(userName) ? userName[0].ToString().ToUpper() : "T");
        }

        private void BindNotes()
        {
            var notes = ExpenseDataService.GetNotes();
            if (notes != null && notes.Count > 0)
            {
                rptNotes.DataSource = notes;
                rptNotes.DataBind();
                rptNotes.Visible = true;
                pnlNoNotes.Visible = false;
            }
            else
            {
                rptNotes.Visible = false;
                pnlNoNotes.Visible = true;
            }
        }

        protected void btnSaveNote_Click(object sender, EventArgs e)
        {
            string content = txtNoteContent.Text.Trim();

            if (!string.IsNullOrWhiteSpace(content))
            {
                ExpenseDataService.AddNote(content);

                txtNoteContent.Text = "";
                BindNotes();
            }
        }

        protected void rptNotes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteNote")
            {
                if (int.TryParse(e.CommandArgument?.ToString(), out int id))
                {
                    ExpenseDataService.DeleteNote(id);
                    BindNotes();
                }
            }
        }
    }
}
