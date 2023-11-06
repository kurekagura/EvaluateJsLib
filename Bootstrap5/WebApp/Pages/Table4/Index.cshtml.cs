using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApp.Dto;
using WebApp.Json;

namespace WebApp.Pages.Table4
{
    public class IndexModel : PageModel
    {
        public List<DtoDeposit> Deposits = new();

        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> OnGet()
        {
            string jsonPath = Path.Combine(_env.WebRootPath, "deposit.json");

            string outPath_WithJsonPropertyName = Path.Combine(Path.GetDirectoryName(jsonPath)!, Path.GetFileNameWithoutExtension(jsonPath) + "_WithJsonPropertyName" + Path.GetExtension(jsonPath));
            string outPath_WithoutJsonPropertyName = Path.Combine(Path.GetDirectoryName(jsonPath)!, Path.GetFileNameWithoutExtension(jsonPath) + "_WithoutJsonPropertyName" + Path.GetExtension(jsonPath));

            // JSON データを List<Deposit> に逆シリアル化
            var jsonStr = System.IO.File.ReadAllText(jsonPath);
            var depositList = JsonSerializer.Deserialize<List<Deposit>>(jsonStr) ?? new List<Deposit>();

            var ienum = depositList.Select(deposit => new DtoDeposit
            {
                RegistrationDate = deposit.RegistrationDate,
                UserID = deposit.UserID,
                Name = deposit.Name,
                Gender = deposit.Gender,
                Age = deposit.Age,
                Total = deposit.Total,
                Birthday = deposit.Birthday
            });
            //ienum = ienum.Take(1);
            Deposits = ienum.Select(deposit => new DtoDeposit
            {
                RegistrationDate = deposit.RegistrationDate,
                UserID = deposit.UserID,
                Name = deposit.Name,
                Gender = deposit.Gender,
                Age = deposit.Age,
                Total = deposit.Total,
                Birthday = deposit.Birthday
            }).ToList();

            string dumpJsonStr = JsonSerializer.Serialize(Deposits, new JsonSerializerOptions() { WriteIndented = true });
            //System.IO.File.WriteAllText(outPath_WithoutJsonPropertyName, dumpJsonStr);

            return Page();
        }
    }
}
