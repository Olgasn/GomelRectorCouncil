using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
 
        public SelectList ListYears {get;set;}
        
    }
}
