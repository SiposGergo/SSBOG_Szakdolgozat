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
        Task<string> RecordCheckpointPass(int loggedInUserId, RecordDto recordDto);
    }

    public class AdminService : IAdminService
    {
        private readonly ApplicationContext context;
        private readonly IEmailSender emailSender;
        public AdminService(ApplicationContext context, IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
        }

        public async Task<string> RecordCheckpointPass(int loggedInUserId, RecordDto recordDto)
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

            if (!checkpoint.Course.Hike.Staff.Any(x => x.HikerId == loggedInUserId))
            {
                throw new UnauthorizedException();
            }

            if (!checkpoint.Course.Registrations.Any(x => x.StartNumber == recordDto.StartNumber))
            {
                throw new ApplicationException($"A {checkpoint.Course.Name} távra nincs nevezés {recordDto.StartNumber} rajtszámmal!");
            }

            if (checkpoint.Open > DateTime.UtcNow)
            {
                throw new ApplicationException($"Az ellenőrzőpont csak {checkpoint.Open.ToShortTimeString()} UTC időpontban nyit ki!");
            }

            if (checkpoint.Close < DateTime.UtcNow)
            {
                throw new ApplicationException($"Az ellenőrzőpont már bezárt {checkpoint.Close.ToShortTimeString()} UTC időpontban!");
            }

            Registration registration = await context.Registrations
                .Where(x => x.StartNumber == recordDto.StartNumber && x.HikeCourseId == checkpoint.CourseId)
                .Include(x => x.Passes)
                .Include(x => x.Hiker)
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
                    registration.Passes.Add(new CheckPointPass
                    {
                        TimeStamp = null,
                        NettoTime = null,
                        RegistrationId = registration.Id,
                        CheckPointId = min + i
                    });
                }
            }

            if (checkpoint.Id == max && registration.Passes[cpId].TimeStamp != null)
            {
                throw new ApplicationException("Egy cél idő már rögzítésre került korábban!");
            }

            CheckPointPass pass = registration.Passes[cpId];
            pass.TimeStamp = recordDto.TimeStamp;
            pass.NettoTime = cpId == 0 ? new TimeSpan(0, 0, 0) : recordDto.TimeStamp - registration.Passes[0].TimeStamp;


            registration.AvgSpeed = cpId == 0 ? 0 : Math.Round((checkpoint.DistanceFromStart / 1000) / registration.Passes[cpId].NettoTime.Value.TotalHours, 2);

            for (int i = 1; i < registration.Passes.Count; i++)
            {
                if ((registration.Passes[i - 1].TimeStamp ?? new DateTime(1970, 01, 01)) > (registration.Passes[i].TimeStamp ?? new DateTime(3000, 01, 01)))
                {
                    throw new ApplicationException("Érvénytelen idő!");
                }
            }

            await context.SaveChangesAsync();

            if (checkpoint.Id == max)
            {
                await emailSender.SendEmail(
                    registration.Hiker.Email,
                     $"Gratulálunk a {checkpoint.Course.Name} túra sikeres teljesítéséhez!",
                     "Túra teljesítés",
                     PdfGenerator.GetDiploma(registration, checkpoint.Course),
                     "oklevél.pdf");
            }

            if (cpId == 0)
            {
                return $"Túrázó elindítva: {registration.Hiker.Name}";
            }
            return $"Áthaladás Rögzítve!\nTúrázó: {registration.Hiker.Name}, idő: {registration.Passes[cpId].NettoTime.Value.ToString("hh\\:mm\\:ss")}, sebesség: {registration.AvgSpeed} km/h";
        }
    }
}
