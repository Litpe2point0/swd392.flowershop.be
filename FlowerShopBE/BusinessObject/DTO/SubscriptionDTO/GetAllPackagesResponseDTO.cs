using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.SubscriptionDTO
{
    public class GetAllPackagesResponseDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<SubscriptionPackageDTO> Result { get; set; }
    }
}
