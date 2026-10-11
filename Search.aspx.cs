using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Search : Page
    {
        private string CurrentFilter
        {
            get { return ViewState["SearchFilter"] as string ?? "All"; }
            set { ViewState["SearchFilter"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                CurrentFilter = "All";
                UpdatePillStyles();
                PerformSearch();
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

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton btn)
            {
                CurrentFilter = btn.CommandArgument;
                UpdatePillStyles();
                PerformSearch();
            }
        }

        protected void txtSearchQuery_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void UpdatePillStyles()
        {
            btnFilterAll.CssClass = CurrentFilter == "All" ? "search-pill active" : "search-pill";
            btnFilterIncome.CssClass = CurrentFilter == "Income" ? "search-pill active" : "search-pill";
            btnFilterExpense.CssClass = CurrentFilter == "Expense" ? "search-pill active" : "search-pill";
        }

        private void PerformSearch()
        {
            string query = txtSearchQuery.Text.Trim();

            if (string.IsNullOrWhiteSpace(query))
            {
                pnlEmptyState.Visible = true;
                pnlNoResults.Visible = false;
                pnlResults.Visible = false;
                return;
            }

            var results = ExpenseDataService.SearchTransactions(query, CurrentFilter);

            if (results != null && results.Count > 0)
            {
                pnlEmptyState.Visible = false;
                pnlNoResults.Visible = false;
                pnlResults.Visible = true;
                rptSearchResults.DataSource = results;
                rptSearchResults.DataBind();
            }
            else
            {
                pnlEmptyState.Visible = false;
                pnlNoResults.Visible = true;
                pnlResults.Visible = false;
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
                case "investment":
                    return "<svg viewBox='0 0 24 24'><polyline points='22 7 13.5 15.5 8.5 10.5 2 17'></polyline><polyline points='16 7 22 7 22 13'></polyline></svg>";
                case "freelance":
                    return "<svg viewBox='0 0 24 24'><rect width='18' height='12' x='3' y='4' rx='2'></rect><line x1='2' x2='22' y1='20' y2='20'></line></svg>";
                case "salary":
                default:
                    return "<svg viewBox='0 0 24 24'><rect width='20' height='14' x='2' y='7' rx='2'></rect><path d='M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16'></path></svg>";
            }
        }

        public string GetIconBoxClass(string iconType, bool isIncome)
        {
            if (isIncome)
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
            return "txn-icon-circle";
        }
    }
}
