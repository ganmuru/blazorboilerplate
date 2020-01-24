﻿using BlazorBoilerplate.Server.Middleware.Wrappers;
using BlazorBoilerplate.Shared.AuthorizationDefinitions;
using BlazorBoilerplate.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlazorBoilerplate.Server.Managers;

namespace BlazorBoilerplate.Server.Controllers
{
    /// <summary>
    /// This controller is the entry API for the platform administration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet("Users")]
        [Authorize]
        public async Task<ApiResponse> GetUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
            => await _adminManager.GetUsers(pageSize, pageNumber);

        [HttpGet("Permissions")]
        [Authorize]
        public ApiResponse GetPermissions()
            => _adminManager.GetPermissions();


        [HttpGet("Roles")]
        [Authorize]
        public async Task<ApiResponse> GetRoles([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
            => await _adminManager.GetRoles(pageSize, pageNumber);

    [HttpGet("Role/{roleName}")]
        [Authorize]
        public async Task<ApiResponse> GetRoleAsync(string roleName) 
            => await _adminManager.GetRoleAsync(roleName);

        [HttpPost("Role")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<ApiResponse> CreateRoleAsync([FromBody] RoleDto newRole) 
            => await _adminManager.CreateRoleAsync(newRole);

        [HttpPut("Role")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<ApiResponse> UpdateRoleAsync([FromBody] RoleDto newRole) 
            => await _adminManager.UpdateRoleAsync(newRole);

        // DELETE: api/Admin/Role/5
        [HttpDelete("Role/{name}")]
        [Authorize(Policy = Policies.IsAdmin)]
        public async Task<ApiResponse> DeleteRoleAsync(string name)
            => await _adminManager.DeleteRoleAsync(name);
    }
}
