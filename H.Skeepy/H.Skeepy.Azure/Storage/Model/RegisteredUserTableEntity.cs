using H.Skeepy.API.Registration.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage.Model
{
    public class RegisteredUserTableEntity : TableEntity
    {
        public RegisteredUserTableEntity()
        {
        }

        public RegisteredUserTableEntity(RegisteredUser registeredUser)
        {
            RowKey = registeredUser.Email;
            PartitionKey = registeredUser.Email;

            FirstName = registeredUser.FirstName;
            LastName = registeredUser.LastName;
            Email = registeredUser.Email;
            SkeepyId = registeredUser.SkeepyId;
            Status = registeredUser.Status.ToString();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SkeepyId { get; set; }
        public string Status { get; set; }

        public RegisteredUser ToSkeepy()
        {
            return new RegisteredUser
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                SkeepyId = SkeepyId,
                Status = (RegisteredUser.AccountStatus)Enum.Parse(typeof(RegisteredUser.AccountStatus), Status),
            };
        }
    }
}
