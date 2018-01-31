namespace MeetingSdk.NetAgent.Models
{
    /// <summary>
    /// 参会者信息
    /// </summary>
    public class AccountModel
    {
        public AccountModel(int accountId, string accountName)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
        }

        /// <summary>
        /// 参会者视讯号
        /// </summary>
        public int AccountId { get; }

        /// <summary>
        /// 参会者名称
        /// </summary>
        public string AccountName { get; set; }

        public override string ToString()
        {
            return $"{this.AccountId} - {this.AccountName}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
