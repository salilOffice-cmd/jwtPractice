using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; }
        DbSet<Car> Cars { get; }
        DbSet<BookingDetails> AllBookingDetails { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
