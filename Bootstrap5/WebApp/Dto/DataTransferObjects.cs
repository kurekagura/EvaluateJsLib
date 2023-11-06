using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Dto;

/// <summary>
/// JsonPropertyName属性を付与しない場合、
/// @Html.Raw(Json.Serialize(@Model.Deposits))でキャメルケースに変換される（先頭小文字）
/// ※new JsonSerializerOptions { PropertyNameCaseInsensitive = false }で回避できる。
/// 一方、
/// JsonSerializer.Serialize(Deposits)ではプロパティ名そのものになる。
/// </summary>
public class DtoDeposit
{
    //[JsonPropertyName("RegistrationDate")]
    [BindProperty]
    [Display(Name = "登録日")]
    public DateTime RegistrationDate { get; set; }

    //[JsonPropertyName("UserID")]
    [BindProperty]
    [Display(Name = "ユーザーID")]
    public required string UserID { get; set; }

    //[JsonPropertyName("Name")]
    [BindProperty]
    [Display(Name = "名前")]
    public required string Name { get; set; }

    //[JsonPropertyName("Gender")]
    [BindProperty]
    [Display(Name = "性別")]
    public string? Gender { get; set; }

    //[JsonPropertyName("Age")]
    [BindProperty]
    [Display(Name = "年齢")]
    public int Age { get; set; }

    //[JsonPropertyName("Total")]
    [BindProperty]
    [Display(Name = "残高")]
    public decimal Total { get; set; }

    //[JsonPropertyName("Birthday")]
    [BindProperty]
    [Display(Name = "生年月日")]
    public DateTime Birthday { get; set; }
}

//public class DtoDeposit
//{
//    [JsonPropertyName("RegistrationDate")]
//    [BindProperty]
//    [Display(Name = "登録日")]
//    public DateTime RegistrationDate { get; set; }

//    [JsonPropertyName("UserID")]
//    [BindProperty]
//    [Display(Name = "ユーザーID")]
//    public required string UserID { get; set; }

//    [JsonPropertyName("Name")]
//    [BindProperty]
//    [Display(Name = "名前")]
//    public required string Name { get; set; }

//    [JsonPropertyName("Gender")]
//    [BindProperty]
//    [Display(Name = "性別")]
//    public string? Gender { get; set; }

//    [JsonPropertyName("Age")]
//    [BindProperty]
//    [Display(Name = "年齢")]
//    public int Age { get; set; }

//    [JsonPropertyName("Total")]
//    [BindProperty]
//    [Display(Name = "残高")]
//    public decimal Total { get; set; }

//    [JsonPropertyName("Birthday")]
//    [BindProperty]
//    [Display(Name = "生年月日")]
//    public DateTime Birthday { get; set; }
//}

//public class Deposit
//{
//    [JsonPropertyName("RegistrationDate")]
//    public DateTime RegistrationDate { get; set; }
//    [JsonPropertyName("UserID")]
//    public required string UserID { get; set; }
//    [JsonPropertyName("Name")]
//    public required string Name { get; set; }
//    [JsonPropertyName("Gender")]
//    public string? Gender { get; set; }
//    [JsonPropertyName("Age")]
//    public int Age { get; set; }
//    [JsonPropertyName("Total")]
//    public decimal Total { get; set; }
//    [JsonPropertyName("Birthday")]
//    public DateTime Birthday { get; set; }
//}