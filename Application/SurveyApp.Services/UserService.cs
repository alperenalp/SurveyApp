using AutoMapper;
using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
using SurveyApp.Infrastructure.Repositories;
using SurveyApp.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(CreateNewUserRequest request)
        {
            var user = request.ConvertToUser(_mapper);
            await _repository.CreateAsync(user);
        }

        public async Task<ValidateUserResponse> ValidateUserAsync(string username, string password)
        {
            var user = await _repository.ValidateUserAsync(username, password);
            var response = user.ConvertToDto<ValidateUserResponse>(_mapper);
            return response;
        }

        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            var response = user.ConvertToDto<UserResponse>(_mapper);
            return response;
        }
    }
}
