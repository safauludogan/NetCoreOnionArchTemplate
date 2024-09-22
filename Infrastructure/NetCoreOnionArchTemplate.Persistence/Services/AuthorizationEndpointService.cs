using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Abstractions.Services.Configurations;
using NetCoreOnionArchTemplate.Application.Interfaces.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorizationEndpointService(
            IApplicationService applicationService,
            RoleManager<AppRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _applicationService = applicationService;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            IReadRepository<Menu> menuReadWork = _unitOfWork.GetReadRepository<Menu>();
            IWriteRepository<Menu> menuWriteWork = _unitOfWork.GetWriteRepository<Menu>();
            Menu? _menu = await menuReadWork.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Name = menu
                };
                await menuWriteWork.AddAsync(_menu);
                await _unitOfWork.SaveAsync();
            }

            Endpoint? endpoint = await _unitOfWork.GetReadRepository<Endpoint>().Table
                .Include(m => m.Menu)
                .Include(r => r.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(m => m.Name == menu)
                    ?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Menu = _menu
                };
                await _unitOfWork.GetWriteRepository<Endpoint>().AddAsync(endpoint);
                await _unitOfWork.SaveAsync();
            }
            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();


            foreach (var role in appRoles)
                endpoint.Roles.Add(role);
            await _unitOfWork.SaveAsync();
        }
    }
}
