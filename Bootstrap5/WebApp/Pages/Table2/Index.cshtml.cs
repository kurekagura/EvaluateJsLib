using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApp.Json;

namespace WebApp.Pages.Table2
{
    public class IndexModel : PageModel
    {
        public List<Deposit> DepositList;

        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> OnGet()
        {
            string jsonPath = Path.Combine(_env.WebRootPath, "deposit.json");

            // JSON データを List<Deposit> に逆シリアル化
            var jsonStr = System.IO.File.ReadAllText(jsonPath);
            DepositList = JsonSerializer.Deserialize<List<Deposit>>(jsonStr) ?? new List<Deposit>();

            return Page();
        }
    }
}
