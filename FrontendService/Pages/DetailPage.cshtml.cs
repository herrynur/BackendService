using FrontendService.Application.Models;
using FrontendService.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontendService.Pages
{
    public class DetailPageModel : PageModel
    {
        private readonly IPlaceServices _placeServices;
        [BindProperty]
        public PlaceReadDto Place {  get; set; } = new PlaceReadDto();
        public DetailPageModel(IPlaceServices placeServices)
        {
            _placeServices = placeServices;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            Place = await _placeServices.GetSinglePlaceAsync(id, cancellationToken: default);

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            await _placeServices.UpdatePlaceAsync(id, Place, cancellationToken: default);

            return Page();
        }
    }
}
