using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Emerald.Application
{
    public class HomeModel : PageModel
    {
        private IUserRepository userRepository;

        public HomeModel(IUserRepository userRepository, List<User> users)
        {
            this.userRepository = userRepository;
            Users = users;
        }

        public List<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await userRepository.GetQueryable()
                .OrderByDescending(u => u.CreationTime)
                .ToListAsync();
        }
    }
}