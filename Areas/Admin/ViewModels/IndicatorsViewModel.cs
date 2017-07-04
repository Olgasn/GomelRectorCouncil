using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using GomelRectorCouncil.Models;


namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class IndicatorsViewModel
    {
        public IEnumerable<Indicator> Indicators{get;set;}
        //�������� ��� ��������� �� ���������
        public PageViewModel PageViewModel { get; set; }
        //������ �������� �����
        public SelectList ListYears {get;set;}
        //�������� ��� ����������
        public SortViewModel SortViewModel { get; set; }
        public bool EnableForEdition { get; set; }
        public IndicatorsViewModel()
        {
            EnableForEdition = true;
        }
        
        
    }
}
