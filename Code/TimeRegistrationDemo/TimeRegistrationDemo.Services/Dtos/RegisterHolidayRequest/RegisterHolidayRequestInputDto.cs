using System;

namespace TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest
{
    public class RegisterHolidayRequestInputDto
    {
        public RegisterHolidayRequestInputDto(DateTime from, DateTime to, string remarks, string holidayType, long userId)
        {
            From = from;
            To = to;
            Remarks = remarks;
            HolidayType = holidayType;
            UserId = userId;
        }

        public DateTime From { get; }
        public DateTime To { get; }
        public string Remarks { get; }
        public string HolidayType { get; }
        public long UserId { get; }
    }
}
