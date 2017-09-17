using H.Skeepy.API.Contracts.Authentication;
using Microsoft.WindowsAzure.Storage.Table;

namespace H.Skeepy.Azure.Storage.Model
{
    public class CredentialsTableEntity : TableEntity
    {
        public CredentialsTableEntity()
        {
        }

        public CredentialsTableEntity(Credentials credentials)
        {
            RowKey = credentials.Id;
            PartitionKey = credentials.Id;

            Username = credentials.Username;
            Password = credentials.Password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials ToSkeepy()
        {
            return new Credentials(Username, Password);
        }
    }
}
