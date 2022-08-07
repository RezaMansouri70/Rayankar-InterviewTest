using PhoneNumbers;

using System.Net;

namespace ApplicationServices.Services.PhoneService
{
    public static class PhoneService
    {
        #region ValidatePhoneNumber
        public static Services.PhoneService.ValidatePhoneNumberModel ValidatePhoneNumber(string telephoneNumber, string countryCode)
        {
            Services.PhoneService.ValidatePhoneNumberModel returnResult;

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, countryCode);

                bool isMobile = false;
                bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number

                bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode); // returns  w.r.t phone number region

                string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK

                var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE

                string phoneNumberType = numberType.ToString();

                if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                {
                    isMobile = true;
                }
                else
                {
                    isMobile = false;
                }

                var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+447825152591"

                returnResult = new Services.PhoneService.ValidatePhoneNumberModel
                {
                    FormattedNumber = originalNumber,
                    IsMobile = isMobile,
                    IsValidNumber = isValidNumber,
                    IsValidNumberForRegion = isValidRegion,
                    Region = region,
                    StatusCode = HttpStatusCode.OK,
                    StatusMessage = "Success"
                };


            }
            catch (NumberParseException ex)
            {

                String errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();


                returnResult = new Services.PhoneService.ValidatePhoneNumberModel()
                {
                    Message = errorMessage,
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "Failed",
                    IsMobile = false,
                    IsValidNumber = false
                };


            }
            return returnResult;

        }
        #endregion ValidatePhoneNumber
    }

}
