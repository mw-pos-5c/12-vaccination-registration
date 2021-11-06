using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using VaccReg.Dtos;

using VaccRegDb;

namespace VaccReg.Services
{
    public class VaccRegService
    {
        private VaccRegContext db;

        public VaccRegService(VaccRegContext db)
        {
            this.db = db;
        }

        public Registration CheckSsn(long ssn, long pin)
        {
            Registration registration = db.Registrations.Include(r => r.Vaccination).FirstOrDefault(r => r.SocialSecurityNumber == ssn);

            if (registration == null || registration.PinCode != pin || registration.Vaccination != null)
            {
                return null;
            }

            return registration;
        }

        private static DateTime GetSlotById(DateTime day, int id)
        {
            DateTime start = day.Date.AddHours(8);

            return start.AddMinutes(id * 15);
        }

        public IEnumerable<TimeSlotDto> GetTimeSlots(DateTime date)
        {
            if (new DateTime(2021, 12, 1).CompareTo(date) >= 0)
            {
                yield break;
            }

            if (new DateTime(2021, 12, 20).CompareTo(date) <= 0)
            {
                yield break;
            }

            for (var x = 0; x < 12; x++)
            {
                DateTime slotDate = GetSlotById(date, x);

                if (db.Vaccinations.Any(v => v.VaccinationDate.Equals(slotDate)))
                {
                    continue;
                }

                yield return new TimeSlotDto
                {
                    Id = x,
                    DateTime = slotDate.ToShortTimeString()
                };
            }
        }

        public VaccConfirm RegisterTimeSlot(long ssn, long pin, DateTime slot, int id)
        {
            Registration registration = CheckSsn(ssn, pin);
            
            slot = GetSlotById(slot, id);

            if (registration == null || db.Vaccinations.Any(v => v.VaccinationDate.Equals(slot)))
            {
                return null;
            }

            var vaccination = new Vaccination
            {
                RegistrationId = registration.Id,
                VaccinationDate = slot
            };

            db.Vaccinations.Add(vaccination);
            db.SaveChanges();

            return new VaccConfirm
            {
                RegistrationId = registration.Id,
                Date = slot.ToShortDateString(),
                Time = slot.ToShortTimeString()
            };
        }
    }
}
