using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class LoanTracker : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                txtLoanDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindLoans();
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

        private void BindLoans()
        {
            var loans = ExpenseDataService.GetLoans();
            if (loans != null && loans.Count > 0)
            {
                rptLoans.DataSource = loans;
                rptLoans.DataBind();
                rptLoans.Visible = true;
                pnlNoLoans.Visible = false;
            }
            else
            {
                rptLoans.Visible = false;
                pnlNoLoans.Visible = true;
            }
        }

        protected void btnAddLoan_Click(object sender, EventArgs e)
        {
            string personOrBank = txtPersonOrBank.Text.Trim();
            string dueDate = txtLoanDueDate.Text.Trim();
            string loanType = ddlLoanType.SelectedValue;

            if (decimal.TryParse(txtLoanAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(personOrBank))
                {
                    personOrBank = "Loan";
                }

                if (string.IsNullOrWhiteSpace(dueDate))
                {
                    dueDate = DateTime.Now.ToString("dd/MM/yyyy");
                }

                ExpenseDataService.AddLoan(personOrBank, amount, dueDate, loanType);

                txtPersonOrBank.Text = "";
                txtLoanAmount.Text = "";
                txtLoanDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                BindLoans();
            }
        }

        protected void rptLoans_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteLoan")
            {
                if (int.TryParse(e.CommandArgument?.ToString(), out int id))
                {
                    ExpenseDataService.DeleteLoan(id);
                    BindLoans();
                }
            }
        }
    }
}
