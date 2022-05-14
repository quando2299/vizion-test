namespace Vizion
{
    public class AppConst
    {
        public class Storage
        {
            public const string BLOB_STORAGE_URL = "https://viziontestingapplicants.blob.core.windows.net/applicants";
        }
        
        public class MessageFormat
        {
            private MessageFormat()
            {
            }

            public const string RequiredMessage = "Vui lòng nhập {0} !";
            public const string MaxLengthMessage = "Vui lòng nhập {0} không quá {1} ký tự!";
        }
    }
}