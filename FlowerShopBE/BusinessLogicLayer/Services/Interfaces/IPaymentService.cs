using BusinessObject.DTO.PaymentDTO;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentCreateResponseDTO> CreatePaymentLink(PaymentCreateRequestDTO request);
        Task<bool> ConfirmPayment(WebhookType webhookBody);
        Task<bool> CancelPayment(long orderCode);
    }
}
