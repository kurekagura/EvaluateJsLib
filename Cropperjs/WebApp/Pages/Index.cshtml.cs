using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<EnumCropRatio> CropRatioRadios { set; get; } = new();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            CropRatioRadios.AddRange(new[] {
            EnumCropRatio.OneToOne, EnumCropRatio.SixteenToNine, EnumCropRatio.Free});
        }
    }

    public enum EnumCropRatio
    {
        [Display(Name = "1:1")]
        OneToOne,
        [Display(Name = "16:9")]
        SixteenToNine,
        [Display(Name = "Free")]
        Free
    }

}