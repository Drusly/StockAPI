using Karluna.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Data.DbContext.DbServices
{
    public class MigrationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private UserManager<User> _userManager;
        private RoleManager<UserRole> _roleManager;
        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<KtsDbContext>();
                DbUpdater.UpdateDatabase(context);
                _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
                await DbUpdater.InitializeSeed(_userManager, _roleManager);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
