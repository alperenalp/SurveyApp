using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateNewUserRequest request);
        Task<UserResponse> GetUserByEmailAsync(string email);
        Task<ValidateUserResponse> ValidateUserAsync(string username, string password);
    }
}
