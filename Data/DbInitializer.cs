using System.Linq;
using GomelRectorCouncil.Models;
// Инициализатор базы данных
namespace GomelRectorCouncil.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CouncilDbContext context)
        {
            context.Database.EnsureCreated();

            // Проверка занесены ли университеты
            if (context.Universities.Any())
            {
                return;   // База данных инициализирована
            }
            var university= new University()
            {
                UniversityName="ГГТУ",
                Address="Пр-т Октября, 48, 246746, г. Гомель, Республика Беларусь",
                Website="gstu.by"
            };
            context.Add(university);
            context.SaveChanges();    
        }
    }
}
