using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.OrderDTO
{
    public class OrderCreateResponseDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public OrderResultDTO Result { get; set; }
    }
}
