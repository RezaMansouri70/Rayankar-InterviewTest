using System.Net;


namespace ApplicationServices.Services.PhoneService
{
    public class ValidatePhoneNumberModel
    {
        public bool IsValidNumber { get; set; }

        public bool IsValidNumberForRegion { get; set; }

        public bool IsMobile { get; set; }

        public string Region { get; set; }

        public string FormattedNumber { get; set; }
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }



    }
}
