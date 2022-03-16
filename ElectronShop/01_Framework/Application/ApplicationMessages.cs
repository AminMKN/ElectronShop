namespace _01_Framework.Application
{
    public class ApplicationMessages
    {
        public const string DuplicatedRecord = "امکان ثبت رکورد تکراری وجود ندارد";
        public const string UnableDelete = "امکان حذف رکورد وجود ندارد";
        public const string RecordNotFound = "رکورد با اطلاعات درخواستی یافت نشد";
        public const string TheCountIsMoreTheInventory = "مقدار مورد نظر از موجودی انبار بیشتر است";

        public const string UserNameOrPasswordNotValid = "کاربری با مشخصات وارد شده یافت نشد";
        public const string SentEmailConfirmation = "ایمیلی حاوی لینک فعالسازی برای شما ارسال شد، وارد ایمیل شده و روی لینک فعال سازی کلیک کنید";
        public const string EmailConfirmed = "ایمیل شما با موفقیت تایید شد";
        public const string ForgotPasswordEmailSend = "ایمیلی حاوی لینک تغییر کلمه عبور برای شما ارسال شد";
        public const string ResetPasswordSuccessful = "کلمه عبور شما با موفقیت تغییر کرد";
        public const string Owner = "مدیر سایت";
        public const string OwnerOrAdmin = "مدیر سایت,ادمین سایت";
        public const string DuplicatedUserName = "نام کاربری قبلا انتخاب شده است";
        public const string DuplicatedEmail = "ایمیل قبلا انتخاب شده است";

        public const string PaymentSuccess = "پرداخت با موفقیت انجام شد";
        public const string PaymentFailed = "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد";
        public const string OrderSuccess = "سفارش شما با موفقیت ثبت شد. پس از برسی نتیجه نهایی به شما اعلام خواهد شد";
        public const string EmptyCart = "سبد خرید شما خالی است";

        public const string CommentSuccess = "نظر شما با موفقیت ثبت شد و پس از تایید نمایش داده خواهد شد";
        public const string CommentFailed = "در ثبت نظر شما خطایی رخ داد";
    }
}
