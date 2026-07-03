using ItsCctvBridgeApi.Model;
using ItsCctvBridgeApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ItsCctvBridgeApi.Controllers {
    [ApiController]
    [Route("api/itscctv")]
    public class itsCctvController : ControllerBase {
        private readonly ItsCctvService service;

        public itsCctvController(ItsCctvService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> SearchCctv(CctvRequest request)
        {
            var result = await service.GetCctvListAsync("testURL");

            return Ok(result);
        }
    }
}
