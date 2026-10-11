using System;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class LogExpense : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindRecentExpenses();
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

        private void BindRecentExpenses()
        {
            var recentExpenses = ExpenseDataService.GetRecentExpenses(4);
            if (recentExpenses != null && recentExpenses.Count > 0)
            {
                rptRecentExpenses.DataSource = recentExpenses;
                rptRecentExpenses.DataBind();
                rptRecentExpenses.Visible = true;
                pnlNoExpenses.Visible = false;
            }
            else
            {
                rptRecentExpenses.Visible = false;
                pnlNoExpenses.Visible = true;
            }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            string title = txtExpenseTitle.Text.Trim();
            string category = ddlExpenseCategory.SelectedValue;
            string dateRaw = txtExpenseDate.Text.Trim();
            string note = txtExpenseNote.Text.Trim();

            if (decimal.TryParse(txtExpenseAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    title = category;
                }

                string dateFormatted = dateRaw;
                if (DateTime.TryParse(dateRaw, out DateTime parsedDate))
                {
                    dateFormatted = parsedDate.ToString("d/M/yyyy");
                }

                ExpenseDataService.AddTransaction(title, category, amount, false, dateFormatted, note);

                txtExpenseTitle.Text = "";
                txtExpenseAmount.Text = "";
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtExpenseNote.Text = "";

                BindRecentExpenses();
            }
        }

        protected void btnSubmitIncome_Click(object sender, EventArgs e)
        {
            string title = txtIncomeTitle.Text.Trim();
            string category = ddlIncomeCategory.SelectedValue;
            string date = txtIncomeDate.Text.Trim();

            if (decimal.TryParse(txtIncomeAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(title)) title = "Income";
                ExpenseDataService.AddTransaction(title, category, amount, true, date);
                txtIncomeTitle.Text = "";
                txtIncomeAmount.Text = "";
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindRecentExpenses();
            }
        }

        public string GetIconSvg(string iconType)
        {
            switch (iconType?.ToLower())
            {
                case "food":
                    return "<svg viewBox='0 0 24 24'><path d='M18 8h1a4 4 0 1 1 0 8h-1'></path><path d='M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z'></path><line x1='6' x2='6' y1='2' y2='4'></line><line x1='10' x2='10' y1='2' y2='4'></line><line x1='14' x2='14' y1='2' y2='4'></line></svg>";
                case "shopping":
                    return "<svg viewBox='0 0 24 24'><path d='M6 2 3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4Z'></path><path d='M3 6h18'></path><path d='M16 10a4 4 0 0 1-8 0'></path></svg>";
                case "transport":
                    return "<svg viewBox='0 0 24 24'><rect width='16' height='16' x='4' y='4' rx='2'></rect><path d='M4 10h16'></path><path d='M10 4v16'></path></svg>";
                case "salary":
                    return "<svg viewBox='0 0 24 24'><rect width='20' height='14' x='2' y='7' rx='2'></rect><path d='M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16'></path></svg>";
                default:
                    return "<svg viewBox='0 0 24 24'><circle cx='12' cy='12' r='10'></circle></svg>";
            }
        }
    }
}
