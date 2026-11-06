<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="BillReminders.aspx.cs"
    Inherits="Expense_Tracker.BillReminders" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <!-- Basic page configuration -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Bill Reminders | Expense Tracker</title>

    <!-- Main dashboard stylesheet -->
    <link href="Content/dashboard.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">

        <!-- Main application container -->
        <div class="app-container">

            <!-- ================= SIDEBAR ================= -->
            <aside class="sidebar">

                <div>
                    <!-- Application logo and brand name -->
                    <div class="brand-header">
                        <div class="brand-icon-box">
                            <svg viewBox="0 0 24 24" class="rupee-icon">
                                <path d="M6 3h12M6 8h12M6 13l8.5 8M6 13h3a4 4 0 0 0 4-4 4 4 0 0 0-4-4H6"></path>
                            </svg>
                        </div>

                        <span class="brand-title">Expense Tracker</span>
                    </div>

                    <!-- Main navigation menu -->
                    <ul class="nav-menu">

                        <!-- Dashboard -->
                        <li class="nav-item">
                            <a href="Dashboard.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <rect width="7" height="7" x="3" y="3" rx="1"/>
                                        <rect width="7" height="7" x="14" y="3" rx="1"/>
                                        <rect width="7" height="7" x="14" y="14" rx="1"/>
                                        <rect width="7" height="7" x="3" y="14" rx="1"/>
                                    </svg>
                                </span>
                                1. Dashboard
                            </a>
                        </li>

                        <!-- All Transactions -->
                        <li class="nav-item">
                            <a href="Transactions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <path d="m17 2 4 4-4 4"/>
                                        <path d="M3 11v-1a4 4 0 0 1 4-4h14"/>
                                        <path d="m7 22-4-4 4-4"/>
                                        <path d="M21 13v1a4 4 0 0 1-4 4H3"/>
                                    </svg>
                                </span>
                                2. All Transactions
                            </a>
                        </li>

                        <!-- Categories -->
                        <li class="nav-item">
                            <a href="Categories.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <path d="M21.21 15.89A10 10 0 1 1 8 2.83"/>
                                        <path d="M22 12A10 10 0 0 0 12 2v10z"/>
                                    </svg>
                                </span>
                                3. Categories Breakdown
                            </a>
                        </li>

                        <!-- Monthly Budget -->
                        <li class="nav-item">
                            <a href="Budget.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <line x1="12" x2="12" y1="2" y2="22"/>
                                        <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
                                    </svg>
                                </span>
                                4. Monthly Budget
                            </a>
                        </li>

                        <!-- Log Expense -->
                        <li class="nav-item">
                            <a href="LogExpense.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <rect width="20" height="14" x="2" y="5" rx="2"/>
                                        <line x1="2" x2="22" y1="10" y2="10"/>
                                    </svg>
                                </span>
                                5. Log Expense
                            </a>
                        </li>

                        <!-- Income -->
                        <li class="nav-item">
                            <a href="Income.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <rect width="20" height="14" x="2" y="7" rx="2"/>
                                        <path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/>
                                    </svg>
                                </span>
                                6. Add Salary / Income
                            </a>
                        </li>

                        <!-- Search -->
                        <li class="nav-item">
                            <a href="Search.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <circle cx="11" cy="11" r="8"/>
                                        <path d="m21 21-4.3-4.3"/>
                                    </svg>
                                </span>
                                7. Search
                            </a>
                        </li>

                        <!-- Currency -->
                        <li class="nav-item">
                            <a href="Currency.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <line x1="12" x2="12" y1="2" y2="22"/>
                                        <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
                                    </svg>
                                </span>
                                8. Currency Settings
                            </a>
                        </li>

                        <!-- Profile -->
                        <li class="nav-item">
                            <a href="Profile.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/>
                                        <circle cx="12" cy="7" r="4"/>
                                    </svg>
                                </span>
                                9. Profile &amp; Settings
                            </a>
                        </li>

                        <!-- Subscriptions -->
                        <li class="nav-item">
                            <a href="Subscriptions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <rect width="20" height="14" x="2" y="5" rx="2"/>
                                        <line x1="2" x2="22" y1="10" y2="10"/>
                                    </svg>
                                </span>
                                10. Subscriptions
                            </a>
                        </li>

                        <!-- Active Bill Reminders page -->
                        <li class="nav-item active">
                            <a href="BillReminders.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <path d="M17 8h1a4 4 0 1 1 0 8h-1"/>
                                        <path d="M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z"/>
                                        <line x1="6" x2="6" y1="2" y2="4"/>
                                        <line x1="10" x2="10" y1="2" y2="4"/>
                                        <line x1="14" x2="14" y1="2" y2="4"/>
                                    </svg>
                                </span>
                                11. Bill Reminders
                            </a>
                        </li>

                        <!-- Loan Tracker -->
                        <li class="nav-item">
                            <a href="LoanTracker.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <line x1="12" x2="12" y1="2" y2="22"/>
                                        <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
                                    </svg>
                                </span>
                                12. Loan Tracker
                            </a>
                        </li>

                        <!-- Notes and Memos -->
                        <li class="nav-item">
                            <a href="NotesMemos.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24">
                                        <path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/>
                                        <polyline points="14 2 14 8 20 8"/>
                                    </svg>
                                </span>
                                13. Notes &amp; Memos
                            </a>
                        </li>

                    </ul>
                </div>

                <!-- User logout section -->
                <div class="sidebar-footer">
                    <a href="Login.aspx" class="logout-btn">
                        <svg viewBox="0 0 24 24">
                            <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/>
                            <polyline points="16 17 21 12 16 7"/>
                            <line x1="21" x2="9" y1="12" y2="12"/>
                        </svg>
                        Logout
                    </a>
                </div>

            </aside>

            <!-- ================= MAIN CONTENT ================= -->
            <div class="main-wrapper">

                <!-- Page header -->
                <header class="top-bar">
                    <div class="top-bar-left">

                        <!-- Sidebar toggle button -->
                        <button type="button"
                                class="menu-toggle"
                                aria-label="Toggle navigation menu">
                            <svg viewBox="0 0 24 24">
                                <line x1="4" x2="20" y1="12" y2="12"/>
                                <line x1="4" x2="20" y1="6" y2="6"/>
                                <line x1="4" x2="20" y1="18" y2="18"/>
                            </svg>
                        </button>

                        <h1 class="page-title">Bill Reminders</h1>
                    </div>

                    <!-- Logged-in user information -->
                    <div class="top-bar-right">
                        <a href="Profile.aspx"
                           class="user-profile"
                           title="View Profile Settings">

                            <div class="user-avatar">
                                <asp:Literal ID="litUserInitial" runat="server">T</asp:Literal>
                            </div>

                            <span class="user-name">
                                <asp:Literal ID="litUserName" runat="server">tushar</asp:Literal>
                            </span>

                        </a>
                    </div>
                </header>

                <!-- ================= PAGE BODY ================= -->
                <main class="content-body">

                    <div class="log-expense-grid">

                        <!-- Add a new bill reminder -->
                        <div class="log-expense-card">

                            <h2 class="budget-card-title"
                                style="margin-bottom: 22px;">
                                Add New Bill Reminder
                            </h2>

                            <!-- Bill name input -->
                            <div class="form-group">
                                <label class="form-label-upper"
                                       for="txtBillName">
                                    BILL NAME
                                </label>

                                <asp:TextBox ID="txtBillName"
                                    runat="server"
                                    CssClass="form-input-dark"
                                    placeholder="e.g. Electricity, Rent, Internet..."
                                    MaxLength="100">
                                </asp:TextBox>
                            </div>

                            <!-- Amount and due date fields -->
                            <div class="form-row-2col">

                                <!-- Bill amount -->
                                <div class="form-group">
                                    <label class="form-label-upper"
                                           for="txtBillAmount">
                                        AMOUNT (&#8377;)
                                    </label>

                                    <asp:TextBox ID="txtBillAmount"
                                        runat="server"
                                        CssClass="form-input-dark"
                                        placeholder="Enter amount"
                                        TextMode="Number">
                                    </asp:TextBox>
                                </div>

                                <!-- Bill due date -->
                                <div class="form-group">
                                    <label class="form-label-upper"
                                           for="txtBillDueDate">
                                        DUE DATE
                                    </label>

                                    <asp:TextBox ID="txtBillDueDate"
                                        runat="server"
                                        TextMode="Date"
                                        CssClass="form-input-dark">
                                    </asp:TextBox>
                                </div>

                            </div>

                            <!-- Button to save the reminder -->
                            <asp:Button ID="btnAddReminder"
                                runat="server"
                                Text="Save Bill Reminder"
                                CssClass="btn-purple-full"
                                OnClick="btnAddReminder_Click" />

                        </div>

                        <!-- ================= BILL LIST ================= -->
                        <div class="recent-expenses-card">

                            <h2 class="budget-card-title"
                                style="margin-bottom: 20px;">
                                Your Upcoming Bills
                            </h2>

                            <div class="subscriptions-list">

                                <!-- Display all saved bill reminders -->
                                <asp:Repeater ID="rptBills"
                                    runat="server"
                                    OnItemCommand="rptBills_ItemCommand">

                                    <ItemTemplate>

                                        <div class="subscription-item-card">

                                            <!-- Bill information -->
                                            <div class="sub-left">

                                                <div class="sub-title">
                                                    <%# Eval("Name") %>
                                                </div>

                                                <!-- Formatted due date -->
                                                <div class="sub-due">
                                                    <%# Eval("DueDateDisplay") %>
                                                </div>

                                            </div>

                                            <!-- Bill status and actions -->
                                            <div class="sub-right">

                                                <!-- Due / Upcoming status -->
                                                <span class='<%# Eval("StatusBadgeClass") %>'>
                                                    <%# Eval("Status") %>
                                                </span>

                                                <!-- Formatted bill amount -->
                                                <span class="sub-amount">
                                                    <%# Eval("FormattedAmount") %>
                                                </span>

                                                <!-- Delete bill reminder -->
                                                <asp:LinkButton ID="btnDelete"
                                                    runat="server"
                                                    CommandName="DeleteBill"
                                                    CommandArgument='<%# Eval("Id") %>'
                                                    CssClass="btn-delete-sub"
                                                    ToolTip="Delete this bill reminder"
                                                    CausesValidation="false">

                                                    <svg viewBox="0 0 24 24"
                                                        width="16"
                                                        height="16"
                                                        stroke="currentColor"
                                                        stroke-width="2"
                                                        fill="none">

                                                        <polyline points="3 6 5 6 21 6"></polyline>

                                                        <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>

                                                    </svg>

                                                </asp:LinkButton>

                                            </div>

                                        </div>

                                    </ItemTemplate>

                                </asp:Repeater>

                                <!-- Message displayed when no bills exist -->
                                <asp:Panel ID="pnlNoBills"
                                    runat="server"
                                    Visible="false"
                                    style="text-align:center; padding:40px 20px; color:#8a90a6;">

                                    <p>No bill reminders found. Add your first bill reminder!</p>

                                </asp:Panel>

                            </div>

                        </div>

                    </div>

                </main>

            </div>

        </div>

    </form>
</body>
</html>
