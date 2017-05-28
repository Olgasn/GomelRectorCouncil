using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
