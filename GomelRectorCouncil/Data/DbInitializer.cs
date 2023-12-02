using GomelRectorCouncil.Models;
using System.Linq;
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

            context.AddRange(
                new University()
                {
                    UniversityName = "ГГТУ",
                    Address = "Пр-т Октября, 48, 246746, г. Гомель, Республика Беларусь",
                    Website = "gstu.by"
                },
                new University()
                {
                    UniversityName = "ГГУ",
                    Address = "ул. Советская, 104, 246019, г. Гомель, Республика Беларусь",
                    Website = "gsu.by"
                },
                new University()
                {
                    UniversityName = "БелГУТ",
                    Address = "ул. Кирова, 34, 246653, г. Гомель, Республика Беларусь",
                    Website = "bsut.by"
                }

            );
            context.SaveChanges();
        }

    }
}
