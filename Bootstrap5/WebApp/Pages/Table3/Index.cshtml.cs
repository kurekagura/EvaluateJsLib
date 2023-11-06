using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApp.Json;

namespace WebApp.Pages.Table3
{
    public class IndexModel : PageModel
    {
        public List<Deposit> DepositList;

        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet()
        {
            string jsonPath = Path.Combine(_env.WebRootPath, "deposit.json");

            // JSON �f�[�^�� List<Deposit> �ɋt�V���A����
            var jsonStr = System.IO.File.ReadAllText(jsonPath);
            DepositList = JsonSerializer.Deserialize<List<Deposit>>(jsonStr) ?? new List<Deposit>();

            string watchJson = JsonSerializer.Serialize(DepositList);

            var tableColumnsDefinition = new List<ColumnDefinition>
            {
                new ColumnDefinition { Field = "state", Checkbox = true, Title = null },
                new ColumnDefinition { Field = "RegistrationDate", Title = "�o�^��" },
                new ColumnDefinition { Field = "UserID", Title = "���[�U�[ID" },
                new ColumnDefinition { Field = "Name", Title = "����" },
                new ColumnDefinition { Field = "Gender", Title = "����" },
                new ColumnDefinition { Field = "Age", Title = "�N��" },
                new ColumnDefinition { Field = "Birthday", Title = "���N����" }
            };
            string json = JsonSerializer.Serialize(tableColumnsDefinition);
            ViewData["TableColumnsDefinition"] = tableColumnsDefinition;
            return Page();
        }
    }

    public class ColumnDefinition
    {
        public required string Field { get; set; }
        public required string? Title { get; set; }
        public bool? Checkbox { get; set; }
    }
}
