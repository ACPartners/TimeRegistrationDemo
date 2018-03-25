using System.Collections.Generic;
using TimeRegistrationDemo.Services.Dtos.Shared;

namespace TimeRegistrationDemo.Services.Dtos.ListHolidayRequest
{
    public class ListHolidayRequestOutputDto
    {
        public ListHolidayRequestOutputDto(int totalNumberOfApprovedHolidays,
                int totalNumberOfDisapprovedHolidays,
                int totalNumberOfHolidaysToBeApproved,
                IList<HolidaysGroupedByHolidayType> numberOfApprovedHolidaysGroupedByHolidayType,
                IList<HolidayRequestDto> approvedHolidayRequests,
                IList<HolidayRequestDto> disapprovedHolidayRequests,
                IList<HolidayRequestDto> holidayRequestsToBeApproved)
        {
            TotalNumberOfApprovedHolidays = totalNumberOfApprovedHolidays;
            TotalNumberOfDisapprovedHolidays = totalNumberOfDisapprovedHolidays;
            TotalNumberOfHolidaysToBeApproved = totalNumberOfHolidaysToBeApproved;
            NumberOfApprovedHolidaysGroupedByHolidayType = numberOfApprovedHolidaysGroupedByHolidayType;
            ApprovedHolidayRequests = approvedHolidayRequests;
            DisapprovedHolidayRequests = disapprovedHolidayRequests;
            HolidayRequestsToBeApproved = holidayRequestsToBeApproved;
        }

        public int TotalNumberOfApprovedHolidays { get; }
        public int TotalNumberOfDisapprovedHolidays { get; }
        public int TotalNumberOfHolidaysToBeApproved { get; }
        public IList<HolidaysGroupedByHolidayType> NumberOfApprovedHolidaysGroupedByHolidayType { get; }
        public IList<HolidayRequestDto> ApprovedHolidayRequests { get; }
        public IList<HolidayRequestDto> DisapprovedHolidayRequests { get; }
        public IList<HolidayRequestDto> HolidayRequestsToBeApproved { get; }        
    }
}