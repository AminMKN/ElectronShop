namespace _01_Framework.Application
{
    public class ValidationMessages
    {
        public const string IsRequired = "مقدار این فیلد نمیتواند خالی باشد";
        public const string ValueNotValid = "مقدار وارد شده معتبر نمیباشد";
        public const string EmailIsNotValid = "ایمیل وارد شده معتبر نمیباشد";
        public const string PhoneNumberIsNotValid = "شماره موبایل وارد شده معتبر نمیباشد";
        public const string LinkIsNotValid = "لینک وارد شده معتبر نمیباشد";
        public const string PasswordAndRePasswordDoNotMatch = "تکرار کلمه عبور باید با کلمه عبور مطابقت داشته باشد";
        public const string PersianCharacters = "فقط حروف فارسی قابل قبول است";
        public const string MinPasswordLength = "حداقل شش کاراکتر";
        public const string MaxPasswordLength = "حداکثر دوازده کاراکتر";
    }
}