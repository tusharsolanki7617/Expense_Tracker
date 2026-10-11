using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Budget : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                BindBudgets();
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

        private void BindBudgets()
        {
            var budgetList = ExpenseDataService.GetBudgetStatusList();

            if (budgetList != null && budgetList.Count > 0)
            {
                rptBudgets.DataSource = budgetList;
                rptBudgets.DataBind();
                rptBudgets.Visible = true;
                pnlNoBudgets.Visible = false;
            }
            else
            {
                rptBudgets.Visible = false;
                pnlNoBudgets.Visible = true;
            }
        }

        protected void btnSetBudget_Click(object sender, EventArgs e)
        {
            string category = ddlBudgetCategory.SelectedValue;
            if (decimal.TryParse(txtBudgetAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                ExpenseDataService.SetBudget(category, amount);
                txtBudgetAmount.Text = "";
                BindBudgets();
            }
        }

        protected void rptBudgets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveBudget")
            {
                string category = e.CommandArgument?.ToString();
                if (!string.IsNullOrEmpty(category))
                {
                    ExpenseDataService.RemoveBudget(category);
                    BindBudgets();
                }
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
                BindBudgets();
            }
        }
    }
}
