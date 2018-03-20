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

        public RegisterHolidayRequestService(
            IHolidayRequestRepository holidayRequestRepository,
            IHolidayTypeRepository holidayTypeRepository,
            IUserRepository userRepository)
        {
            HolidayRequestRepository = holidayRequestRepository;
            HolidayTypeRepository = holidayTypeRepository;
            UserRepository = userRepository;
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

            //save entity
            HolidayRequestRepository.Register(holidayRequestEntity);

            return new RegisterHolidayRequestOutputDto();
        }
    }
}

//todo dbcontext must be threadsafe + in transaction