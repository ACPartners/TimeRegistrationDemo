namespace TimeRegistrationDemo.Services.Dtos.ListHolidayRequest
{
    public class HolidaysGroupedByHolidayType
    {
        public HolidaysGroupedByHolidayType(int numberOfHolidays, string holidayTypeId, string holidayTypeDescription)
        {
            NumberOfHolidays = numberOfHolidays;
            HolidayTypeId = holidayTypeId;
            HolidayTypeDescription = holidayTypeDescription;
        }

        public int NumberOfHolidays { get; }
        public string HolidayTypeId { get; }
        public string HolidayTypeDescription { get; }
    }
}