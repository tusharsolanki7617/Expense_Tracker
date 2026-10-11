using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Subscriptions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                txtSubDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindSubscriptions();
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

        private void BindSubscriptions()
        {
            var subs = ExpenseDataService.GetSubscriptions();
            if (subs != null && subs.Count > 0)
            {
                rptSubscriptions.DataSource = subs;
                rptSubscriptions.DataBind();
                rptSubscriptions.Visible = true;
                pnlNoSubscriptions.Visible = false;
            }
            else
            {
                rptSubscriptions.Visible = false;
                pnlNoSubscriptions.Visible = true;
            }
        }

        protected void btnAddSubscription_Click(object sender, EventArgs e)
        {
            string name = txtSubName.Text.Trim();
            string dueDate = txtSubDueDate.Text.Trim();

            if (decimal.TryParse(txtSubAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Subscription";
                }

                if (string.IsNullOrWhiteSpace(dueDate))
                {
                    dueDate = DateTime.Now.ToString("d/M/yyyy");
                }

                ExpenseDataService.AddSubscription(name, amount, dueDate);

                txtSubName.Text = "";
                txtSubAmount.Text = "";
                txtSubDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                BindSubscriptions();
            }
        }

        protected void rptSubscriptions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSub")
            {
                if (int.TryParse(e.CommandArgument?.ToString(), out int id))
                {
                    ExpenseDataService.DeleteSubscription(id);
                    BindSubscriptions();
                }
            }
        }
    }
}
