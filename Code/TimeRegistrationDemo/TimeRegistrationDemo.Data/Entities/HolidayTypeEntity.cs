using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeRegistrationDemo.Data.Entities
{
    public class HolidayTypeEntity
    {
        [Key]
        [StringLength(1, MinimumLength = 1)]
        public string Id { get; set; }

        [StringLength(30)]
        [Required]
        public string Description { get; set; }

        public ICollection<HolidayRequestEntity> HolidayRequests { get; set; }
    }
}