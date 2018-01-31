using MeetingSdk.NetAgent.Models;

namespace MeetingSdk.Wpf
{
    public class AccountResource
    {
        //public AccountResource(int accountId)
        //    : this(accountId, "", 0)
        //{

        //}

        public AccountResource(AccountModel accountModel,
            int resourceId,
            MediaType mediaType)
        {
            AccountModel = accountModel;
            ResourceId = resourceId;
            MediaType = mediaType;
        }

        public AccountModel AccountModel { get; set; }

        private int _resourceId;
        public int ResourceId
        {
            get { return _resourceId; }
            set
            {
                _resourceId = value;
            }
        }

        public MediaType MediaType { get; set; }
    }
}