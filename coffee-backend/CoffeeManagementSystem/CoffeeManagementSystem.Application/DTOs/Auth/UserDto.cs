using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagementSystem.Application.DTOs.Auth
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; }  = string.Empty;

    }
}
