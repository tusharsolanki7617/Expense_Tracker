using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Expense_Tracker.Models;

namespace Expense_Tracker.Services
{
    public static class ExpenseDataService
    {
        private static readonly object FileLock = new object();
        private static readonly object BudgetFileLock = new object();

        private static string GetDataFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "expenses.json");
        }

        private static string GetBudgetsFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "budgets.json");
        }

        public static string GetCurrentEffectiveUserEmail()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                string email = HttpContext.Current.Session["UserEmail"] as string;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    return email.Trim().ToLowerInvariant();
                }
            }
            return null;
        }

        public static bool IsUserMatch(string itemUserEmail, string targetUserEmail)
        {
            if (string.IsNullOrWhiteSpace(targetUserEmail))
            {
                return false;
            }

            targetUserEmail = targetUserEmail.Trim().ToLowerInvariant();

            if (targetUserEmail.StartsWith("tushar", StringComparison.OrdinalIgnoreCase))
            {
                return string.IsNullOrWhiteSpace(itemUserEmail) ||
                       itemUserEmail.StartsWith("tushar", StringComparison.OrdinalIgnoreCase);
            }

            return !string.IsNullOrWhiteSpace(itemUserEmail) &&
                   string.Equals(itemUserEmail.Trim(), targetUserEmail, StringComparison.OrdinalIgnoreCase);
        }

        private static List<Transaction> GetAllRawTransactions()
        {
            lock (FileLock)
            {
                string filePath = GetDataFilePath();
                if (!File.Exists(filePath))
                {
                    var defaultList = GetInitialSeedData();
                    SaveTransactions(defaultList);
                    return defaultList;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<Transaction>>(json);
                    return list ?? GetInitialSeedData();
                }
                catch
                {
                    return GetInitialSeedData();
                }
            }
        }

        public static List<Transaction> GetAllTransactions(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var all = GetAllRawTransactions();
            return all.Where(t => IsUserMatch(t.UserEmail, email)).ToList();
        }

        public static void AddTransaction(string title, string category, decimal amount, bool isIncome, string dateString = null, string note = "", string userEmail = null)
        {
            lock (FileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                var list = GetAllRawTransactions();

                int newId = list.Count > 0 ? list.Max(t => t.Id) + 1 : 1;
                string date = DateTime.Now.ToString("d/M/yyyy");
                if (!string.IsNullOrWhiteSpace(dateString))
                {
                    if (DateTime.TryParse(dateString, out DateTime dt))
                    {
                        date = dt.ToString("d/M/yyyy");
                    }
                    else
                    {
                        date = dateString.Trim();
                    }
                }

                string badgeClass = GetBadgeClassForCategory(category, isIncome);
                string iconType = GetIconTypeForCategory(category, isIncome);

                var newTxn = new Transaction
                {
                    Id = newId,
                    UserEmail = email,
                    Title = title,
                    Category = category,
                    CategoryBadgeClass = badgeClass,
                    Date = date,
                    Amount = amount,
                    IsIncome = isIncome,
                    IconType = iconType,
                    Note = note ?? ""
                };

                // Add to front of list (newest first)
                list.Insert(0, newTxn);
                SaveTransactions(list);
            }
        }

        public static void DeleteTransaction(int id)
        {
            lock (FileLock)
            {
                var list = GetAllRawTransactions();
                list.RemoveAll(t => t.Id == id);
                SaveTransactions(list);
            }
        }

        public static decimal GetTotalIncome(string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            return list.Where(t => t.IsIncome).Sum(t => t.Amount);
        }

        public static decimal GetTotalExpenses(string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            return list.Where(t => !t.IsIncome).Sum(t => t.Amount);
        }

        public static decimal GetNetBalance(string userEmail = null)
        {
            return GetTotalIncome(userEmail) - GetTotalExpenses(userEmail);
        }

        public static int GetTransactionCount(string userEmail = null)
        {
            return GetAllTransactions(userEmail).Count;
        }

        public static List<Transaction> GetRecentTransactions(int count = 4, string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            return list.Take(count).ToList();
        }

        public static List<Transaction> GetRecentExpenses(int count = 4, string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            return list.Where(t => !t.IsIncome).Take(count).ToList();
        }

        public static List<Transaction> GetRecentIncomes(int count = 4, string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            return list.Where(t => t.IsIncome).Take(count).ToList();
        }

        public static List<Transaction> SearchTransactions(string query, string filterType = "All", string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);

            if (!string.IsNullOrWhiteSpace(filterType))
            {
                if (filterType.Equals("Income", StringComparison.OrdinalIgnoreCase))
                {
                    list = list.Where(t => t.IsIncome).ToList();
                }
                else if (filterType.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                {
                    list = list.Where(t => !t.IsIncome).ToList();
                }
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Transaction>();
            }

            query = query.Trim().ToLower();

            return list.Where(t =>
                (t.Title != null && t.Title.ToLower().Contains(query)) ||
                (t.Category != null && t.Category.ToLower().Contains(query)) ||
                (t.Note != null && t.Note.ToLower().Contains(query)) ||
                (t.Date != null && t.Date.ToLower().Contains(query))
            ).ToList();
        }

        public static List<Transaction> GetTransactionsByCategory(string category, string userEmail = null)
        {
            var list = GetAllTransactions(userEmail);
            if (string.IsNullOrWhiteSpace(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return list;
            }
            return list.Where(t => t.Category != null && t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<CategoryBreakdown> GetCategoryBreakdowns(string userEmail = null)
        {
            var transactions = GetAllTransactions(userEmail).Where(t => !t.IsIncome).ToList();
            decimal totalExpense = transactions.Sum(t => t.Amount);

            var grouped = transactions
                .GroupBy(t => string.IsNullOrWhiteSpace(t.Category) ? "Other" : t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount),
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            var result = new List<CategoryBreakdown>();

            foreach (var item in grouped)
            {
                decimal percentage = totalExpense > 0
                    ? Math.Round((item.Total / totalExpense) * 100m, 1)
                    : 0m;

                result.Add(new CategoryBreakdown
                {
                    CategoryName = item.Category,
                    TotalAmount = item.Total,
                    Percentage = percentage,
                    TransactionCount = item.Count,
                    BarColor = GetColorForCategory(item.Category),
                    IconSvg = GetEmojiOrIconForCategory(item.Category)
                });
            }

            return result;
        }

        // --- Budget Management Methods ---

        private static Dictionary<string, Dictionary<string, decimal>> GetAllUserBudgetsInternal()
        {
            string filePath = GetBudgetsFilePath();
            if (!File.Exists(filePath))
            {
                var dict = new Dictionary<string, Dictionary<string, decimal>>();
                dict["tushar123@gmail.com"] = GetDefaultBudgets();
                SaveAllBudgetsInternal(dict);
                return dict;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var serializer = new JavaScriptSerializer();
                try
                {
                    var multi = serializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json);
                    if (multi != null && multi.Count > 0)
                    {
                        return multi;
                    }
                }
                catch
                {
                    // Fall back
                }

                var flat = serializer.Deserialize<Dictionary<string, decimal>>(json);
                var converted = new Dictionary<string, Dictionary<string, decimal>>();
                converted["tushar123@gmail.com"] = flat ?? GetDefaultBudgets();
                converted["tushar@gmail.com"] = flat ?? GetDefaultBudgets();
                return converted;
            }
            catch
            {
                var dict = new Dictionary<string, Dictionary<string, decimal>>();
                dict["tushar123@gmail.com"] = GetDefaultBudgets();
                return dict;
            }
        }

        public static Dictionary<string, decimal> GetBudgets(string userEmail = null)
        {
            lock (BudgetFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                if (string.IsNullOrWhiteSpace(email))
                {
                    return new Dictionary<string, decimal>();
                }

                var allBudgets = GetAllUserBudgetsInternal();

                if (email.StartsWith("tushar", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var kvp in allBudgets)
                    {
                        if (kvp.Key.StartsWith("tushar", StringComparison.OrdinalIgnoreCase))
                        {
                            return new Dictionary<string, decimal>(kvp.Value);
                        }
                    }
                    return GetDefaultBudgets();
                }

                foreach (var kvp in allBudgets)
                {
                    if (string.Equals(kvp.Key, email, StringComparison.OrdinalIgnoreCase))
                    {
                        return new Dictionary<string, decimal>(kvp.Value);
                    }
                }

                return new Dictionary<string, decimal>();
            }
        }

        public static void SetBudget(string category, decimal amount, string userEmail = null)
        {
            lock (BudgetFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                if (string.IsNullOrWhiteSpace(email)) return;

                var allBudgets = GetAllUserBudgetsInternal();
                string matchedKey = null;
                foreach (var key in allBudgets.Keys)
                {
                    if (string.Equals(key, email, StringComparison.OrdinalIgnoreCase))
                    {
                        matchedKey = key;
                        break;
                    }
                }

                if (matchedKey == null)
                {
                    matchedKey = email;
                    allBudgets[matchedKey] = new Dictionary<string, decimal>();
                }

                allBudgets[matchedKey][category] = amount;
                SaveAllBudgetsInternal(allBudgets);
            }
        }

        public static void RemoveBudget(string category, string userEmail = null)
        {
            lock (BudgetFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                if (string.IsNullOrWhiteSpace(email)) return;

                var allBudgets = GetAllUserBudgetsInternal();
                foreach (var key in allBudgets.Keys)
                {
                    if (string.Equals(key, email, StringComparison.OrdinalIgnoreCase) ||
                        (email.StartsWith("tushar", StringComparison.OrdinalIgnoreCase) && key.StartsWith("tushar", StringComparison.OrdinalIgnoreCase)))
                    {
                        if (allBudgets[key].ContainsKey(category))
                        {
                            allBudgets[key].Remove(category);
                            SaveAllBudgetsInternal(allBudgets);
                            break;
                        }
                    }
                }
            }
        }

        private static void SaveAllBudgetsInternal(Dictionary<string, Dictionary<string, decimal>> budgets)
        {
            string filePath = GetBudgetsFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(budgets);
            File.WriteAllText(filePath, json);
        }

        public static List<BudgetModel> GetBudgetStatusList(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var budgets = GetBudgets(email);
            var transactions = GetAllTransactions(email).Where(t => !t.IsIncome).ToList();

            var list = new List<BudgetModel>();
            foreach (var kvp in budgets)
            {
                string category = kvp.Key;
                decimal budgetLimit = kvp.Value;
                decimal spent = transactions
                    .Where(t => t.Category != null && t.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .Sum(t => t.Amount);

                list.Add(new BudgetModel
                {
                    Category = category,
                    BudgetAmount = budgetLimit,
                    SpentAmount = spent,
                    DefaultColor = GetColorForCategory(category),
                    IconSvg = GetEmojiOrIconForCategory(category)
                });
            }

            return list;
        }

        private static Dictionary<string, decimal> GetDefaultBudgets()
        {
            return new Dictionary<string, decimal>
            {
                { "Food", 8000.00m },
                { "Shopping", 7000.00m },
                { "Transport", 3000.00m },
                { "Entertainment", 2000.00m },
                { "Health", 4000.00m },
                { "Utilities", 4000.00m }
            };
        }

        public static string GetColorForCategory(string category)
        {
            switch (category?.ToLower())
            {
                case "shopping":
                    return "#f43f5e";
                case "food":
                    return "#f59e0b";
                case "utilities":
                    return "#eab308";
                case "health":
                    return "#14b8a6";
                case "transport":
                    return "#3b82f6";
                case "entertainment":
                    return "#ec4899";
                case "gift":
                    return "#f97316";
                case "bills":
                    return "#a855f7";
                default:
                    return "#8b5cf6";
            }
        }

        public static string GetEmojiOrIconForCategory(string category)
        {
            switch (category?.ToLower())
            {
                case "shopping":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M6 2 3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4Z'/><path d='M3 6h18'/><path d='M16 10a4 4 0 0 1-8 0'/></svg>";
                case "food":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M18 2v20'/><path d='M18 8a3 3 0 0 0 3-3V2h-6v3a3 3 0 0 0 3 3Z'/><path d='M6 2v20'/><path d='M3 2v6a3 3 0 0 0 6 0V2'/></svg>";
                case "utilities":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><polygon points='13 2 3 14 12 14 11 22 21 10 12 10 13 2'/></svg>";
                case "health":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z'/></svg>";
                case "transport":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><rect width='16' height='16' x='4' y='4' rx='2'/><path d='M9 18h6'/><path d='M10 22h4'/><circle cx='8' cy='15' r='1'/><circle cx='16' cy='15' r='1'/></svg>";
                case "entertainment":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><rect width='20' height='15' x='2' y='7' rx='2'/><polyline points='17 2 12 7 7 2'/></svg>";
                case "gift":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><polyline points='20 12 20 22 4 22 4 12'/><rect width='20' height='5' x='2' y='7'/><line x1='12' x2='12' y1='22' y2='7'/><path d='M12 7H7.5a2.5 2.5 0 0 1 0-5C11 2 12 7 12 7z'/><path d='M12 7h4.5a2.5 2.5 0 0 0 0-5C13 2 12 7 12 7z'/></svg>";
                case "bills":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M4 2v20l2-1 2 1 2-1 2 1 2-1 2 1 2-1 2 1V2l-2 1-2-1-2 1-2-1-2 1-2-1-2 1-2-1Z'/><path d='M16 8h-8'/><path d='M16 12h-8'/><path d='M10 16h-2'/></svg>";
                case "salary":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><rect width='20' height='14' x='2' y='5' rx='2'/><line x1='2' x2='22' y1='10' y2='10'/></svg>";
                case "investment":
                case "investments":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><line x1='12' x2='12' y1='2' y2='22'/><path d='M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6'/></svg>";
                case "freelance":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><rect width='20' height='14' x='2' y='3' rx='2'/><line x1='8' x2='16' y1='21' y2='21'/><line x1='12' x2='12' y1='17' y2='21'/></svg>";
                case "bonus":
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><circle cx='12' cy='8' r='6'/><path d='M15.477 12.89 17 22l-5-3-5 3 1.523-9.11'/></svg>";
                default:
                    return "<svg viewBox='0 0 24 24' width='18' height='18' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M12 2H2v10l9.29 9.29c.94.94 2.48.94 3.42 0l6.58-6.58c.94-.94.94-2.48 0-3.42L12 2Z'/><path d='M7 7h.01'/></svg>";
            }
        }

        private static void SaveTransactions(List<Transaction> transactions)
        {
            string filePath = GetDataFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(transactions);
            File.WriteAllText(filePath, json);
        }

        private static string GetBadgeClassForCategory(string category, bool isIncome)
        {
            if (isIncome)
            {
                switch (category?.ToLower())
                {
                    case "investment":
                    case "investments":
                        return "tag-investment";
                    case "freelance":
                        return "tag-freelance";
                    case "bonus":
                        return "tag-bonus";
                    default:
                        return "tag-salary";
                }
            }

            switch (category?.ToLower())
            {
                case "food":
                    return "tag-food";
                case "shopping":
                    return "tag-shopping";
                case "transport":
                    return "tag-transport";
                case "salary":
                    return "tag-salary";
                case "entertainment":
                    return "tag-shopping";
                default:
                    return "tag-shopping";
            }
        }

        private static string GetIconTypeForCategory(string category, bool isIncome)
        {
            if (isIncome)
            {
                switch (category?.ToLower())
                {
                    case "investment":
                    case "investments":
                        return "investment";
                    case "freelance":
                        return "freelance";
                    case "bonus":
                        return "bonus";
                    default:
                        return "salary";
                }
            }

            switch (category?.ToLower())
            {
                case "food":
                    return "food";
                case "shopping":
                    return "shopping";
                case "transport":
                    return "transport";
                case "salary":
                    return "salary";
                default:
                    return "food";
            }
        }

        // --- Subscriptions Management ---

        private static readonly object SubFileLock = new object();
        private static readonly object SettingsFileLock = new object();

        private static string GetSubscriptionsFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "subscriptions.json");
        }

        private static List<SubscriptionModel> GetAllRawSubscriptions()
        {
            lock (SubFileLock)
            {
                string filePath = GetSubscriptionsFilePath();
                if (!File.Exists(filePath))
                {
                    var defaults = GetInitialSeedSubscriptions();
                    SaveSubscriptionsInternal(defaults);
                    return defaults;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<SubscriptionModel>>(json);
                    return list ?? GetInitialSeedSubscriptions();
                }
                catch
                {
                    return GetInitialSeedSubscriptions();
                }
            }
        }

        public static List<SubscriptionModel> GetSubscriptions(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var all = GetAllRawSubscriptions();
            return all.Where(s => IsUserMatch(s.UserEmail, email)).ToList();
        }

        public static void AddSubscription(string name, decimal amount, string nextDueDate, string statusBadge = "Upcoming", string userEmail = null)
        {
            lock (SubFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                var list = GetAllRawSubscriptions();
                int newId = list.Count > 0 ? list.Max(s => s.Id) + 1 : 1;

                string formattedDueDate = nextDueDate;
                if (!string.IsNullOrWhiteSpace(nextDueDate) && DateTime.TryParse(nextDueDate, out DateTime dt))
                {
                    formattedDueDate = dt.ToString("d/M/yyyy");
                }

                list.Add(new SubscriptionModel
                {
                    Id = newId,
                    UserEmail = email,
                    Name = name,
                    Amount = amount,
                    NextDueDate = formattedDueDate,
                    StatusBadge = statusBadge ?? "Upcoming"
                });

                SaveSubscriptionsInternal(list);
            }
        }

        public static void DeleteSubscription(int id)
        {
            lock (SubFileLock)
            {
                var list = GetAllRawSubscriptions();
                list.RemoveAll(s => s.Id == id);
                SaveSubscriptionsInternal(list);
            }
        }

        private static void SaveSubscriptionsInternal(List<SubscriptionModel> list)
        {
            string filePath = GetSubscriptionsFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }

        private static List<SubscriptionModel> GetInitialSeedSubscriptions()
        {
            return new List<SubscriptionModel>
            {
                new SubscriptionModel { Id = 1, Name = "Netflix", Amount = 199.00m, NextDueDate = "10/8/2026", StatusBadge = "Upcoming" },
                new SubscriptionModel { Id = 2, Name = "Spotify", Amount = 119.00m, NextDueDate = "14/8/2026", StatusBadge = "Upcoming" },
                new SubscriptionModel { Id = 3, Name = "Amazon Prime", Amount = 299.00m, NextDueDate = "20/8/2026", StatusBadge = "Upcoming" },
                new SubscriptionModel { Id = 4, Name = "YouTube Premium", Amount = 139.00m, NextDueDate = "25/8/2026", StatusBadge = "Upcoming" }
            };
        }

        // --- Currency Settings Management ---

        private static string GetSettingsFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "settings.json");
        }

        public static string GetSelectedCurrencyCode()
        {
            lock (SettingsFileLock)
            {
                string filePath = GetSettingsFilePath();
                if (!File.Exists(filePath))
                {
                    return "INR";
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var dict = serializer.Deserialize<Dictionary<string, object>>(json);
                    if (dict != null && dict.ContainsKey("SelectedCurrency"))
                    {
                        return dict["SelectedCurrency"]?.ToString() ?? "INR";
                    }
                }
                catch
                {
                    // Ignore
                }
                return "INR";
            }
        }

        public static void SetSelectedCurrencyCode(string code)
        {
            lock (SettingsFileLock)
            {
                string filePath = GetSettingsFilePath();
                var dict = new Dictionary<string, object>
                {
                    { "SelectedCurrency", code }
                };
                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(dict);
                File.WriteAllText(filePath, json);
            }
        }

        public static List<CurrencyModel> GetAllCurrencies()
        {
            string selectedCode = GetSelectedCurrencyCode();

            var list = new List<CurrencyModel>
            {
                new CurrencyModel { Code = "INR", Symbol = "₹", Name = "Indian Rupee", FlagEmoji = "🇮🇳" },
                new CurrencyModel { Code = "USD", Symbol = "US$", Name = "US Dollar", FlagEmoji = "🇺🇸" },
                new CurrencyModel { Code = "EUR", Symbol = "€", Name = "Euro", FlagEmoji = "🇪🇺" },
                new CurrencyModel { Code = "GBP", Symbol = "£", Name = "British Pound", FlagEmoji = "🇬🇧" },
                new CurrencyModel { Code = "JPY", Symbol = "¥", Name = "Japanese Yen", FlagEmoji = "🇯🇵" },
                new CurrencyModel { Code = "AED", Symbol = "د.إ", Name = "UAE Dirham", FlagEmoji = "🇦🇪" },
                new CurrencyModel { Code = "SGD", Symbol = "S$", Name = "Singapore Dollar", FlagEmoji = "🇸🇬" },
                new CurrencyModel { Code = "AUD", Symbol = "A$", Name = "Australian Dollar", FlagEmoji = "🇦🇺" }
            };

            foreach (var c in list)
            {
                c.IsSelected = c.Code.Equals(selectedCode, StringComparison.OrdinalIgnoreCase);
            }

            return list;
        }

        public static CurrencyModel GetSelectedCurrencyModel()
        {
            var list = GetAllCurrencies();
            return list.Find(c => c.IsSelected) ?? (list.Count > 0 ? list[0] : null);
        }

        public static string GetCurrencySymbol()
        {
            var model = GetSelectedCurrencyModel();
            return model != null && !string.IsNullOrEmpty(model.Symbol) ? model.Symbol : "₹";
        }

        // --- User Authentication Management ---

        private static readonly object UserFileLock = new object();

        private static string GetUsersFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "users.json");
        }

        public static List<UserModel> GetUsers()
        {
            lock (UserFileLock)
            {
                string filePath = GetUsersFilePath();
                if (!File.Exists(filePath))
                {
                    var defaults = GetInitialSeedUsers();
                    SaveUsersInternal(defaults);
                    return defaults;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<UserModel>>(json);
                    return list ?? GetInitialSeedUsers();
                }
                catch
                {
                    return GetInitialSeedUsers();
                }
            }
        }

        public static bool RegisterUser(string fullName, string email, string password, string phone, out string errorMessage)
        {
            lock (UserFileLock)
            {
                errorMessage = null;
                var list = GetUsers();

                if (list.Exists(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    errorMessage = "An account with this email address already exists. Please log in.";
                    return false;
                }

                int newId = list.Count > 0 ? list.Max(u => u.Id) + 1 : 1;
                var newUser = new UserModel
                {
                    Id = newId,
                    FullName = fullName.Trim(),
                    Email = email.Trim().ToLowerInvariant(),
                    Password = password.Trim(),
                    Phone = !string.IsNullOrWhiteSpace(phone) ? phone.Trim() : "+91 00000 00000",
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd")
                };

                list.Add(newUser);
                SaveUsersInternal(list);
                return true;
            }
        }

        public static bool ValidateUser(string email, string password, out UserModel user)
        {
            lock (UserFileLock)
            {
                user = null;
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return false;
                }

                var list = GetUsers();
                var found = list.Find(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) && u.Password == password.Trim());
                if (found != null)
                {
                    user = found;
                    return true;
                }

                return false;
            }
        }

        public static bool UpdateUserProfile(string oldEmail, string newName, string newEmail, string newPhone)
        {
            lock (UserFileLock)
            {
                var list = GetUsers();
                var user = list.Find(u => u.Email.Equals(oldEmail, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    user.FullName = newName;
                    user.Email = newEmail.ToLowerInvariant();
                    user.Phone = newPhone;
                    SaveUsersInternal(list);
                    return true;
                }
                return false;
            }
        }

        public static bool UpdateUserPassword(string email, string currentPassword, string newPassword, out string errorMessage)
        {
            lock (UserFileLock)
            {
                errorMessage = null;
                var list = GetUsers();
                var user = list.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    errorMessage = "User not found.";
                    return false;
                }

                if (user.Password != currentPassword)
                {
                    errorMessage = "Current password is incorrect.";
                    return false;
                }

                user.Password = newPassword;
                SaveUsersInternal(list);
                return true;
            }
        }

        private static void SaveUsersInternal(List<UserModel> list)
        {
            string filePath = GetUsersFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }

        private static List<UserModel> GetInitialSeedUsers()
        {
            return new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    FullName = "tushar",
                    Email = "tushar@gmail.com",
                    Password = "password123",
                    Phone = "+91 98765 43210",
                    CreatedAt = "2026-03-20"
                }
            };
        }

        // --- Bills Management ---

        private static readonly object BillFileLock = new object();
        private static readonly object LoanFileLock = new object();
        private static readonly object NoteFileLock = new object();

        private static string GetBillsFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "bills.json");
        }

        private static List<BillModel> GetAllRawBills()
        {
            lock (BillFileLock)
            {
                string filePath = GetBillsFilePath();
                if (!File.Exists(filePath))
                {
                    var defaults = GetInitialSeedBills();
                    SaveBillsInternal(defaults);
                    return defaults;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<BillModel>>(json);
                    return list ?? GetInitialSeedBills();
                }
                catch
                {
                    return GetInitialSeedBills();
                }
            }
        }

        public static List<BillModel> GetBills(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var all = GetAllRawBills();
            return all.Where(b => IsUserMatch(b.UserEmail, email)).ToList();
        }

        public static void AddBill(string name, decimal amount, string dueDate, string status = null, string userEmail = null)
        {
            lock (BillFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                var list = GetAllRawBills();
                int newId = list.Count > 0 ? list.Max(b => b.Id) + 1 : 1;

                string formattedDueDate = dueDate;
                if (!string.IsNullOrWhiteSpace(dueDate) && DateTime.TryParse(dueDate, out DateTime dt))
                {
                    formattedDueDate = dt.ToString("d/M/yyyy");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    status = "Upcoming";
                    if (DateTime.TryParse(dueDate, out DateTime dtParsed) && dtParsed.Date < DateTime.Today)
                    {
                        status = "Overdue";
                    }
                }

                list.Add(new BillModel
                {
                    Id = newId,
                    UserEmail = email,
                    Name = name,
                    Amount = amount,
                    DueDate = formattedDueDate,
                    Status = status
                });

                SaveBillsInternal(list);
            }
        }

        public static void DeleteBill(int id)
        {
            lock (BillFileLock)
            {
                var list = GetAllRawBills();
                list.RemoveAll(b => b.Id == id);
                SaveBillsInternal(list);
            }
        }

        private static void SaveBillsInternal(List<BillModel> list)
        {
            string filePath = GetBillsFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }

        private static List<BillModel> GetInitialSeedBills()
        {
            return new List<BillModel>
            {
                new BillModel { Id = 1, UserEmail = "tushar123@gmail.com", Name = "Electricity", Amount = 1450.00m, DueDate = "5/8/2026", Status = "Overdue" },
                new BillModel { Id = 2, UserEmail = "tushar123@gmail.com", Name = "Internet", Amount = 699.00m, DueDate = "12/8/2026", Status = "Upcoming" },
                new BillModel { Id = 3, UserEmail = "tushar123@gmail.com", Name = "House Rent", Amount = 14000.00m, DueDate = "1/8/2026", Status = "Overdue" },
                new BillModel { Id = 4, UserEmail = "tushar123@gmail.com", Name = "Water & Gas", Amount = 380.00m, DueDate = "18/8/2026", Status = "Upcoming" }
            };
        }

        // --- Loans Management ---

        private static string GetLoansFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "loans.json");
        }

        private static List<LoanModel> GetAllRawLoans()
        {
            lock (LoanFileLock)
            {
                string filePath = GetLoansFilePath();
                if (!File.Exists(filePath))
                {
                    var defaults = GetInitialSeedLoans();
                    SaveLoansInternal(defaults);
                    return defaults;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<LoanModel>>(json);
                    return list ?? GetInitialSeedLoans();
                }
                catch
                {
                    return GetInitialSeedLoans();
                }
            }
        }

        public static List<LoanModel> GetLoans(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var all = GetAllRawLoans();
            return all.Where(l => IsUserMatch(l.UserEmail, email)).ToList();
        }

        public static void AddLoan(string personOrBank, decimal amount, string dueDate, string type, string userEmail = null)
        {
            lock (LoanFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                var list = GetAllRawLoans();
                int newId = list.Count > 0 ? list.Max(l => l.Id) + 1 : 1;

                string formattedDueDate = dueDate;
                if (!string.IsNullOrWhiteSpace(dueDate) && DateTime.TryParse(dueDate, out DateTime dt))
                {
                    formattedDueDate = dt.ToString("d/M/yyyy");
                }

                list.Add(new LoanModel
                {
                    Id = newId,
                    UserEmail = email,
                    PersonOrBank = personOrBank,
                    Amount = amount,
                    DueDate = formattedDueDate,
                    Type = string.IsNullOrWhiteSpace(type) ? "I Borrowed" : type
                });

                SaveLoansInternal(list);
            }
        }

        public static void DeleteLoan(int id)
        {
            lock (LoanFileLock)
            {
                var list = GetAllRawLoans();
                list.RemoveAll(l => l.Id == id);
                SaveLoansInternal(list);
            }
        }

        private static void SaveLoansInternal(List<LoanModel> list)
        {
            string filePath = GetLoansFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }

        private static List<LoanModel> GetInitialSeedLoans()
        {
            return new List<LoanModel>
            {
                new LoanModel { Id = 1, UserEmail = "tushar123@gmail.com", PersonOrBank = "HDFC Bank", Amount = 50000.00m, DueDate = "1/9/2026", Type = "I Borrowed" },
                new LoanModel { Id = 2, UserEmail = "tushar123@gmail.com", PersonOrBank = "Rahul", Amount = 5000.00m, DueDate = "15/8/2026", Type = "I Lent" },
                new LoanModel { Id = 3, UserEmail = "tushar123@gmail.com", PersonOrBank = "Car Loan EMI", Amount = 8500.00m, DueDate = "5/8/2026", Type = "I Borrowed" }
            };
        }

        // --- Notes & Memos Management ---

        private static string GetNotesFilePath()
        {
            string appDataPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/App_Data")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            return Path.Combine(appDataPath, "notes.json");
        }

        private static List<NoteModel> GetAllRawNotes()
        {
            lock (NoteFileLock)
            {
                string filePath = GetNotesFilePath();
                if (!File.Exists(filePath))
                {
                    var defaults = GetInitialSeedNotes();
                    SaveNotesInternal(defaults);
                    return defaults;
                }

                try
                {
                    string json = File.ReadAllText(filePath);
                    var serializer = new JavaScriptSerializer();
                    var list = serializer.Deserialize<List<NoteModel>>(json);
                    return list ?? GetInitialSeedNotes();
                }
                catch
                {
                    return GetInitialSeedNotes();
                }
            }
        }

        public static List<NoteModel> GetNotes(string userEmail = null)
        {
            string email = userEmail ?? GetCurrentEffectiveUserEmail();
            var all = GetAllRawNotes();
            return all.Where(n => IsUserMatch(n.UserEmail, email)).ToList();
        }

        public static void AddNote(string content, string userEmail = null)
        {
            lock (NoteFileLock)
            {
                string email = userEmail ?? GetCurrentEffectiveUserEmail();
                var list = GetAllRawNotes();
                int newId = list.Count > 0 ? list.Max(n => n.Id) + 1 : 1;

                list.Insert(0, new NoteModel
                {
                    Id = newId,
                    UserEmail = email,
                    Content = content,
                    DateFormatted = DateTime.Now.ToString("dd/MM/yyyy")
                });

                SaveNotesInternal(list);
            }
        }

        public static void DeleteNote(int id)
        {
            lock (NoteFileLock)
            {
                var list = GetAllRawNotes();
                list.RemoveAll(n => n.Id == id);
                SaveNotesInternal(list);
            }
        }

        private static void SaveNotesInternal(List<NoteModel> list)
        {
            string filePath = GetNotesFilePath();
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }

        private static List<NoteModel> GetInitialSeedNotes()
        {
            return new List<NoteModel>
            {
                new NoteModel { Id = 1, Content = "File ITR before July 31 — carry forward losses.", DateFormatted = "23/03/2026" },
                new NoteModel { Id = 2, Content = "Review mutual fund SIPs — increase by ₹2000 next month.", DateFormatted = "23/03/2026" },
                new NoteModel { Id = 3, Content = "Ask Rahul to return ₹5000 by 15 Aug.", DateFormatted = "23/03/2026" }
            };
        }

        private static List<Transaction> GetInitialSeedData()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    Title = "Dinner",
                    Category = "Food",
                    CategoryBadgeClass = "tag-food",
                    Date = "26/7/2026",
                    Amount = 700.00m,
                    IsIncome = false,
                    IconType = "food",
                    Note = "Zomato order"
                },
                new Transaction
                {
                    Id = 2,
                    Title = "Mall",
                    Category = "Shopping",
                    CategoryBadgeClass = "tag-shopping",
                    Date = "26/7/2026",
                    Amount = 5000.00m,
                    IsIncome = false,
                    IconType = "shopping",
                    Note = "Zara clothes"
                },
                new Transaction
                {
                    Id = 3,
                    Title = "Bus",
                    Category = "Transport",
                    CategoryBadgeClass = "tag-transport",
                    Date = "26/7/2026",
                    Amount = 50.00m,
                    IsIncome = false,
                    IconType = "transport",
                    Note = "Daily pass"
                },
                new Transaction
                {
                    Id = 4,
                    Title = "Dinner",
                    Category = "Food",
                    CategoryBadgeClass = "tag-food",
                    Date = "10/7/2026",
                    Amount = 300.00m,
                    IsIncome = false,
                    IconType = "food",
                    Note = "Restaurant with friends"
                },
                new Transaction
                {
                    Id = 5,
                    Title = "Online shopping",
                    Category = "Shopping",
                    CategoryBadgeClass = "tag-shopping",
                    Date = "5/7/2026",
                    Amount = 3500.00m,
                    IsIncome = false,
                    IconType = "shopping",
                    Note = "Electronics"
                },
                new Transaction
                {
                    Id = 6,
                    Title = "Monthly Salary",
                    Category = "Salary",
                    CategoryBadgeClass = "tag-salary",
                    Date = "1/7/2026",
                    Amount = 23500.00m,
                    IsIncome = true,
                    IconType = "salary",
                    Note = "July paycheck"
                }
            };
        }
    }
}
