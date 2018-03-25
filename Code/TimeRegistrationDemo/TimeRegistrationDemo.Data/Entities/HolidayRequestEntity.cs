using System;
using System.ComponentModel.DataAnnotations;

namespace TimeRegistrationDemo.Data.Entities
{
    public class HolidayRequestEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public bool? IsApproved { get; set; }

        [StringLength(200)]
        public string DisapprovedReason { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public HolidayTypeEntity HolidayType { get; set; }

        [Required]
        public UserEntity User { get; set; }
    }
}

//todo add approvaldate --> not yet defined in businesscase document
//todo add rowversion