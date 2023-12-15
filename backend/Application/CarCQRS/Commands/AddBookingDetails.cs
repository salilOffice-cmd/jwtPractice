using Application.DTOs.CarDTOs;
using Application.UserCQRS.Commands;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.CarCQRS.Commands
{
    public class AddBookingDetails : IRequest<ResponseMessage>
    {
        //public BookingDetailsCartDTO _addBookDetailsDTO { get; set; }
        public List<AddBookDetailsDTO> _addBookDetailsDTO { get; set; }
    }


    public class AddBookingDetails_Handler : IRequestHandler<AddBookingDetails, ResponseMessage>
    {
        private readonly IApplicationDBContext context;
        public AddBookingDetails_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<ResponseMessage> Handle(AddBookingDetails request, CancellationToken cancellationToken)
        {
            //var addBookDetailsDTOCart = request._addBookDetailsDTO.AddBookDetailsCart.ToList();
            var addBookDetailsDTOCart = request._addBookDetailsDTO;

            foreach(var bookingDetails in  addBookDetailsDTOCart)
            {
                BookingDetails newBookingDetails = new BookingDetails
                {
                    CarId = bookingDetails.CarId,
                    CarName = bookingDetails.CarName,
                    Contact_Details= bookingDetails.Contact_Details,
                    Price= bookingDetails.Price,
                    LicenseNumber = bookingDetails.LicenseNumber ,
                    Date= bookingDetails.Date,
                    Time= bookingDetails.Time,
                    Category= bookingDetails.Category
                };

                await context.AllBookingDetails.AddAsync(newBookingDetails);
                await context.SaveChangesAsync(cancellationToken);

            }
            return new ResponseMessage("Booking Added successfully", 200);


            //try
            //{
            //    await context.AllBookingDetails.AddAsync(newBookingDetails);
            //    await context.SaveChangesAsync(cancellationToken);
            //    return new ResponseMessage("Booking Added successfully", 200);
            //}

            //catch (Exception ex)
            //{
            //    return new ResponseMessage("Server Error", 500);
            //}


        }
    }
}
