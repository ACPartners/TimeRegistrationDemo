using System;

namespace TimeRegistrationDemo.Services.Dtos.Shared
{
    public class HolidayRequestDto
    {
        public HolidayRequestDto(long id, DateTime from, DateTime to,
            string remarks, bool? isApproved, string disapprovedReason,
            HolidayTypeDto holidayType
            )
        {
            Id = id;
            From = from;
            To = to;
            Remarks = remarks;
            IsApproved = isApproved;
            DisapprovedReason = disapprovedReason;
            HolidayType = holidayType;
        }

        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Remarks { get; set; }
        public bool? IsApproved { get; set; }
        public string DisapprovedReason { get; set; }
        public HolidayTypeDto HolidayType { get; set; }
    }
}