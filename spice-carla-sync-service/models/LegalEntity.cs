using System;
using System.Collections.Generic;

namespace SpiceCarlaSync.models
{
    public class LegalEntity
    {
        public string EntityId { get; set; }
        public string Name { get; set; }
        public bool IsIndividual { get; set; }
        public Contact Contact { get; set; }
        public Account Account { get; set; }
        public string Title { get; set; }
        public List<string> Positions { get; set; }
        public bool TiedHouse { get; set; }
        public decimal? InterestPercentage { get; set; }
        public DateTimeOffset? AppointmentDate { get; set; }
        public decimal? NumberVotingShares { get; set; }
        public List<Alias> Aliases { get; set; }
        public List<Address> PreviousAddresses { get; set; }
    }
}
