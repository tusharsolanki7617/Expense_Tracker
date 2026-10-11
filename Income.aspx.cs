using System;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Income : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindRecentIncomes();
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

        private void BindRecentIncomes()
        {
            var recentIncomes = ExpenseDataService.GetRecentIncomes(4);
            if (recentIncomes != null && recentIncomes.Count > 0)
            {
                rptRecentIncomes.DataSource = recentIncomes;
                rptRecentIncomes.DataBind();
                rptRecentIncomes.Visible = true;
                pnlNoIncomes.Visible = false;
            }
            else
            {
                rptRecentIncomes.Visible = false;
                pnlNoIncomes.Visible = true;
            }
        }

        protected void btnAddIncome_Click(object sender, EventArgs e)
        {
            string source = txtIncomeSource.Text.Trim();
            string category = ddlIncomeCategory.SelectedValue;
            string date = txtIncomeDate.Text.Trim();
            string note = txtIncomeNote.Text.Trim();

            if (decimal.TryParse(txtIncomeAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    source = category;
                }

                if (string.IsNullOrWhiteSpace(date))
                {
                    date = DateTime.Now.ToString("d/M/yyyy");
                }

                ExpenseDataService.AddTransaction(source, category, amount, true, date, note);

                txtIncomeSource.Text = "";
                txtIncomeAmount.Text = "";
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeNote.Text = "";

                BindRecentIncomes();
            }
        }

        public string GetIconSvg(string iconType)
        {
            switch (iconType?.ToLower())
            {
                case "investment":
                    return "<svg viewBox='0 0 24 24'><polyline points='22 7 13.5 15.5 8.5 10.5 2 17'></polyline><polyline points='16 7 22 7 22 13'></polyline></svg>";
                case "freelance":
                    return "<svg viewBox='0 0 24 24'><rect width='18' height='12' x='3' y='4' rx='2'></rect><line x1='2' x2='22' y1='20' y2='20'></line></svg>";
                case "salary":
                default:
                    return "<svg viewBox='0 0 24 24'><rect width='20' height='14' x='2' y='7' rx='2'></rect><path d='M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16'></path></svg>";
            }
        }

        public string GetIconBoxClass(string iconType)
        {
            switch (iconType?.ToLower())
            {
                case "investment":
                    return "txn-icon-circle icon-box-pink";
                case "freelance":
                    return "txn-icon-circle icon-box-blue";
                case "salary":
                default:
                    return "txn-icon-circle icon-box-orange";
            }
        }
    }
}
