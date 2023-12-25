using Application.DTOs;
using Application.DTOs.CarDTOs;
using Application.UserCQRS.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Commands
{
    public class AddCarCustomValidation : IRequest<ResponseMessage>
    {
        public AddCarDTO _car { get; set; }
        public string _jwtToken { get; set; }
    }


    public class AddCarCustomValidation_Handler : IRequestHandler<AddCarCustomValidation, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public AddCarCustomValidation_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(AddCarCustomValidation request, CancellationToken cancellationToken)
        {

            var jwt = request._jwtToken.Split(" ").Last();

            if (!string.IsNullOrEmpty(jwt))
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    // The ReadToken() is used to parse a JWT string (jwt) and convert it into an object representation.
                    // In short, it is used to decode the jwt payload
                    var token = tokenHandler.ReadToken(jwt) as JwtSecurityToken;

                    if (token != null)
                    {
                        // Extract the role claim from the decoded JWT
                        var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "role");

                        // get all the payload
                        //foreach (var c in token.Claims)
                        //{
                        //    Console.WriteLine($"User Role: {c.Type}");
                        //}

                        if(roleClaim.Value == "Seller")
                        {
                            var carDTO = request._car;

                            Car newCar = new Car
                            {
                                CarName = carDTO.CarName,
                                Contact_Details = carDTO.Contact_Details,
                                Price = carDTO.Price,
                                Category = carDTO.Category,
                            };

                            try
                            {
                                await context.Cars.AddAsync(newCar);
                                await context.SaveChangesAsync(cancellationToken);
                                return new ResponseMessage("Car Added successfully", 200);
                            }

                            catch (Exception ex)
                            {
                                return new ResponseMessage("Server Error", 500);
                            }
                        }

                        else
                        {
                            // The "401 Unauthorized" status code indicates that the
                            // request lacks valid authentication credentials.
                            // On the other hand, the "403 Forbidden" status code signifies that
                            // the server understands the request but refuses to fulfill it.
                            return new ResponseMessage("Only seller are allowed to add a Car", 403);
                        }
                    }
                    else
                    {
                        return new ResponseMessage("Invalid JWT format", 401);
                    }
                }
                catch (Exception ex)
                {
                    return new ResponseMessage($"Error decoding JWT: {ex.Message}", 500);
                }
            }
            else
            {
                return new ResponseMessage("JWT token not found in the Authorization header", 500);
            }

            


        }
    }
}
