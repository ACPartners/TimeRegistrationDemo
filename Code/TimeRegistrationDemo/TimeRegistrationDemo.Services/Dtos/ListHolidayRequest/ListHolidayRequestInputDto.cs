namespace TimeRegistrationDemo.Services.Dtos.ListHolidayRequest
{
    public class ListHolidayRequestInputDto
    {
        public ListHolidayRequestInputDto(int year, long userId)
        {
            Year = year;
            UserId = userId;
        }

        public int Year { get; }
        public long UserId { get; }
    }
}
