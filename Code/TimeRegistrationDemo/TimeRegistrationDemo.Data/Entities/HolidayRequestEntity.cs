using System;

namespace TimeRegistrationDemo.Data.Entities
{
    public class HolidayRequestEntity
    {
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Remarks { get; set; }
        public HolidayTypeEntity HolidayType { get; set; }
        public bool IsApproved { get; set; }
        public string DisapprovedReason { get; set; }
    }
}