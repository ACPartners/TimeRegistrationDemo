using Microsoft.AspNetCore.Mvc;
using System;
using TimeRegistrationDemo.Services.Dtos.ListHolidayRequest;
using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;
using TimeRegistrationDemo.Services.Interfaces;
using TimeRegistrationDemo.WebApi.Models;

namespace TimeRegistrationDemo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HolidayRequest")]
    public class HolidayRequestController : Controller
    {
        private readonly IRegisterHolidayRequestService RegisterHolidayRequestService;
        private readonly IListHolidayRequestService ListHolidayRequestService;

        public HolidayRequestController(
            IRegisterHolidayRequestService registerHolidayRequestService,
            IListHolidayRequestService listHolidayRequestService
            )
        {
            RegisterHolidayRequestService = registerHolidayRequestService;
            ListHolidayRequestService = listHolidayRequestService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterHolidayRequestModel holidayRequest)
        {
            //todo get user from authentication system
            var userId = 1;

            var inputDto = new RegisterHolidayRequestInputDto(
                holidayRequest.From, holidayRequest.To,
                holidayRequest.Remarks, holidayRequest.HolidayType,
                userId);

            var outputDto = RegisterHolidayRequestService.Register(inputDto);

            if (outputDto.IsSuccessful)
            {
                return Ok(outputDto.Result);
            }
            else
            {
                return BadRequest(outputDto.ValidationResult);
            }
        }

        [HttpGet]
        [Route("{year:int?}")]
        public IActionResult Get(int? year)
        {
            //todo get user from authentication system
            var userId = 1;

            if (!year.HasValue)
                year = DateTime.Today.Year;

            var outputDto = ListHolidayRequestService.List(new ListHolidayRequestInputDto(year.Value, userId));

            return Ok(outputDto);
        }
    }
}