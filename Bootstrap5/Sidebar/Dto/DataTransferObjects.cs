using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MyResourceNamespace;

namespace Sidebar.Dto;

/// <summary>
/// JsonPropertyName属性を付与しない場合、
/// @Html.Raw(Json.Serialize(@Model.Deposits))でキャメルケースに変換される（先頭小文字）
/// ※new JsonSerializerOptions { PropertyNameCaseInsensitive = false }で回避できる。
/// 一方、
/// JsonSerializer.Serialize(Deposits)ではプロパティ名そのものになる。
/// </summary>
public class DtoDeposit
{
    [BindProperty]
    //[Display(Name = "RegistrationDate", ResourceType = typeof(MyResourceNamespace.All))]
    [Display(Name = nameof(All.REGISTRATION_DATE), ResourceType = typeof(MyResourceNamespace.All))]
    public DateTime RegistrationDate { get; set; }

    [BindProperty]
    [Display(Name = "ユーザーID")]
    public required string UserID { get; set; }

    [BindProperty]
    [Display(Name = "名前")]
    public required string Name { get; set; }

    [BindProperty]
    [Display(Name = "性別")]
    public string? Gender { get; set; }

    [BindProperty]
    [Display(Name = "年齢")]
    public int Age { get; set; }

    [BindProperty]
    [Display(Name = "残高")]
    public decimal Total { get; set; }

    [BindProperty]
    [Display(Name = "生年月日")]
    public DateTime Birthday { get; set; }
}
