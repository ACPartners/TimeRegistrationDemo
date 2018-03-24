﻿using FluentValidation;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Dtos;
using TimeRegistrationDemo.Services.Interfaces;

namespace TimeRegistrationDemo.Services.Implementations
{
    public class RegisterHolidayRequestService : IRegisterHolidayRequestService
    {
        private readonly IHolidayRequestRepository HolidayRequestRepository;
        private readonly IHolidayTypeRepository HolidayTypeRepository;
        private readonly IUserRepository UserRepository;
        private readonly IValidator<HolidayRequestEntity> HolidayRequestValidator;

        public RegisterHolidayRequestService(
            IHolidayRequestRepository holidayRequestRepository,
            IHolidayTypeRepository holidayTypeRepository,
            IUserRepository userRepository,
            IValidator<HolidayRequestEntity> holidayRequestValidator)
        {
            HolidayRequestRepository = holidayRequestRepository;
            HolidayTypeRepository = holidayTypeRepository;
            UserRepository = userRepository;
            HolidayRequestValidator = holidayRequestValidator;
        }

        public RegisterHolidayRequestOutputDto Register(RegisterHolidayRequestInputDto request)
        {
            // get referential data
            var holidayType = HolidayTypeRepository.GetByKey(request.HolidayType);
            var user = UserRepository.GetByKey(request.UserId);

            // create entity
            var holidayRequestEntity = new HolidayRequestEntity()
            {
                From = request.From,
                To = request.To,
                Remarks = request.Remarks,
                HolidayType = holidayType,
                User = user
            };

            //validate entity
            var validationResult = HolidayRequestValidator.Validate(holidayRequestEntity);

            //save entity
            HolidayRequestRepository.Register(holidayRequestEntity);

            //todo fill output object with saved entity and errors
            return new RegisterHolidayRequestOutputDto();
        }
    }
}

//todo validate: Holiday is not yet in database
//todo validate: holidaytype input die niet bestaat in dto (of null)
//todo dbcontext must be threadsafe + in transaction