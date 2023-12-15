using Application.CarCQRS.Commands;
using Application.CarCQRS.Queries;
using Application.DTOs;
using Application.DTOs.CarDTOs;
using Application.UserCQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CarController : APIControllerBase
    {

        [HttpGet]
        [Route("allCars")]
        public async Task<ActionResult> GetAllCarsAsync()
        {
            var gotAllCars = await Mediator.Send(new GetAllCars());
            return Ok(gotAllCars);
        }


        [HttpGet]
        [Route("carByID/{_carID}")]
        public async Task<ActionResult> GetAllCarsAsync([FromRoute] int _carID)
        {
            var gotCar = await Mediator.Send(new GetCarByID { carID = _carID});

            if(gotCar == null)
            {
                return Ok(new { msg = "Car Not found" });
            }

            return Ok(gotCar);
        }


        [HttpGet]
        [Route("carsByCategory/{_carCategory}")]
        public async Task<ActionResult> GetAllCarsAsync([FromRoute] string _carCategory)
        {
            var gotCars = await Mediator.Send(new GetCarsByCategory { _CarCategory = _carCategory });

            if (gotCars.Any() == false)
            {
                return Ok(new { msg = "Category Not found" });
            }

            return Ok(gotCars);
        }


        [HttpPost]
        [Route("addCar")]
        public async Task<ActionResult> addCarAsync([FromBody] AddCarDTO _addCarDTO)
        {
            var responseMessage = await Mediator.Send(new AddCar { _car = _addCarDTO  });

            return Ok(responseMessage);
        }


        [HttpPut]
        [Route("editCar")]
        public async Task<ActionResult> editCarAsync([FromBody] EditCarDTO _editCarDTO)
        {
            var responseMessage = await Mediator.Send(new EditCar { _car = _editCarDTO });

            return Ok(responseMessage);
        }

        [HttpPost]
        [Route("addBooking")]
        //public async Task<ActionResult> addBookingAsync([FromBody] BookingDetailsCartDTO _addBookDetailsDTO)
        public async Task<ActionResult> addBookingAsync([FromBody] List<AddBookDetailsDTO> _addBookDetailsDTO)
        {
            var responseMessage = await Mediator.Send(new AddBookingDetails { _addBookDetailsDTO = _addBookDetailsDTO });

            return Ok(responseMessage);
            //return Ok(2);
        }

    }
}
