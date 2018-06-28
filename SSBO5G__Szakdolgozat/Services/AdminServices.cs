using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SSBO5G__Szakdolgozat.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IAdminService
    {
        Task RecordCheckpointPass(int loggedInUserId, RecordDto recordDto);
    }

    public class AdminServices : IAdminService
    {
        ApplicationContext context;
        public AdminServices(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task RecordCheckpointPass(int loggedInUserId, RecordDto recordDto)
        {
            CheckPoint checkpoint = await context.CheckPoints
                .Where(x => x.Id == recordDto.CheckpointId)
                .Include(x => x.Course).ThenInclude(c => c.Hike).ThenInclude(h => h.Staff)
                .Include(x => x.Course).ThenInclude(c => c.Registrations)
                .Include(x => x.Course).ThenInclude(c => c.CheckPoints)
                .SingleOrDefaultAsync();

            if (checkpoint == null)
            {
                throw new NotFoundException("ellenőrzőpont");
            }

            // if logged in user no helper of the hike
            if (!checkpoint.Course.Hike.Staff.Any(x => x.HikerId == loggedInUserId))
            {
                throw new UnauthorizedException();
            }

            if (!checkpoint.Course.Registrations.Any(x => x.StartNumber == recordDto.StartNumber))
            {
                throw new ApplicationException($"A {checkpoint.Course.Name} távra nincs nevezés {recordDto.StartNumber} rajtszámmal!");
            }

            if (checkpoint.Open > DateTime.Now)
            {
                throw new ApplicationException($"Az ellenőrzőpont csak {checkpoint.Open.ToShortTimeString()} időpontban nyit ki!");
            }

            if (checkpoint.Close < DateTime.Now)
            {
                throw new ApplicationException($"Az ellenőrzőpont már bezárt {checkpoint.Close.ToShortTimeString()} időpontban!");
            }

            Registration registration = await context.Registrations
                .Where(x => x.StartNumber == recordDto.StartNumber)
                .Include(x => x.Passes)
                .SingleOrDefaultAsync();

            int min = checkpoint.Course.CheckPoints.ToList().Min(x => x.Id);
            int max = checkpoint.Course.CheckPoints.ToList().Max(x => x.Id);
            int cpId = checkpoint.Id - min;

            if (registration.Passes.Count == 0 && cpId != 0)
            {
                throw new ApplicationException("Amég a túrázó nem rajtolt el nem rögzíthető más idő!");
            }

            if (registration.Passes.Count == 0)
            {
                int size = checkpoint.Course.CheckPoints.Count;
                registration.Passes = new List<CheckPointPass>(size);
                for (int i = 0; i < size; i++)
                {
                    registration.Passes.Add(new CheckPointPass { TimeStamp = null });
                }
            }

            if (checkpoint.Id == max && registration.Passes[cpId].TimeStamp != null)
            {
                throw new ApplicationException("Egy cél már rögzítésre került korábban!");
            }

            registration.Passes[cpId] = new CheckPointPass
            {
                CheckPointId = recordDto.CheckpointId,
                RegistrationId = registration.Id,
                TimeStamp = recordDto.TimeStamp
            };

            for (int i = 1; i < registration.Passes.Count; i++)
            {
                if ((registration.Passes[i - 1].TimeStamp ?? new DateTime(1970, 01, 01)) > (registration.Passes[i].TimeStamp ?? new DateTime(3000, 01, 01)))
                {
                    throw new ApplicationException("Érvénytelen idő!");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
