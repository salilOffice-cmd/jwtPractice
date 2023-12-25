using Application.CarCQRS.Commands;
using Application.CarCQRS.Queries;
using Application.DTOs;
using Application.DTOs.CarDTOs;
using Application.UserCQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Reflection.PortableExecutable;

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

            if (gotCars.Any())
            {
                return Ok(new { msg = "Category Not found" });
            }

            return Ok(gotCars);
        }


        // ***********************************************

        // SERVER AUTOMATICALLY VALIDATES THE JWT
        // When you pass a header to an API with the [Authorize] attribute,
        // the server typically expects the JWT (JSON Web Token) to be included
        // in the 'Authorization' header of the HTTP request.
        // The structure of the Authorization header is standardized, and it typically looks like this:
        //Authorization: Bearer {JWT}

        [Authorize(Roles = "Seller")]
        //[Authorize]
        [HttpPost]
        [Route("addCarAutomatic")]
        public async Task<ActionResult> addCarAsyncAutomaticValidation([FromBody] AddCarDTO _addCarDTO)
        {
            var responseMessage = await Mediator.Send(new AddCarAutoValidation { _car = _addCarDTO });
            return Ok(responseMessage);
        }

        
        // CUSTOM VALIDATING THE JWT 
        [HttpPost]
        [Route("addCarCustom")]
        public async Task<ActionResult> addCarAsyncCustomValidation([FromBody] AddCarDTO _addCarDTO)
        {

            if (Request.Headers.TryGetValue("Authorization", out var jwtToken))
            {
                Console.WriteLine($"Custom Header Value: {jwtToken}");

                string jwtToString = jwtToken.ToString();
                if(jwtToString != null)
                {
                    var responseMessage = await Mediator.Send(new AddCarCustomValidation { _car = _addCarDTO, _jwtToken = jwtToString });
                    return Ok(responseMessage);
                }

                else
                {
                    return Unauthorized("Valid Token not found");
                }

            }
            else
            {
                return Unauthorized("Authorization Header not found");
            }
        }


        // CUSTOM MIDDLEWARE FOR VALIDATING JWT
        [HttpPost]
        [Route("addCarCustomMiddleware")]
        public async Task<ActionResult> AddCarAsyncCustomMiddleware([FromBody] AddCarDTO _addCarDTO)
        {
            var responseMessage = await Mediator.Send(new AddCarAutoValidation { _car = _addCarDTO });
            return Ok(responseMessage);
        }


        // ANOTHER APPROACH FOR GETTING THE HEADER
        [HttpPost]
        [Route("addCarDemo")]
        public async Task<ActionResult> addCarAsync([FromBody] AddCarDTO _addCarDTO, [FromHeader] string Authorization)
        {
            // In this approach, 'Authorization' must be included in the header
            // (otherwise server responses as badrequest)

            Console.WriteLine(Authorization);
            return Ok();


            // Summary:
            // If you prefer the framework to handle parameter binding automatically,
            // and you don't need additional processing, use [FromHeader].
            // If you need more control, error handling, or custom logic,
            // manually retrieving headers using Request.Headers.TryGetValue provides more flexibility.
        }
        // **************************************************



        [HttpPut]
        [Route("editCar")]
        public async Task<ActionResult> editCarAsync([FromBody] EditCarDTO _editCarDTO)
        {
            var responseMessage = await Mediator.Send(new EditCar { _car = _editCarDTO });

            return Ok(responseMessage);
        }

        [HttpPost]
        [Route("addBooking")]
        public async Task<ActionResult> addBookingAsync([FromBody] List<AddBookDetailsDTO> _addBookDetailsDTO)
        {
            var responseMessage = await Mediator.Send(new AddBookingDetails { _addBookDetailsDTO = _addBookDetailsDTO });

            return Ok(responseMessage);
        }

    }

}
