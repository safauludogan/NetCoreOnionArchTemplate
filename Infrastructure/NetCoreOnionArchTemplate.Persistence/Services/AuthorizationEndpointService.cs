using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Abstractions.Services.Configurations;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
	public class AuthorizationEndpointService : IAuthorizationEndpointService
	{
		private readonly IApplicationService _applicationService;
		private readonly IEndpointReadRepository _endpointReadRepository;
		private readonly IEndpointWriteRepository _endpointWriteRepository;
		private readonly IMenuWriteRepository _menuWriteRepository;
		private readonly IMenuReadRepository _menuReadRepository;
		private readonly RoleManager<AppRole> _roleManager;
		public AuthorizationEndpointService(
			IApplicationService applicationService,
			IEndpointReadRepository endpointReadRepository,
			IEndpointWriteRepository endpointWriteRepository,
			IMenuWriteRepository menuWriteRepository,
			IMenuReadRepository menuReadRepository,
			RoleManager<AppRole> roleManager)
		{
			_applicationService = applicationService;
			_endpointReadRepository = endpointReadRepository;
			_endpointWriteRepository = endpointWriteRepository;
			_menuWriteRepository = menuWriteRepository;
			_menuReadRepository = menuReadRepository;
			_roleManager = roleManager;
		}

		public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
		{
			Menu? _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
			if (_menu == null)
			{
				_menu = new()
				{
					Name = menu
				};
				await _menuWriteRepository.AddAsync(_menu);
				await _menuWriteRepository.SaveAsync();
			}

			Endpoint? endpoint = await _endpointReadRepository.Table
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
				await _endpointWriteRepository.AddAsync(endpoint);
				await _endpointWriteRepository.SaveAsync();
			}
			foreach (var role in endpoint.Roles)
				endpoint.Roles.Remove(role);

			var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();


			foreach (var role in appRoles)
				endpoint.Roles.Add(role);
			await _endpointWriteRepository.SaveAsync();
		}
	}
}
