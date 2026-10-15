<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="Expense_Tracker.Categories" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Categories Breakdown - Expense Tracker</title>
    <link href="Content/dashboard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="app-container">
            <!-- Sidebar Navigation -->
            <aside class="sidebar">
                <div>

                    <!-- Brand / App Logo -->
                    <div class="brand-header">
                        <div class="brand-icon-box">
                            <svg viewBox="0 0 24 24" class="rupee-icon">
                                <path d="M6 3h12M6 8h12M6 13l8.5 8M6 13h3a4 4 0 0 0 4-4 4 4 0 0 0-4-4H6"></path>
                            </svg>
                        </div>
                        <span class="brand-title">Expense Tracker</span>
                    </div>


                    <!-- Navigation Links -->
                    <ul class="nav-menu">
                        <li class="nav-item">
                            <a href="Dashboard.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="7" height="7" x="3" y="3" rx="1"/><rect width="7" height="7" x="14" y="3" rx="1"/><rect width="7" height="7" x="14" y="14" rx="1"/><rect width="7" height="7" x="3" y="14" rx="1"/></svg>
                                </span>
                                1. Dashboard
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Transactions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="m17 2 4 4-4 4"/><path d="M3 11v-1a4 4 0 0 1 4-4h14"/><path d="m7 22-4-4 4-4"/><path d="M21 13v1a4 4 0 0 1-4 4H3"/></svg>
                                </span>
                                2. All Transactions
                            </a>
                        </li>
                        <li class="nav-item active">
                            <a href="Categories.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M21.21 15.89A10 10 0 1 1 8 2.83"/><path d="M22 12A10 10 0 0 0 12 2v10z"/></svg>
                                </span>
                                3. Categories Breakdown
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Budget.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                4. Monthly Budget
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="LogExpense.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                5. Log Expense
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Income.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="7" rx="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>
                                </span>
                                6. Add Salary / Income
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Search.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/></svg>
                                </span>
                                7. Search
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Currency.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                8. Currency Settings
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Profile.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                                </span>
                                9. Profile &amp; Settings
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Subscriptions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                10. Subscriptions
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="BillReminders.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M17 8h1a4 4 0 1 1 0 8h-1"/><path d="M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z"/><line x1="6" x2="6" y1="2" y2="4"/><line x1="10" x2="10" y1="2" y2="4"/><line x1="14" x2="14" y1="2" y2="4"/></svg>
                                </span>
                                11. Bill Reminders
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="LoanTracker.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                12. Loan Tracker
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="NotesMemos.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/></svg>
                                </span>
                                13. Notes &amp; Memos
                            </a>
                        </li>
                    </ul>
                </div>

                <!-- Logout Button -->
                <div class="sidebar-footer">
                    <a href="Login.aspx" class="logout-btn">
                        <svg viewBox="0 0 24 24"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" x2="9" y1="12" y2="12"/></svg>
                        Logout
                    </a>
                </div>
            </aside>

            <!-- Main Content Wrapper -->
            <div class="main-wrapper">
                <!-- Top Header Bar -->
                <header class="top-bar">
                    <div class="top-bar-left">
                        <button type="button" class="menu-toggle" aria-label="Toggle menu">
                            <svg viewBox="0 0 24 24"><line x1="4" x2="20" y1="12" y2="12"/><line x1="4" x2="20" y1="6" y2="6"/><line x1="4" x2="20" y1="18" y2="18"/></svg>
                        </button>
                        <h1 class="page-title">Categories Breakdown</h1>
                    </div>

                    <div class="top-bar-right">
                        <div class="quick-actions">
                            <button type="button" class="btn-quick btn-quick-expense" onclick="openExpenseModal();">
                                <svg viewBox="0 0 24 24"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                                Log Expense
                            </button>
                            <button type="button" class="btn-quick btn-quick-income" onclick="openIncomeModal();">
                                <svg viewBox="0 0 24 24"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                                Add Income
                            </button>
                        </div>

                        <a href="Profile.aspx" class="user-profile" title="Account & Profile Settings">
                            <div class="user-avatar">
                                <asp:Literal ID="litUserInitial" runat="server">T</asp:Literal>
                            </div>
                            <span class="user-name">
                                <asp:Literal ID="litUserName" runat="server">tushar</asp:Literal>
                            </span>
                        </a>
                    </div>
                </header>

                <!-- Page Content Body -->
                <main class="content-body">
                    <div class="categories-container">
                        <!-- Top Card: Custom Categories -->
                        <div class="categories-top-card">
                            <h2 class="card-title" style="margin-bottom:0;">Custom Categories</h2>
                            <button type="button" class="btn-purple-action" onclick="openAddCategoryModal();">
                                <svg viewBox="0 0 24 24" width="16" height="16" stroke="currentColor" stroke-width="2.5" fill="none"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                                Add Category
                            </button>
                        </div>

                        <!-- Main Card: Spending by Category -->
                        <div class="spending-card">
                            <h2 class="card-title" style="margin-bottom: 8px;">Spending by Category</h2>

                            <!-- Dynamic List of Categories with Animated Progress Bars -->
                            <div class="category-breakdown-list">
                                <asp:Repeater ID="rptCategories" runat="server">
                                    <ItemTemplate>
                                        <div class="category-breakdown-row">
                                            <div class="category-row-meta">
                                                <div class="cat-meta-left">
                                                    <span style="display:inline-flex; align-items:center; justify-content:center; color: <%# Eval("BarColor") %>;"><%# Eval("IconSvg") %></span>
                                                    <span><%# Eval("CategoryName") %></span>
                                                </div>
                                                <div class="cat-meta-right">
                                                    <span><%# Eval("FormattedAmount") %></span>
                                                    <span class="cat-percentage"><%# Eval("FormattedPercentage") %></span>
                                                </div>
                                            </div>
                                            <div class="progress-track">
                                                <div class="progress-fill" style="<%# Eval("BarWidthStyle") %>"></div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Panel ID="pnlEmpty" runat="server" Visible="false" style="text-align:center; padding: 40px 20px; color:#8a90a6;">
                                    <p>No expenses recorded yet. Use <strong>Log Expense</strong> to add your first expense!</p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>

        <!-- Add Category Modal -->
        <div id="modalAddCategory" class="modal-overlay">
            <div class="modal-box">
                <div class="modal-header">
                    <h3 class="modal-title">
                        <span style="color:#7c3aed;">●</span> Create New Category
                    </h3>
                    <button type="button" class="modal-close" onclick="closeAddCategoryModal();">&times;</button>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Category Name</label>
                    <asp:TextBox ID="txtNewCategoryName" runat="server" CssClass="modal-input" placeholder="e.g. Subscriptions, Groceries, Fitness"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Color Theme</label>
                    <asp:DropDownList ID="ddlCategoryColor" runat="server" CssClass="modal-select">
                        <asp:ListItem Text="Magenta / Pink (#f43f5e)" Value="#f43f5e"></asp:ListItem>
                        <asp:ListItem Text="Orange (#f59e0b)" Value="#f59e0b"></asp:ListItem>
                        <asp:ListItem Text="Yellow (#eab308)" Value="#eab308"></asp:ListItem>
                        <asp:ListItem Text="Teal / Cyan (#14b8a6)" Value="#14b8a6"></asp:ListItem>
                        <asp:ListItem Text="Blue (#3b82f6)" Value="#3b82f6"></asp:ListItem>
                        <asp:ListItem Text="Purple (#8b5cf6)" Value="#8b5cf6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnSaveCategory" runat="server" Text="Create Category" CssClass="btn-modal-submit btn-purple-action" style="width:100%; justify-content:center;" OnClick="btnSaveCategory_Click" />
            </div>
        </div>

        <!-- Log Expense Modal -->
        <div id="modalExpense" class="modal-overlay">
            <div class="modal-box">
                <div class="modal-header">
                    <h3 class="modal-title">
                        <span style="color:#f59e0b;">●</span> Log New Expense
                    </h3>
                    <button type="button" class="modal-close" onclick="closeExpenseModal();">&times;</button>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Title / Note</label>
                    <asp:TextBox ID="txtExpenseTitle" runat="server" CssClass="modal-input" placeholder="e.g. Dinner, Groceries, Shopping"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Category</label>
                    <asp:DropDownList ID="ddlExpenseCategory" runat="server" CssClass="modal-select">
                        <asp:ListItem Text="Food" Value="Food"></asp:ListItem>
                        <asp:ListItem Text="Shopping" Value="Shopping"></asp:ListItem>
                        <asp:ListItem Text="Utilities" Value="Utilities"></asp:ListItem>
                        <asp:ListItem Text="Health" Value="Health"></asp:ListItem>
                        <asp:ListItem Text="Transport" Value="Transport"></asp:ListItem>
                        <asp:ListItem Text="Gift" Value="Gift"></asp:ListItem>
                        <asp:ListItem Text="Bills" Value="Bills"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Amount (&#8377;)</label>
                    <asp:TextBox ID="txtExpenseAmount" runat="server" CssClass="modal-input" placeholder="e.g. 700.00"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Date</label>
                    <asp:TextBox ID="txtExpenseDate" runat="server" TextMode="Date" CssClass="modal-input"></asp:TextBox>
                </div>
                <asp:Button ID="btnSaveExpense" runat="server" Text="Log Expense" CssClass="btn-modal-submit btn-expense-submit" OnClick="btnSaveExpense_Click" />
            </div>
        </div>

        <!-- Add Income Modal -->
        <div id="modalIncome" class="modal-overlay">
            <div class="modal-box">
                <div class="modal-header">
                    <h3 class="modal-title">
                        <span style="color:#10b981;">●</span> Add Income / Salary
                    </h3>
                    <button type="button" class="modal-close" onclick="closeIncomeModal();">&times;</button>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Title / Source</label>
                    <asp:TextBox ID="txtIncomeTitle" runat="server" CssClass="modal-input" placeholder="e.g. Monthly Salary, Freelance"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Category</label>
                    <asp:DropDownList ID="ddlIncomeCategory" runat="server" CssClass="modal-select">
                        <asp:ListItem Text="Salary" Value="Salary"></asp:ListItem>
                        <asp:ListItem Text="Freelance" Value="Freelance"></asp:ListItem>
                        <asp:ListItem Text="Investment" Value="Investment"></asp:ListItem>
                        <asp:ListItem Text="Bonus" Value="Bonus"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Amount (&#8377;)</label>
                    <asp:TextBox ID="txtIncomeAmount" runat="server" CssClass="modal-input" placeholder="e.g. 23500.00"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Date</label>
                    <asp:TextBox ID="txtIncomeDate" runat="server" TextMode="Date" CssClass="modal-input"></asp:TextBox>
                </div>
                <asp:Button ID="btnSaveIncome" runat="server" Text="Add Income" CssClass="btn-modal-submit btn-income-submit" OnClick="btnSaveIncome_Click" />
            </div>
        </div>

        <script type="text/javascript">
            function openAddCategoryModal() {
                document.getElementById('modalAddCategory').classList.add('active');
            }
            function closeAddCategoryModal() {
                document.getElementById('modalAddCategory').classList.remove('active');
            }
            function openExpenseModal() {
                document.getElementById('modalExpense').classList.add('active');
            }
            function closeExpenseModal() {
                document.getElementById('modalExpense').classList.remove('active');
            }
            function openIncomeModal() {
                document.getElementById('modalIncome').classList.add('active');
            }
            function closeIncomeModal() {
                document.getElementById('modalIncome').classList.remove('active');
            }
            window.onclick = function(event) {
                var catModal = document.getElementById('modalAddCategory');
                var expModal = document.getElementById('modalExpense');
                var incModal = document.getElementById('modalIncome');
                if (event.target == catModal) closeAddCategoryModal();
                if (event.target == expModal) closeExpenseModal();
                if (event.target == incModal) closeIncomeModal();
            }
        </script>
    </form>
</body>
</html>
