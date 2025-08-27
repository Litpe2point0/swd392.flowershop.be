using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.AuthDTO
{
    public class LoginResult
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
