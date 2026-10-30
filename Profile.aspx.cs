using System;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    // Profile page handles user profile, password, expense, and income operations.
    public partial class Profile : System.Web.UI.Page
    {
        // Page_Load executes whenever the page loads.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute this block only when the page loads for the first time.
            if (!IsPostBack)
            {
                // Hide profile and password messages initially.
                lblProfileMessage.Visible = false;
                lblPasswordMessage.Visible = false;

                // Load the user's profile information.
                LoadUserProfileData();

                // Set the default expense and income dates to today's date.
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        // Loads the user's profile information from the session.
        private void LoadUserProfileData()
        {
            // Retrieve user information stored in the session.
            string userName = Session["UserName"] as string;
            string userEmail = Session["UserEmail"] as string;
            string userPhone = Session["UserPhone"] as string;

            // Use default values if the username is missing.
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "tushar";
            }

            // Use a default email if the email is missing.
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                userEmail = "tushar@gmail.com";
            }

            // Use a default phone number if the phone number is missing.
            if (string.IsNullOrWhiteSpace(userPhone))
            {
                userPhone = "+91 00000 00000";
            }

            // Display the user's information in the text fields.
            txtFullName.Text = userName;
            txtEmail.Text = userEmail;
            txtPhone.Text = userPhone;

            // Update the user's avatar and top navigation bar.
            UpdateAvatarAndTopBar(userName);
        }

        // Updates the username and avatar initials displayed on the page.
        private void UpdateAvatarAndTopBar(string userName)
        {
            // Display the username safely by encoding HTML characters.
            litUserNameTop.Text = Server.HtmlEncode(userName);

            // Set the default avatar initial.
            string initial = "T";

            // Remove unnecessary spaces from the username.
            string trimmed = userName.Trim();

            // Get the first character of the username if it is not empty.
            if (!string.IsNullOrEmpty(trimmed))
            {
                initial = char.ToUpper(trimmed[0]).ToString();
            }

            // Update the initials in the top bar and large profile avatar.
            litUserInitialTop.Text = Server.HtmlEncode(initial);
            litBigAvatarInitial.Text = Server.HtmlEncode(initial);
        }

        // Handles the Save Profile button click event.
        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            // Read the updated profile details from the text fields.
            string newName = txtFullName.Text.Trim();
            string newEmail = txtEmail.Text.Trim();
            string newPhone = txtPhone.Text.Trim();

            // Check whether the full name is empty.
            if (string.IsNullOrEmpty(newName))
            {
                ShowProfileMessage("Full Name cannot be empty.", false);
                return;
            }

            // Get the user's existing email address.
            string oldEmail = Session["UserEmail"] as string ?? "tushar@gmail.com";

            // Update the user's profile information in the data service.
            ExpenseDataService.UpdateUserProfile(oldEmail, newName, newEmail, newPhone);

            // Store the updated profile information in the session.
            Session["UserName"] = newName;
            Session["UserEmail"] = newEmail;
            Session["UserPhone"] = newPhone;

            // Refresh the username and avatar initials.
            UpdateAvatarAndTopBar(newName);

            // Display a success message.
            ShowProfileMessage("Profile updated successfully!", true);
        }

        // Handles the Update Password button click event.
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            // Read the current, new, and confirmation passwords.
            string currentPass = txtCurrentPassword.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirmPass = txtConfirmNewPassword.Text.Trim();

            // Ensure the new password contains at least six characters.
            if (string.IsNullOrEmpty(newPass) || newPass.Length < 6)
            {
                ShowPasswordMessage("New password must be at least 6 characters.", false);
                return;
            }

            // Check whether the new password and confirmation match.
            if (newPass != confirmPass)
            {
                ShowPasswordMessage("New password and confirm password do not match.", false);
                return;
            }

            // Get the current user's email address from the session.
            string userEmail = Session["UserEmail"] as string ?? "tushar@gmail.com";

            // Update the password and display an error if the operation fails.
            if (!ExpenseDataService.UpdateUserPassword(userEmail, currentPass, newPass, out string err))
            {
                ShowPasswordMessage(err, false);
                return;
            }

            // Clear all password fields after a successful update.
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";

            // Display a success message.
            ShowPasswordMessage("Password updated successfully!", true);
        }

        // Displays a success or error message for profile operations.
        private void ShowProfileMessage(string message, bool isSuccess)
        {
            // Set the message text.
            lblProfileMessage.Text = message;

            // Apply the appropriate CSS class based on the result.
            lblProfileMessage.CssClass = isSuccess
                ? "auth-alert visible success"
                : "auth-alert visible error";

            // Make the message visible.
            lblProfileMessage.Visible = true;
        }

        // Displays a success or error message for password operations.
        private void ShowPasswordMessage(string message, bool isSuccess)
        {
            // Set the message text.
            lblPasswordMessage.Text = message;

            // Apply the appropriate CSS class based on the result.
            lblPasswordMessage.CssClass = isSuccess
                ? "auth-alert visible success"
                : "auth-alert visible error";

            // Make the message visible.
            lblPasswordMessage.Visible = true;
        }

        // Handles the Save Expense button click event.
        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            // Read the expense title, category, and date.
            string title = txtExpenseTitle.Text.Trim();
            string category = ddlExpenseCategory.SelectedValue;
            string date = txtExpenseDate.Text.Trim();

            // Variable to store the expense amount.
            decimal amount;

            // Use a default title if the expense title is empty.
            if (string.IsNullOrEmpty(title))
            {
                title = "Expense";
            }

            // Validate the amount and ensure it is greater than zero.
            if (decimal.TryParse(txtExpenseAmount.Text.Trim(), out amount) && amount > 0)
            {
                // Save the transaction as an expense (isIncome = false).
                ExpenseDataService.AddTransaction(title, category, amount, false, date);

                // Clear the title and amount fields after saving.
                txtExpenseTitle.Text = "";
                txtExpenseAmount.Text = "";

                // Reset the expense date to today's date.
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        // Handles the Save Income button click event.
        protected void btnSaveIncome_Click(object sender, EventArgs e)
        {
            // Read the income title, category, and date.
            string title = txtIncomeTitle.Text.Trim();
            string category = ddlIncomeCategory.SelectedValue;
            string date = txtIncomeDate.Text.Trim();

            // Variable to store the income amount.
            decimal amount;

            // Use a default title if the income title is empty.
            if (string.IsNullOrEmpty(title))
            {
                title = "Income";
            }

            // Validate the amount and ensure it is greater than zero.
            if (decimal.TryParse(txtIncomeAmount.Text.Trim(), out amount) && amount > 0)
            {
                // Save the transaction as income (isIncome = true).
                ExpenseDataService.AddTransaction(title, category, amount, true, date);

                // Clear the title and amount fields after saving.
                txtIncomeTitle.Text = "";
                txtIncomeAmount.Text = "";

                // Reset the income date to today's date.
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
```
