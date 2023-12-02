﻿using GomelRectorCouncil.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        //Список отчетных годов
        public SelectList ListYears { get; set; }
        //Свойство для сортировки
        public SortViewModel SortViewModel { get; set; }
        public bool EnableForEdition { get; set; }
        public IndicatorsViewModel()
        {
            EnableForEdition = true;
        }
        //Количесвтво записей достижений по показателям
        public int AchievementsCount { get; set; }


    }
}
