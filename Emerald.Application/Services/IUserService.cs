﻿using Emerald.Domain.Models.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IUserService
    {
        Task<User> CurrentUser();
    }
}
