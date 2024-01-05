using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Basic1
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "入力が必須です。")]
        public string UserName { get; set; } = default!;

        [BindProperty]
        [Range(minimum: 18, maximum: int.MaxValue, ErrorMessage = "18以上である必要があります。")]
        public int Age { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
