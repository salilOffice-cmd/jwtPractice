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
    public class GetAllCars : IRequest<List<ViewCarDTO>>
    {

    }

    public class GetAllCars_Handler : IRequestHandler<GetAllCars, List<ViewCarDTO>>
    {
        private readonly IApplicationDBContext context;
        public GetAllCars_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<List<ViewCarDTO>> Handle(GetAllCars request, CancellationToken cancellationToken)
        {
            var gotAllCars = await context.Cars.ToListAsync();

            var carDtoList = gotAllCars.Select(car => new ViewCarDTO
            {
                CarId = car.CarId,
                CarName = car.CarName,
                Contact_Details = car.Contact_Details,
                Price = car.Price,
                Category = car.Category,
            }).ToList();

            return carDtoList;
        }
    }
}
