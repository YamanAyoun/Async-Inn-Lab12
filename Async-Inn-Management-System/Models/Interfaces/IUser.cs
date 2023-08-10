﻿using Async_Inn_Management_System.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn_Management_System.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDto> Register(RegisterUserDto registerUser, ModelStateDictionary modelState);

        public Task<UserDto> Authenticate(string username, string password);

        public Task<UserDto> GetUser(ClaimsPrincipal principal);
    }
}
