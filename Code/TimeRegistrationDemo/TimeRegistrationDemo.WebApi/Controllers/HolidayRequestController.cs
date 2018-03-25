﻿using Microsoft.AspNetCore.Mvc;
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

        public HolidayRequestController(IRegisterHolidayRequestService registerHolidayRequestService)
        {
            RegisterHolidayRequestService = registerHolidayRequestService;
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
    }
}