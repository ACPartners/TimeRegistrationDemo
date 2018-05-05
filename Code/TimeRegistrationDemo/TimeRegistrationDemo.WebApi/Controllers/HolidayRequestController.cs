using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "RequireEmployeeRole")]
        public IActionResult Post([FromBody] RegisterHolidayRequestModel holidayRequest)
        {
            var userId = Helpers.GetUserIdFromClaimsPrincipal(User);

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
        [Route("{year}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "RequireEmployeeRole")]
        public IActionResult Get(int? year)
        {
            var userId = Helpers.GetUserIdFromClaimsPrincipal(User);

            if (!year.HasValue)
                year = DateTime.Today.Year;

            var outputDto = ListHolidayRequestService.List(new ListHolidayRequestInputDto(year.Value, userId));

            return Ok(outputDto);
        }

        //this.User.IsInRole("E")	This expression causes side effects and will not be evaluated	
    }
}