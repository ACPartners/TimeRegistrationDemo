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

        public RegisterHolidayRequestService(
            IHolidayRequestRepository holidayRequestRepository,
            IHolidayTypeRepository holidayTypeRepository)
        {
            HolidayRequestRepository = holidayRequestRepository;
            HolidayTypeRepository = holidayTypeRepository;
        }

        public RegisterHolidayRequestOutputDto Register(RegisterHolidayRequestInputDto request)
        {
            // get referential data
            var holidayType = HolidayTypeRepository.GetByKey(request.HolidayType);

            // create entity
            var holidayRequestEntity = new HolidayRequestEntity()
            {
                From = request.From,
                To = request.To,
                Remarks = request.Remarks,
                HolidayType = holidayType,
                //todo fill user
                //UserId = request.userId,


            };

            //validate entity

            //save entity


            //return result;
            throw new System.NotImplementedException();
        }
    }
}
