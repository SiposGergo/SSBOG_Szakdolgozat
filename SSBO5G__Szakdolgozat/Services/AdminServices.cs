using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IAdminService
    {
        Task<string> RecordCheckpointPass(int loggedInUserId, RecordDto recordDto);
    }

    public class AdminServices : IAdminService
    {
        ApplicationContext context;
        public AdminServices(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<string> RecordCheckpointPass(int loggedInUserId, RecordDto recordDto)
        {
            CheckPoint cp = await context.CheckPoints.FindAsync(recordDto.CheckpointId);

            Registration registration = await context.Registrations
                .Where(x => x.StartNumber == recordDto.StartNumber)
                .Include(x => x.Passes)
                .SingleOrDefaultAsync();

            registration.Passes.Add(new CheckPointPass
            {
                CheckPointId = recordDto.CheckpointId,
                RegistrationId = registration.Id,
                TimeStamp = recordDto.TimeStamp
            });
            await context.SaveChangesAsync();
            return "Oké";
        }
    }
}
