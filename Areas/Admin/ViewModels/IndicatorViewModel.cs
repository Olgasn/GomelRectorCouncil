using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;
using System.Linq;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
 
        [Display(Name="Выберите год")]
        public int CurrentYear { get; set; }
        public SelectList ListYears 
        { 
            get
            {
                SelectList ls=new SelectList(Indicators.Where(f=>f.Year==CurrentYear).Select(f=>f.Year).Distinct());
                return ls;

            }
        }


    }
}
