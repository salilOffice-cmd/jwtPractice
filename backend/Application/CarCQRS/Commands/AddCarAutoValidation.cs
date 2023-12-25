using Application.DTOs.CarDTOs;
using Application.UserCQRS.Commands;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Commands
{
    public class AddCarAutoValidation : IRequest<ResponseMessage>
    {
        public AddCarDTO _car { get; set; }
        public string _jwtToken { get; set; }
    }


    public class AddCarAutoValidation_Handler : IRequestHandler<AddCarAutoValidation, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public AddCarAutoValidation_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(AddCarAutoValidation request, CancellationToken cancellationToken)
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
    }
}
