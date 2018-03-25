using System;

namespace TimeRegistrationDemo.WebApi.Models
{
    public class RegisterHolidayRequestModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Remarks { get; set; }
        public string HolidayType { get; set; }
    }
}

/*
json object:
{ // RegisterHolidayRequest
  "From": "0001-01-01T00:00:00",
  "To": "0001-01-01T00:00:00",
  "Remarks": null,
  "HolidayType": null
}
*/
