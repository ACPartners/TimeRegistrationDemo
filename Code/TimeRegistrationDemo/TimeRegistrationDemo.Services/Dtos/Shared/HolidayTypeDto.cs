namespace TimeRegistrationDemo.Services.Dtos.Shared
{
    public class HolidayTypeDto
    {
        public HolidayTypeDto(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Id { get; }
        public string Description { get; }
    }
}
