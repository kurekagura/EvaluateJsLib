using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Ajax
{
    [IgnoreAntiforgeryToken]
    public class Ajax1Model : PageModel
    {
        [BindProperty]
        //[Required(ErrorMessage = "入力が必須です。")]
        //[PageRemote(
        //    ErrorMessage = "登録済みのユーザー名です。"
        //    )]
        //[PageRemote(
        //    ErrorMessage = "登録済みのユーザー名です。",
        //    AdditionalFields = "__RequestVerificationToken",
        //    HttpMethod = "post",
        //    PageHandler = "CheckUserName"
        //    )]
        public string? UserName { get; set; } = default!;

        [BindProperty]
        //[Range(minimum: 18, maximum: int.MaxValue, ErrorMessage = "18以上である必要があります。")]
        public int? Age { get; set; } = default!;

        public Ajax1Model() { }

        public void OnGet() { }

        /// <summary>
        /// errorMessage（文字列）とmemberNames（配列）
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public JsonResult OnPostCheckUserName([FromBody] JsonPostData inputData)
        {
            var list = new[] { "user1", "user2" };

            {
                var attr = new RequiredAttribute();
                attr.ErrorMessage = "{0}は必須です。";
                if (inputData == null || !attr.IsValid(inputData.UserName))
                {
                    var vr = new ValidationResult(attr.FormatErrorMessage("名前"), new[] { nameof(UserName) });
                    return new JsonResult(vr); //UserName_Required
                }
            }
            {
                var attr = new RequiredAttribute();
                attr.ErrorMessage = "{0}は必須です。";
                if (!attr.IsValid(inputData.Age))
                {
                    return new JsonResult(new ValidationResult(attr.FormatErrorMessage("年齢"), new[] { nameof(Age) }));
                }
            }
            

            if (list.Contains(inputData.UserName))
            {
                var vr = new ValidationResult($"「{inputData.UserName}」は既に存在します。", new[] { nameof(UserName) });
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
