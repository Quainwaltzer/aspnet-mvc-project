using PracticeNa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeNa.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }
    }

    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
    }
}

namespace PracticeNa.ViewModels
{
    public class BudgetViewModel
    {
        public Budget NewBudget { get; set; }
        public Categories newCat { get; set; }
        public List<Budget> BudgetList { get; set; }
        public List<Categories> CatList { get; set; }

        public List<CategoryChartData> ChartData { get; set; }
        public List<CategoryChartData> MostExp { get; set; }

    }

    public class CategoryChartData
    {
        public string Category { get; set; }
        public decimal TotalAmount { get; set; }

        public int TotalCount { get; set; }
    }

}



