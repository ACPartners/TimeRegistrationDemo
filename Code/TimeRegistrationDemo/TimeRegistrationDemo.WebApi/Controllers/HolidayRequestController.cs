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
        [HttpGet]
        [Route("Admin")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "RequireAdministratorsRole")]
        public IActionResult GetAdministrator()
        {
            return Ok();
        }
        [HttpGet]
        [Route("Manager")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "RequireManagersRole")]
        public IActionResult GetManager()
        {
            return Ok();
        }
        [HttpPost]
        // Simple requirement, a valid access token must have been presented
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "Administrators")] // A claim must be in the access token of the type "role" and value "administrator"
        //[Authorize(Policy = "RequireAdministratorsRole")]
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