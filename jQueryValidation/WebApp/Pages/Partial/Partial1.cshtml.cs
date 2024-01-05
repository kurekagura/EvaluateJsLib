using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebApp.Pages.Partial
{
    [IgnoreAntiforgeryToken]
    public class Partial1Model : PageModel
    {
        public string LabelName { get; set; } = default!;

        public void OnGet() { }

        public IActionResult OnPostAddLabel([FromBody] JsonPostData inputData)
        {
            //必ず配列で戻す⇒JavaScript側の一貫性
            var errors = new List<ValidationResult>();

            var required = new RequiredAttribute { ErrorMessage = "{0}は入力が必須です。" };
            if (!required.IsValid(inputData.LabelName))
            {
                errors.Add(new ValidationResult(required.FormatErrorMessage("ラベル"), new[] { nameof(LabelName) }));
                return new JsonResult(errors) { StatusCode = (int)HttpStatusCode.Accepted };
            }

            var regEx = new RegularExpressionAttribute(@"^[^\\/:*?""<>|]*$") { ErrorMessage = "次の文字は使えません。\\ / : * ? < > |" };
            if (!regEx.IsValid(inputData.LabelName))
            {
                errors.Add(new ValidationResult(regEx.ErrorMessage, new[] { nameof(LabelName) }));
                return new JsonResult(errors) { StatusCode = (int)HttpStatusCode.Accepted };
            }
            return Partial("_LabelPartial", inputData.LabelName);
        }
    }

    public class JsonPostData
    {
        public string LabelName { get; set; } = default!;
    }
}
