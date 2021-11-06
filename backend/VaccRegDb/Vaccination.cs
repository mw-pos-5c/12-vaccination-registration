using System;

#nullable disable

namespace VaccRegDb
{
    public partial class Vaccination
    {
        public int Id { get; set; }

        public virtual Registration Registration { get; set; }
        public int RegistrationId { get; set; }
        public DateTime VaccinationDate { get; set; }
    }
}
