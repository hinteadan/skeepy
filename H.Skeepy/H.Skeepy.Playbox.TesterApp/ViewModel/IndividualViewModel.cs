using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Skeepy.Playbox.TesterApp.ViewModel
{
    public class IndividualViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static IndividualViewModel FromSkeepy(Individual individual)
        {
            return new IndividualViewModel
            {
                Id = individual.Id,
                FirstName = individual.FirstName,
                LastName = individual.LastName,
            };
        }

        public Individual ToSkeepy()
        {
            return Id == null ? Individual.New(FirstName, LastName) : Individual.Existing(Id, FirstName, LastName);
        }
    }
}
