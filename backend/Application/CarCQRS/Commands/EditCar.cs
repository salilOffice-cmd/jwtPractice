using Application.DTOs.CarDTOs;
using Application.UserCQRS.Commands;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Commands
{
    public class EditCar : IRequest<ResponseMessage>
    {
        public EditCarDTO _car { get; set; }
    }


    public class EditCar_Handler : IRequestHandler<EditCar, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public EditCar_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(EditCar request, CancellationToken cancellationToken)
        {
            var carDTO = request._car;

            Car editedCar = new Car
            {
                CarId = carDTO.CarId,
                CarName = carDTO.CarName,
                Contact_Details = carDTO.Contact_Details,
                Price = carDTO.Price,
                Category = carDTO.Category,
            };

            try
            {
                context.Cars.Update(editedCar);
                await context.SaveChangesAsync(cancellationToken);
                return new ResponseMessage("Car Edited successfully", 200);
            }

            catch (Exception ex)
            {
                return new ResponseMessage("Server Error", 500);
            }


        }
    }
}
