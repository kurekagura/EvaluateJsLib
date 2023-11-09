using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.BlockUI1
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            var wwwroot = _env.WebRootPath;
        }
    }
}
