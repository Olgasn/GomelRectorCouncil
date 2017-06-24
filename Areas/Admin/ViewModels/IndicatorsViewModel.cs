using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
        //�������� ��� ��������� �� ���������
        public PageViewModel PageViewModel { get; set; }
        //������ �������� �����
        public SelectList ListYears {get;set;}
        public bool EnableForEdition { get; set; }
        public IndicatorsViewModel()
        {
            EnableForEdition = true;
        }
        
        
    }
}
