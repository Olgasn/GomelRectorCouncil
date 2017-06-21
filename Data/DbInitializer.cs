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

            
        }
    }
}
