using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCQRS.Commands
{
    public class RegisterUser : IRequest<ResponseMessage>
    {
        public RegisterUserDTO registerUserDTO { get; set; }
    }


    public class RegisterUser_Handler : IRequestHandler<RegisterUser, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public RegisterUser_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(RegisterUser request, CancellationToken cancellationToken)
        {

            try
            {
                var gotAllUsers = await context.Users.ToListAsync();

                var gotUser = gotAllUsers.FirstOrDefault(user => user.UserName == request.registerUserDTO.UserName);

                if (gotUser == null)
                {
                    User newUser = new User
                    {
                        UserName = request.registerUserDTO.UserName,
                        Password = request.registerUserDTO.Password,
                        Role = request.registerUserDTO.Role,
                    };


                    context.Users.Add(newUser);
                    await context.SaveChangesAsync(cancellationToken);

                    return new ResponseMessage("User Registered Successfully", 200);
                }

                return new ResponseMessage("Username already Exists", 400);

            }

            catch (Exception ex)
            {
                return new ResponseMessage("Server Error", 500);
            }

        }
    }
}
