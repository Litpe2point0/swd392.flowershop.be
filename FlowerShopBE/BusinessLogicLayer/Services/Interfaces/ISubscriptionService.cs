using BusinessObject.DTO.SubscriptionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<GetAllPackagesResponseDTO> GetSubscriptionPackages();
        Task<GetSubscriptionByOrderCodeResponseDTO> GetSubscriptionByOrderCode(string orderCode);
        Task<SubscriptionPackageDTO> GetSubscriptionPackageById(int id);
    }
}
