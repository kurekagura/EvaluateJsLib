using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sidebar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _htAcs;

        public IndexModel(IHttpContextAccessor httpContextAccessor, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _htAcs = httpContextAccessor;
        }

        public void OnGet()
        {

        }
    }
}