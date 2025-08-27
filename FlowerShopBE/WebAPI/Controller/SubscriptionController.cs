using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet("packages")]
        public async Task<IActionResult> GetSubscriptionPackages()
        {
            var result = await _subscriptionService.GetSubscriptionPackages();
            return Ok(result);
        }
        [HttpGet("orderCode/{orderCode}")]
        public async Task<IActionResult> GetSubscriptionByOrderCode(string orderCode)
        {
            var result = await _subscriptionService.GetSubscriptionByOrderCode(orderCode);
            return StatusCode(result.Code, result);
        }
        [HttpGet("packages/{id}")]
        public async Task<IActionResult> GetSubscriptionPackageById(int id)
        {
            var result = await _subscriptionService.GetSubscriptionPackageById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
