using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Services.Dtos.Shared;

namespace TimeRegistrationDemo.Services.Dtos
{
    public static class EntityExtensions
    {
        public static HolidayRequestDto ToDto(this HolidayRequestEntity entity)
        {
            return new HolidayRequestDto(entity.Id, entity.From, entity.To,
                entity.Remarks, entity.IsApproved, entity.DisapprovedReason, entity.HolidayType.ToDto());
        }

        public static HolidayTypeDto ToDto(this HolidayTypeEntity entity)
        {
            return new HolidayTypeDto(entity.Id, entity.Description);
        }
    }
}
