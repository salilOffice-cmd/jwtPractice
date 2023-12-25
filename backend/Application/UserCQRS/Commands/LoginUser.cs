using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        public IConfiguration config { get; }

        public LoginUser_Handler(IApplicationDBContext _applicationDBContext, IConfiguration _config)
        {
            context = _applicationDBContext;
            config = _config;
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

                        var gotJwtToken = GenerateJwtToken(gotUser);


                        gotUser.IsLoggedIn = true;
                        await context.SaveChangesAsync(cancellationToken);
                        //return new ResponseMessage("User logged in successfully", 200);
                        return new ResponseMessage(gotJwtToken, 200);

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
                return new ResponseMessage($"Server Error - {ex.Message}", 500);
            }

        }


        private string GenerateJwtToken(User user)
        {
            // 'JwtSecurityTokenHandler' class which is responsible for creating and validating JWTs.
            var tokenHandler = new JwtSecurityTokenHandler();

            // The Encoding.ASCII.GetBytes method converts the string key into a byte array.
            // This byte array is used as the secret key for signing the JWT.
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

            // 'SecurityTokenDescriptor' is a class that contains information about how to create a JWT,
            // including the claims(user information), expiration time, and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                // Define the claims (user information) that will be included in the token's payload.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // A claim is a piece of information about a subject (user)
                    // that the server wants to store in the token.

                    // 1. ClaimTypes.NameIdentifier is a predefined claim type in .NET,
                    // often used to represent a unique identifier for the user.
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),

                    // 2. We can create our own custom claims without using predefined claim types.
                    new Claim("CustomClaim", "CustomClaimValue")
                }),

                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time

                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                //Issuer = "398748erierueor",
                //Audience = "83948wyr83974eurioeroewirwoeore",

                // The SigningCredentials property specifies the credentials used to sign the JWT.
                // It includes the symmetric key (SymmetricSecurityKey) and
                // the signing algorithm (SecurityAlgorithms.HmacSha256Signature).
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
                // Sha256 algorithm requires the key to have atleast '128 bits' size
            };

            //The CreateToken method of JwtSecurityTokenHandler generates a JWT based on the provided SecurityTokenDescriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // converts the JWT into a string
            return tokenHandler.WriteToken(token);
        }


        //private string GenerateJwtToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {

        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, user.UserName),
        //        }),

        //        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
        //        Issuer = config["Jwt:Issuer"],
        //        Audience = config["Jwt:Audience"],

        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //                                                    SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return tokenHandler.WriteToken(token);
        //}
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
