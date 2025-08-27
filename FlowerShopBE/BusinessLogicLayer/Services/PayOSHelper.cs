using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PayOSHelper
    {
        private readonly PayOS _payOs;
        public PayOSHelper(PayOS payOs)
        {
            _payOs = payOs;
        }
        public async Task<string> CreatePayment(ItemData item, string returnUrl, string cancelUrl, string description, int price, long orderCode)
        {
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            PaymentData paymentData = new PaymentData(
                orderCode, //Order code
                price, //Price Amount
                description, //Description
                items, //List of items
                cancelUrl, //Cancel URL
                returnUrl //Return URL
                );
            CreatePaymentResult createPayment = await _payOs.createPaymentLink(paymentData);
            return createPayment.checkoutUrl;
        }
        public async Task<WebhookData> ConfirmPayment(WebhookType webhookBody)
        {
            WebhookData webhookData = _payOs.verifyPaymentWebhookData(webhookBody);
            return webhookData;
        }
    }
}
