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
    public class GetUserByUserName : IRequest<User>
    {
        public string _userName { get; set; }
    }


    public class GetUserByUserName_Handler : IRequestHandler<GetUserByUserName, User>
    {
        private readonly IApplicationDBContext context;
        public GetUserByUserName_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<User> Handle(GetUserByUserName request, CancellationToken cancellationToken)
        {
            var gotAllUsers = await context.Users.ToListAsync();

            var gotUser = gotAllUsers.FirstOrDefault(user => user.UserName == request._userName);

            if (gotUser != null)
            {
                return gotUser;
            }

            return null;
        }
    }
}
