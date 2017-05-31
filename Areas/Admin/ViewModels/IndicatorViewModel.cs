using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorViewModel
    {
        public IEnumerable<Indicator> Indicators { get; set; }
        [Display(Name="Заданный год")]
        public int CurrentYear { get; set; }

    }
}
