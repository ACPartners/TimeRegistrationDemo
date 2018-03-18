using System;

namespace TimeRegistrationDemo.Services.Dtos
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

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Remarks { get; set; }
        public string HolidayType { get; set; }
        public long UserId { get; set; }
    }
}
