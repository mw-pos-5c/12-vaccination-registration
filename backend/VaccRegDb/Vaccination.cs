﻿#nullable disable

namespace VaccRegDb
{
    public partial class Vaccination
    {
        public long Id { get; set; }

        public virtual Registration Registration { get; set; }
        public long RegistrationId { get; set; }
        public string VaccinationDate { get; set; }
    }
}
