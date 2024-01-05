using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.PageRemote1
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "���͂��K�{�ł��B")]
        [PageRemote(
            ErrorMessage = "�o�^�ς݂̃��[�U�[���ł��B",
            AdditionalFields = "__RequestVerificationToken",
            HttpMethod = "post",
            PageHandler = "CheckUserName"
            )]
        public string? UserName { get; set; } = default!;

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

        public JsonResult OnPostCheckUserName()
        {
            var list = new[] { "user1", "user2" };
            var isExist = list.Contains(UserName);
            return new JsonResult(!isExist);
        }
    }
}
