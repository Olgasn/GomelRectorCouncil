using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        //Список отчетных годов
        public SelectList ListYears {get;set;}
        public bool EnableForEdition { get; set; }
        public IndicatorsViewModel()
        {
            EnableForEdition = true;
        }
        
        
    }
}
