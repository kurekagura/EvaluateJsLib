using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Pages.Ajax
{
    [IgnoreAntiforgeryToken]
    public class Ajax1Model : PageModel
    {
        [BindProperty]
        //[Required(ErrorMessage = "���͂��K�{�ł��B")]
        public string? UserName { get; set; } = default!;

        [BindProperty]
        //[Range(minimum: 18, maximum: int.MaxValue, ErrorMessage = "18�ȏ�ł���K�v������܂��B")]
        //[Required(ErrorMessage = "���͂��K�{�ł��B")]
        public int? Age { get; set; } = default!;

        public void OnGet() { }

        /// <summary>
        /// errorMessage�i������j��memberNames�i�z��j
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public JsonResult OnPostCheckInputs([FromBody] JsonPostData inputData)
        {
            var list = new[] { "user1", "user2" };

            //�K���z��Ŗ߂���JavaScript���̈�ѐ�
            var errors = new List<ValidationResult>();

            var required = new RequiredAttribute();
            required.ErrorMessage = "{0}�͓��͂��K�{�ł��B";
            if (!required.IsValid(inputData.UserName))
            {
                errors.Add(new ValidationResult(required.FormatErrorMessage("���O"), new[] { nameof(UserName) }));
            }
            if (!required.IsValid(inputData.Age))
            {
                errors.Add(new ValidationResult(required.FormatErrorMessage("�N��"), new[] { nameof(Age) }));
            }
            if (errors.Count > 0)
                return new JsonResult(errors);

            var range = new RangeAttribute(18, int.MaxValue) { ErrorMessage = "18�ȏ�ł���K�v������܂��B" };
            if (!range.IsValid(inputData.Age))
            {
                errors.Add(new ValidationResult(range.ErrorMessage, new[] { nameof(Age) }));
                return new JsonResult(errors);
            }

            if (list.Contains(inputData.UserName))
            {
                errors.Add(new ValidationResult($"�u{inputData.UserName}�v�͊��ɑ��݂��܂��B", new[] { nameof(UserName) }));
                return new JsonResult(errors);
            }
            else
            {
                return new JsonResult(ValidationResult.Success!); //null
            }
        }
    }

    public class JsonPostData
    {
        public string? UserName { get; set; } = default!;

        [JsonConverter(typeof(JsonNullableIntConverter))]
        public int? Age { get; set; } = default!;
    }
}
