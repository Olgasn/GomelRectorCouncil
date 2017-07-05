using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using GomelRectorCouncil.Models;


namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        //Список отчетных годов
        public SelectList ListYears {get;set;}
        //Свойство для сортировки
        public SortViewModel SortViewModel { get; set; }
        public bool EnableForEdition { get; set; }
        public IndicatorsViewModel()
        {
            EnableForEdition = true;
        }
        
        
    }
}
