using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.BouquetDTO;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class BouquetService : IBouquetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private string BaseUrl = "";
        public BouquetService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            BaseUrl = _configuration["BaseUrl"] + "/";
        }

        public async Task<GetAllBouquetResponseDTO> GetAllBouquetAsync()
        {
            var bouquets = await _unitOfWork.BouquetRepository.GetAllAsync();
            if(bouquets == null || bouquets.Count() == 0)
            {
                return new GetAllBouquetResponseDTO
                {
                    Code = 404,
                    Result = null,
                    Message = "No bouquets found",
                    IsSuccess = false
                };
            }
            var bouquetDTOs = bouquets.Select(bouquet => new BouquetResultDTO
            {
                Id = bouquet.Id,
                Name = bouquet.Name,
                Description = bouquet.Description,
                BouquetTypeId = bouquet.BouquetTypeId,
                ImageUrl = BaseUrl + bouquet.ImageUrl,
                isAvailable = bouquet.isAvailable
            }).ToList();
            return new GetAllBouquetResponseDTO
            {
                Code = 200,
                Result = bouquetDTOs,
                Message = "Get all bouquets successfully",
                IsSuccess = true
            };
        }
    }
}
