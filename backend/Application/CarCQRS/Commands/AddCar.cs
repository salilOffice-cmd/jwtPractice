using Application.DTOs;
using Application.DTOs.CarDTOs;
using Application.UserCQRS.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Commands
{
    public class AddCar : IRequest<ResponseMessage>
    {
        public AddCarDTO _car { get; set; }
    }


    public class AddCar_Handler : IRequestHandler<AddCar, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public AddCar_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(AddCar request, CancellationToken cancellationToken)
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

            catch(Exception ex)
            {
                return new ResponseMessage("Server Error", 500);
            }


        }
    }
}
