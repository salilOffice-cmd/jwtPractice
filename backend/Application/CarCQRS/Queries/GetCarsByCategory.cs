using Application.DTOs.CarDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCQRS.Queries
{
    public class GetCarsByCategory : IRequest<List<ViewCarDTO>>
    {
        public string _CarCategory { get; set; }
    }


    public class GetCarsByCategory_Handler : IRequestHandler<GetCarsByCategory, List<ViewCarDTO>>
    {
        private readonly IApplicationDBContext context;
        public GetCarsByCategory_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<List<ViewCarDTO>> Handle(GetCarsByCategory request, CancellationToken cancellationToken)
        {
            var gotAllCars = await context.Cars.ToListAsync();

            var gotCategorisedCars = gotAllCars.Where(car => car.Category == request._CarCategory)
                                               .Select(car => new ViewCarDTO
                                               {
                                                   CarId = car.CarId,
                                                   CarName = car.CarName,
                                                   Contact_Details = car.Contact_Details,
                                                   Price = car.Price,
                                                   Category = car.Category,
                                               }).ToList();

            return gotCategorisedCars;

        }
    }
}
