using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RestAPI.DTO;
using RestAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Data
{
    public class DatabaseSeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DatabaseSeed(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitDBAsync()
        {
            List<AddApplicationRoleDTO> addApplicationRoleDTOs = new List<AddApplicationRoleDTO>
            {
                new AddApplicationRoleDTO
                {
                    RoleName = "Worker",
                    Description = "System Worker"
                },

                new AddApplicationRoleDTO
                {
                    RoleName = "Solver",
                    Description = "System Solver"
                }
            };

            foreach (AddApplicationRoleDTO model in addApplicationRoleDTOs)
            {
                ApplicationRole applicationRole =
                new ApplicationRole
                {
                    Name = model.RoleName,
                    Description = model.Description,
                    CreatedDate = DateTime.UtcNow,
                    IPAddress = null
                };

                if (!await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    await _roleManager.CreateAsync(applicationRole);
                }
            }
        }
    }
}