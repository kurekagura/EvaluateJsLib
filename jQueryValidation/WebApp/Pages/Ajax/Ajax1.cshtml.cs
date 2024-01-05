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

            //必ず配列で戻す⇒JavaScript側の一貫性
            var errors = new List<ValidationResult>();

            var required = new RequiredAttribute();
            required.ErrorMessage = "{0}は入力が必須です。";
            if (!required.IsValid(inputData.UserName))
            {
                errors.Add(new ValidationResult(required.FormatErrorMessage("名前"), new[] { nameof(UserName) }));
            }
            if (!required.IsValid(inputData.Age))
            {
                errors.Add(new ValidationResult(required.FormatErrorMessage("年齢"), new[] { nameof(Age) }));
            }
            if (errors.Count > 0)
                return new JsonResult(errors);

            if (list.Contains(inputData.UserName))
            {
                errors.Add(new ValidationResult($"「{inputData.UserName}」は既に存在します。", new[] { nameof(UserName) }));
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
