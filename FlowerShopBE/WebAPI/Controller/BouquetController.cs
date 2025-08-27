using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouquetController : ControllerBase
    {
        private readonly IBouquetService _bouquetService;
        public BouquetController(IBouquetService bouquetService)
        {
            _bouquetService = bouquetService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAvailableBouquets()
        {
            var result = await _bouquetService.GetAllBouquetAsync();
            return StatusCode(result.Code, result);
        }
    }
}
