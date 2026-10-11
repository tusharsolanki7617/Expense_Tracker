using System;
using System.Collections.Generic;
using System.Web.UI;
using Expense_Tracker.Models;

namespace Expense_Tracker
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
                LoadDashboardData();
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadUserProfile()
        {
            string userName = Session["UserName"] as string;
            if (string.IsNullOrWhiteSpace(userName))
            {
                string email = Session["UserEmail"] as string;
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                {
                    userName = email.Split('@')[0];
                }
                else
                {
                    userName = "tushar";
                }
            }

            litUserName.Text = Server.HtmlEncode(userName);
            string initial = "T";
            string trimmed = userName.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                initial = char.ToUpper(trimmed[0]).ToString();
            }
            litUserInitial.Text = Server.HtmlEncode(initial);
        }

        private void LoadDashboardData()
        {
            decimal totalIncome = Expense_Tracker.Services.ExpenseDataService.GetTotalIncome();
            decimal totalExpenses = Expense_Tracker.Services.ExpenseDataService.GetTotalExpenses();
            decimal netBalance = Expense_Tracker.Services.ExpenseDataService.GetNetBalance();
            int totalTransactions = Expense_Tracker.Services.ExpenseDataService.GetTransactionCount();

            string sym = Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol();

            // Bind Hero Balance
            litNetBalanceHero.Text = sym + netBalance.ToString("N2");
            litIncomeHero.Text = sym + totalIncome.ToString("N2");
            litExpenseHero.Text = sym + totalExpenses.ToString("N2");

            // Bind Summary Card
            litSummaryIncome.Text = sym + totalIncome.ToString("N2");
            litSummaryExpense.Text = sym + totalExpenses.ToString("N2");
            litSummaryNet.Text = sym + netBalance.ToString("N2");

            // Bind Bottom Metric Cards
            litBottomBalance.Text = sym + netBalance.ToString("N2");
            litBottomIncome.Text = sym + totalIncome.ToString("N2");
            litBottomExpense.Text = sym + totalExpenses.ToString("N2");
            litBottomCount.Text = totalTransactions.ToString();

            // Bind Recent Activity dynamically
            var recentList = Expense_Tracker.Services.ExpenseDataService.GetRecentTransactions(4);
            if (recentList != null && recentList.Count > 0)
            {
                rptRecentActivity.DataSource = recentList;
                rptRecentActivity.DataBind();
                rptRecentActivity.Visible = true;
                phEmptyActivity.Visible = false;
            }
            else
            {
                rptRecentActivity.Visible = false;
                phEmptyActivity.Visible = true;
            }
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            string title = txtExpenseTitle.Text.Trim();
            string category = ddlExpenseCategory.SelectedValue;
            string date = txtExpenseDate.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(title))
            {
                title = "Expense";
            }

            if (decimal.TryParse(txtExpenseAmount.Text.Trim(), out amount) && amount > 0)
            {
                Expense_Tracker.Services.ExpenseDataService.AddTransaction(title, category, amount, false, date);
                txtExpenseTitle.Text = "";
                txtExpenseAmount.Text = "";
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadDashboardData();
            }
        }

        protected void btnSaveIncome_Click(object sender, EventArgs e)
        {
            string title = txtIncomeTitle.Text.Trim();
            string category = ddlIncomeCategory.SelectedValue;
            string date = txtIncomeDate.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(title))
            {
                title = "Income";
            }

            if (decimal.TryParse(txtIncomeAmount.Text.Trim(), out amount) && amount > 0)
            {
                Expense_Tracker.Services.ExpenseDataService.AddTransaction(title, category, amount, true, date);
                txtIncomeTitle.Text = "";
                txtIncomeAmount.Text = "";
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadDashboardData();
            }
        }

        public string GetCategoryIconSvg(string iconType)
        {
            switch (iconType?.ToLower())
            {
                case "food":
                    return "<svg viewBox='0 0 24 24'><path d='M18 2v20M2 2v8a4 4 0 0 0 4 4v8M6 2v6M10 2v6'/></svg>";
                case "shopping":
                    return "<svg viewBox='0 0 24 24'><path d='M6 2 3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4Z'/><line x1='3' x2='21' y1='6' y2='6'/><path d='M16 10a4 4 0 0 1-8 0'/></svg>";
                case "transport":
                    return "<svg viewBox='0 0 24 24'><rect width='18' height='14' x='3' y='4' rx='3'/><path d='M7 18v2M17 18v2M3 11h18M7 7h.01M17 7h.01'/></svg>";
                case "salary":
                    return "<svg viewBox='0 0 24 24'><rect width='20' height='14' x='2' y='7' rx='2'/><path d='M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16'/></svg>";
                default:
                    return "<svg viewBox='0 0 24 24'><circle cx='12' cy='12' r='10'/></svg>";
            }
        }
    }
}
