using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.AutoMapper;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Abstractions.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Application.Exceptions;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Rules;
using NetCoreOnionArchTemplate.Application.Helpers;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;
using System.Data;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AuthRules _authRules;
        public UserService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager, IMapper mapper, AuthRules authRules)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _mapper = mapper;
            _authRules = authRules;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserDto model)
        {
            /// SecurityStamp aynı anda yapılacak olan create işlemin de,
			/// veri çakışmasını önlemek için kullanıyoruz.
            AppUser user = _mapper.Map<AppUser, CreateUserDto>(model);
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            CreateUserResponse response = new() { IsSuccess = result.Succeeded };
            if (result.Succeeded)
            {
                response.Message = "User Created";

                /// Kullanıcı create edildikten sonra eğer user role'u yoksa create et
                /// Ardından kullanıcıya default olarak user role'ünü ata.
                if (!await _roleManager.RoleExistsAsync("user"))
                {
                    await _roleManager.CreateAsync(new AppRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });
                }
                await _userManager.AddToRoleAsync(user, "user");
            }
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - {error.Description}\n";
            }
            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser? user, DateTime refreshTokenEndDate)
        {
            await _authRules.EmailAndUsernameOrPasswordShouldNotBeInvalid(user, true);

            user!.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = refreshTokenEndDate;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

        }

        public async Task<bool> UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (identityResult.Succeeded)
                {
                    var result = await _userManager.UpdateSecurityStampAsync(user);
                    return result.Succeeded;
                }
                else
                    throw new PasswordChangeException();
            }
            return false;
        }

        public async Task<List<ListUser>> GetAllUsers(int page, int size)
        {
            List<AppUser>? userList = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return userList.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                UserName = user.UserName,
                TwoFactorEnabled = user.TwoFactorEnabled
            }).ToList();
        }
        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]?> GetRolesToUser(string userIdOrName)
        {
            AppUser? user = new AppUser();
            string[]? currentUserRoles = new string[] { };
            try
            {
                user = await _userManager.FindByIdAsync(userIdOrName);

            }
            catch (ArgumentException ex)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }
            finally
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    currentUserRoles = userRoles.ToArray();
                }
            }
            return currentUserRoles;
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUser(name);

            if (userRoles != null && !userRoles.Any()) return false;

            Endpoint? endpoint = await _unitOfWork.GetReadRepository<Endpoint>().Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null) return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;

            }
            return false;
        }
    }
}
