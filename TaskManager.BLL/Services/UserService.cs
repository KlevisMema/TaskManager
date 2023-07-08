using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.DAL.DTO_s.User;
using TaskManager.BLL.BaseServices;
using Microsoft.AspNetCore.Identity;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.ServicesInterfaces;

namespace TaskManager.BLL.Services
{

    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService
        (
            IMapper mapper,
            UserManager<User> userManager,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserDto>>
        CreateUser
        (
            UserCreateDto userDto
        )
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                    return Response<UserDto>.UnSuccessMessage(result.Errors.First().Description);

                var createdUser = await _userManager.FindByEmailAsync(userDto.Email);

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(createdUser), "User created successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<UserDto>>
        GetUserById
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<UserDto>.NotFound($"User with id: {userId} doesn't exist.");

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(user), $"User {user.UserName} retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<UserDto>>
        UpdateUser
        (
            string userId,
            UserUpdateDto userDto
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<UserDto>.NotFound($"User with id: {userId} doesn't exist.");

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Response<UserDto>.UnSuccessMessage(result.Errors.First().Description);

                return Response<UserDto>.Ok(_mapper.Map<UserDto>(user), $"User with id: {userId} updated successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<UserDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>>
        DeleteUser
        (
            string userId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Response<bool>.NotFound($"User with id: {userId} doesn't exist.");

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return Response<bool>.UnSuccessMessage(result.Errors.First().Description);

                return Response<bool>.Ok(true, $"User with id: {userId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }
    }
}