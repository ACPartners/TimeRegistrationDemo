using System.Linq;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Dtos;
using TimeRegistrationDemo.Services.Dtos.ListHolidayRequest;
using TimeRegistrationDemo.Services.Interfaces;

namespace TimeRegistrationDemo.Services.Implementations
{
    public class ListHolidayRequestService : IListHolidayRequestService
    {
        private readonly IHolidayRequestRepository HolidayRequestRepository;

        public ListHolidayRequestService(IHolidayRequestRepository holidayRequestRepository)
        {
            HolidayRequestRepository = holidayRequestRepository;
        }

        public ListHolidayRequestOutputDto List(ListHolidayRequestInputDto request)
        {
            //fetch data
            var holidayRequests = HolidayRequestRepository.GetByYearAndUserId(request.Year, request.UserId);

            //transform/group/filter/... data
            var approvedHolidayRequests = holidayRequests.Where(hr => hr.IsApproved.HasValue && hr.IsApproved.Value);
            var disapprovedHolidayRequests = holidayRequests.Where(hr => hr.IsApproved.HasValue && !hr.IsApproved.Value);
            var holidayRequestsToBeApproved = holidayRequests.Where(hr => !hr.IsApproved.HasValue);

            //total numbers
            //+ 1 because if From and To are the same then you would have 0 holidays
            var totalNumberOfApprovedHolidays = approvedHolidayRequests.Sum(hr => (hr.To - hr.From).Days + 1);
            var totalNumberOfDisapprovedHolidays = disapprovedHolidayRequests.Sum(hr => (hr.To - hr.From).Days + 1);
            var totalNumberOfHolidaysToBeApproved = holidayRequestsToBeApproved.Sum(hr => (hr.To - hr.From).Days + 1);

            //total number of approved holidays (grouped by Type of holiday)
            //+ 1 because if From and To are the same then you would have 0 holidays
            var totalNumberOfApprovedHolidaysGroupedByHolidayType =
                approvedHolidayRequests
                    .GroupBy(hr => hr.HolidayType)
                    .Select(ghr => new HolidaysGroupedByHolidayType(
                        ghr.ToList().Sum(hr => (hr.To - hr.From).Days + 1),
                        ghr.Key.Id,
                        ghr.Key.Description)
                    ).ToList();

            return new ListHolidayRequestOutputDto(
                totalNumberOfApprovedHolidays,
                totalNumberOfDisapprovedHolidays,
                totalNumberOfHolidaysToBeApproved,
                totalNumberOfApprovedHolidaysGroupedByHolidayType,
                approvedHolidayRequests.Select(x => x.ToDto()).OrderBy(x => x.From).ToList(),
                disapprovedHolidayRequests.Select(x => x.ToDto()).OrderBy(x => x.From).ToList(),
                holidayRequestsToBeApproved.Select(x => x.ToDto()).OrderBy(x => x.From).ToList());
        }
    }
}
