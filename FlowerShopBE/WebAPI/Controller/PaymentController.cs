using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.PaymentDTO;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("CreatePaymentLink")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] PaymentCreateRequestDTO request)
        {
            var checkoutUrl = await _paymentService.CreatePaymentLink(request);
            return Ok(new { checkoutUrl });
        }
        [HttpPost("ConfirmPayment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] WebhookType request)
        {
            var result = await _paymentService.ConfirmPayment(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("CancelPayment")]
        public async Task<IActionResult> CancelPayment([FromBody] long request)
        {
            var result = await _paymentService.CancelPayment(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
