using BusinessObject.DTO.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login (LoginRequestDTO loginRequest);
        Task<RegisterResponseDTO> Register (RegisterRequestDTO registerRequest);
    }
}
