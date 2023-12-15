using Application.DTOs.CarDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Queries
{
    public class GetCarByID : IRequest<ViewCarDTO>
    {
        public int carID { get; set; }
    }


    public class GetCarByID_Handler : IRequestHandler<GetCarByID, ViewCarDTO>
    {
        private readonly IApplicationDBContext context;
        public GetCarByID_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ViewCarDTO> Handle(GetCarByID request, CancellationToken cancellationToken)
        {
            var gotAllCars = await context.Cars.ToListAsync();

            var gotCar = gotAllCars.FirstOrDefault(car => car.CarId == request.carID);


            if (gotCar != null)
            {

                var gotCartoDTO = new ViewCarDTO
                {
                    CarId = gotCar.CarId,
                    CarName = gotCar.CarName,
                    Contact_Details = gotCar.Contact_Details,
                    Price = gotCar.Price,
                    Category = gotCar.Category,
                };

                return gotCartoDTO;
            }

            return null;
        }
    }
}
