using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace WebApp.Pages.Save;

[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostSaveDataAsync()
    {
        await Task.Delay(3000);

        return StatusCode((int)HttpStatusCode.OK);
    }
}
