using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class AchievementsViewModel
    {
        public IEnumerable<Achievement> Achievements { get;set;}
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        //Свойство для сортировки
        public SortViewModel SortViewModel { get; set; }
        //Список отчетных годов
        public SelectList ListYears {get;set;}
        
    }
}
