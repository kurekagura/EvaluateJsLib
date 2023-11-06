using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            // JSON データを List<Deposit> に逆シリアル化
            var jsonStr = System.IO.File.ReadAllText(jsonPath);
            DepositList = JsonSerializer.Deserialize<List<Deposit>>(jsonStr) ?? new List<Deposit>();

            var tableColumnsDefinition = new List<ColumnDefinition>
            {
                new ColumnDefinition { Field = "state", Checkbox = true, Title = null },
                new ColumnDefinition { Field = "RegistrationDate", Title = "登録日" },
                new ColumnDefinition { Field = "UserID", Title = "ユーザーID" },
                new ColumnDefinition { Field = "Name", Title = "氏名" },
                new ColumnDefinition { Field = "Gender", Title = "性別" },
                new ColumnDefinition { Field = "Age", Title = "年齢" },
                new ColumnDefinition { Field = "Birthday", Title = "生年月日" }
            };
            string json = JsonSerializer.Serialize(tableColumnsDefinition);
            ViewData["TableColumnsDefinition"] = tableColumnsDefinition;
            return Page();
        }
    }

    public class Deposit
    {
        [JsonPropertyName("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
        [JsonPropertyName("UserID")]
        public required string UserID { get; set; }
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("Age")]
        public int Age { get; set; }
        [JsonPropertyName("Total")]
        public decimal Total { get; set; }
        [JsonPropertyName("Birthday")]
        public DateTime Birthday { get; set; }
    }

    public class ColumnDefinition
    {
        public required string Field { get; set; }
        public required string? Title { get; set; }
        public bool? Checkbox { get; set; }
    }
}
