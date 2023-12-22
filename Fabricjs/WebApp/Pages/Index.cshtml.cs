using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //public List<EnumCropRatio> CropRatioRadios { set; get; } = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //CropRatioRadios.AddRange(new[] { EnumCropRatio.OneToOne, EnumCropRatio.SixteenToNine, EnumCropRatio.Free });
        }
    }

    //public enum EnumCropRatio
    //{
    //    [Display(Name = "1:1")]
    //    OneToOne,
    //    [Display(Name = "16:9")]
    //    SixteenToNine,
    //    [Display(Name = "Free")]
    //    Free
    //}
}