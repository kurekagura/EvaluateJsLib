using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Basic1
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "���͂��K�{�ł��B")]
        public string UserName { get; set; } = default!;

        [BindProperty]
        [Range(minimum: 18, maximum: int.MaxValue, ErrorMessage = "18�ȏ�ł���K�v������܂��B")]
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
