using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Ajax
{
    [IgnoreAntiforgeryToken]
    public class Ajax1Model : PageModel
    {
        [BindProperty]
        //[Required(ErrorMessage = "���͂��K�{�ł��B")]
        //[PageRemote(
        //    ErrorMessage = "�o�^�ς݂̃��[�U�[���ł��B"
        //    )]
        //[PageRemote(
        //    ErrorMessage = "�o�^�ς݂̃��[�U�[���ł��B",
        //    AdditionalFields = "__RequestVerificationToken",
        //    HttpMethod = "post",
        //    PageHandler = "CheckUserName"
        //    )]
        public string? UserName { get; set; } = default!;

        [BindProperty]
        //[Range(minimum: 18, maximum: int.MaxValue, ErrorMessage = "18�ȏ�ł���K�v������܂��B")]
        public int? Age { get; set; } = default!;

        public Ajax1Model() { }

        public void OnGet() { }

        /// <summary>
        /// errorMessage�i������j��memberNames�i�z��j
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public JsonResult OnPostCheckUserName([FromBody] JsonPostData inputData)
        {
            var list = new[] { "user1", "user2" };

            {
                var attr = new RequiredAttribute();
                attr.ErrorMessage = "{0}�͕K�{�ł��B";
                if (inputData == null || !attr.IsValid(inputData.UserName))
                {
                    var vr = new ValidationResult(attr.FormatErrorMessage("���O"), new[] { nameof(UserName) });
                    return new JsonResult(vr); //UserName_Required
                }
            }
            {
                var attr = new RequiredAttribute();
                attr.ErrorMessage = "{0}�͕K�{�ł��B";
                if (!attr.IsValid(inputData.Age))
                {
                    return new JsonResult(new ValidationResult(attr.FormatErrorMessage("�N��"), new[] { nameof(Age) }));
                }
            }
            

            if (list.Contains(inputData.UserName))
            {
                var vr = new ValidationResult($"�u{inputData.UserName}�v�͊��ɑ��݂��܂��B", new[] { nameof(UserName) });
                return new JsonResult(vr); //UserName_PageRemote
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
        public int? Age { get; set; } = default!;
    }
}
