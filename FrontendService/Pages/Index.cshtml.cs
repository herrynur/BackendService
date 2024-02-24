using FrontendService.Application.Models;
using FrontendService.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontendService.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPlaceServices _placeServices;

        //Define model
        public List<PlaceReadDto> Places { get; set; } = [];
        [BindProperty]
        public PlaceWriteDto PlaceWrite {  get; set; } = new PlaceWriteDto();

        public IndexModel(ILogger<IndexModel> logger,
            IPlaceServices placeService)
        {
            _logger = logger;
            _placeServices = placeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Get Data From Api");

            Places = await _placeServices.GetAllPlaceAsync(cancellationToken: default);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Post Data to API");

            await _placeServices.PostPlaceAsync(PlaceWrite, cancellationToken: default);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(PlaceReadDto place)
        {
            _logger.LogInformation($"Delete {place.Id}");

            await _placeServices.DeletePlaceAsync(place.Id, cancellationToken : default);

            return RedirectToPage();
        }
    }
}
