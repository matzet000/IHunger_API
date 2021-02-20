using IHunger.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data.Context
{
    public class DataIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataIdentityDbContext(DbContextOptions<DataIdentityDbContext> options) : base(options) { }
    }
}
