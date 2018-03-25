using System;
using System.ComponentModel.DataAnnotations;

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

        public long Id { get; }
        [DataType(DataType.Date)]
        public DateTime From { get; }
        [DataType(DataType.Date)]
        public DateTime To { get; }
        public string Remarks { get; }
        public bool? IsApproved { get; }
        public string DisapprovedReason { get; }
        public HolidayTypeDto HolidayType { get; }
    }
}