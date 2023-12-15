using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCQRS.Queries
{
    public class GetAllUsers : IRequest<List<User>>
    {

    }

    public class GetAllUsers_Handler : IRequestHandler<GetAllUsers, List<User>>
    {
        private readonly IApplicationDBContext context;
        public GetAllUsers_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<List<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var gotAllUsers = await context.Users.ToListAsync();
            return gotAllUsers;
        }
    }
}
