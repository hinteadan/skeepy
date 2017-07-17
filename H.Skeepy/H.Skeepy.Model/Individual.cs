using System;

namespace H.Skeepy.Model
{
    public sealed class Individual : DetailsHolder
    {
        private readonly string id;
        private readonly string firstName;
        private readonly string lastName;

        public static Individual Existing(string id, string firstName, string lastName)
        {
            return new Individual(id, firstName, lastName);
        }

        public static Individual New(string firstName, string lastName = null)
        {
            return new Individual(Guid.NewGuid().ToString(), firstName, lastName);
        }

        private Individual(string id, string firstName, string lastName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("Individuals must have a name");
            }

            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string Id { get { return id; } }

        public string FullName
        {
            get
            {
                return $"{firstName} {lastName}".Trim();
            }
        }
    }
}
