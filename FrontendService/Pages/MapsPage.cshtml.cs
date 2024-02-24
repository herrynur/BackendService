using FrontendService.Application.Models;
using FrontendService.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontendService.Pages
{
    public class MapsPageModel : PageModel
    {
        private readonly IPlaceServices _placeServices;

        //Define model
        public List<PlaceReadDto> Places { get; set; } = [];

        public MapsPageModel(IPlaceServices placeServices)
        {
            _placeServices = placeServices;
        }

        public async Task<IActionResult> OnGet()
        {
            Places = await _placeServices.GetAllPlaceAsync(cancellationToken: default);

            return Page();
        }
    }
}
