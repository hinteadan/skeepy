using System;

namespace H.Skeepy.Model
{
    public class Individual : DetailsHolder
    {
        private readonly string id;
        private readonly string firstName;
        private readonly string lastName;
        private readonly Lazy<string> fullName;

        public static Individual Existing(string id, string firstName = null, string lastName = null)
        {
            return new Individual(id, firstName, lastName);
        }

        public static Individual New(string firstName, string lastName = null)
        {
            return new Individual(Guid.NewGuid().ToString(), firstName, lastName);
        }

        private Individual(string id, string firstName, string lastName = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException("Existing Individuals must have an ID");
            }

            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("Individuals must have a name");
            }

            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            fullName = new Lazy<string>(() => $"{firstName} {lastName}".Trim());
        }

        public string Id { get { return id; } }

        public string FullName
        {
            get
            {
                return fullName.Value;
            }
        }
    }
}
