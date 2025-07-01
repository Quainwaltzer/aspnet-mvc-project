using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeNa.Models;

namespace PracticeNa.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options){}

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Categories> Second { get; set; }
    }
}
