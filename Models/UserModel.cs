using System;

namespace Expense_Tracker.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CreatedAt { get; set; }
    }
}
