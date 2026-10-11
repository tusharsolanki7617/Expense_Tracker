using System;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblProfileMessage.Visible = false;
                lblPasswordMessage.Visible = false;
                LoadUserProfileData();
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadUserProfileData()
        {
            string userName = Session["UserName"] as string;
            string userEmail = Session["UserEmail"] as string;
            string userPhone = Session["UserPhone"] as string;

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "tushar";
            }
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                userEmail = "tushar@gmail.com";
            }
            if (string.IsNullOrWhiteSpace(userPhone))
            {
                userPhone = "+91 00000 00000";
            }

            txtFullName.Text = userName;
            txtEmail.Text = userEmail;
            txtPhone.Text = userPhone;

            UpdateAvatarAndTopBar(userName);
        }

        private void UpdateAvatarAndTopBar(string userName)
        {
            litUserNameTop.Text = Server.HtmlEncode(userName);
            string initial = "T";
            string trimmed = userName.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                initial = char.ToUpper(trimmed[0]).ToString();
            }
            litUserInitialTop.Text = Server.HtmlEncode(initial);
            litBigAvatarInitial.Text = Server.HtmlEncode(initial);
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string newName = txtFullName.Text.Trim();
            string newEmail = txtEmail.Text.Trim();
            string newPhone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                ShowProfileMessage("Full Name cannot be empty.", false);
                return;
            }

            string oldEmail = Session["UserEmail"] as string ?? "tushar@gmail.com";
            ExpenseDataService.UpdateUserProfile(oldEmail, newName, newEmail, newPhone);

            Session["UserName"] = newName;
            Session["UserEmail"] = newEmail;
            Session["UserPhone"] = newPhone;

            UpdateAvatarAndTopBar(newName);

            ShowProfileMessage("Profile updated successfully!", true);
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string currentPass = txtCurrentPassword.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirmPass = txtConfirmNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPass) || newPass.Length < 6)
            {
                ShowPasswordMessage("New password must be at least 6 characters.", false);
                return;
            }

            if (newPass != confirmPass)
            {
                ShowPasswordMessage("New password and confirm password do not match.", false);
                return;
            }

            string userEmail = Session["UserEmail"] as string ?? "tushar@gmail.com";
            if (!ExpenseDataService.UpdateUserPassword(userEmail, currentPass, newPass, out string err))
            {
                ShowPasswordMessage(err, false);
                return;
            }

            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";

            ShowPasswordMessage("Password updated successfully!", true);
        }

        private void ShowProfileMessage(string message, bool isSuccess)
        {
            lblProfileMessage.Text = message;
            lblProfileMessage.CssClass = isSuccess ? "auth-alert visible success" : "auth-alert visible error";
            lblProfileMessage.Visible = true;
        }

        private void ShowPasswordMessage(string message, bool isSuccess)
        {
            lblPasswordMessage.Text = message;
            lblPasswordMessage.CssClass = isSuccess ? "auth-alert visible success" : "auth-alert visible error";
            lblPasswordMessage.Visible = true;
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
                ExpenseDataService.AddTransaction(title, category, amount, false, date);
                txtExpenseTitle.Text = "";
                txtExpenseAmount.Text = "";
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                ExpenseDataService.AddTransaction(title, category, amount, true, date);
                txtIncomeTitle.Text = "";
                txtIncomeAmount.Text = "";
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
