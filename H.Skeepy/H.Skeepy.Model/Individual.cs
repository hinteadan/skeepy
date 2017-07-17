using System;

namespace H.Skeepy.Model
{
    public sealed class Individual : DetailsHolder
    {
        private readonly Guid id;
        private readonly string firstName;
        private readonly string lastName;

        public static Individual Existing(Guid id, string firstName, string lastName)
        {
            return new Individual(id, firstName, lastName);
        }

        public static Individual New(string firstName, string lastName)
        {
            return new Individual(Guid.NewGuid(), firstName, lastName);
        }

        private Individual(Guid id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Guid Id { get { return id; } }
    }
}
