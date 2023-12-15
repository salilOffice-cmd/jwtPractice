using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCQRS.Commands
{
    public class LoginUser : IRequest<ResponseMessage>
    {
        public LoginUserDTO loginUserDTO { get; set; }
    }


    public class LoginUser_Handler : IRequestHandler<LoginUser, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        private object _message;

        public LoginUser_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            try
            {

                var gotAllUsers = await context.Users.ToListAsync();

                var gotUser = gotAllUsers.FirstOrDefault(user => user.UserName == request.loginUserDTO.UserName);

                if (gotUser != null)
                {
                    if (request.loginUserDTO.Password == gotUser.Password)
                    {
                        //if(gotUser.IsLoggedIn == true)
                        //{
                        //    return new ResponseMessage("User Already Logged In!", 400);
                        //}
                        gotUser.IsLoggedIn = true;
                        await context.SaveChangesAsync(cancellationToken);
                        return new ResponseMessage("User logged in successfully", 200);

                    }

                    else
                    {
                        return new ResponseMessage("Invalid Username or Password", 400);
                    }
                }

                else
                {
                    return new ResponseMessage("User not found", 404);

                }
            }

            catch (Exception ex)
            {
                return new ResponseMessage("Server Error", 500);
            }
        }
    }

    public class ResponseMessage
    {
        public string _message { get; set; }
        public int _statusCode { get; set; }


        public ResponseMessage(string message, int statusCode)
        {
            this._message = message;
            this._statusCode = statusCode;
        }
    }
}
