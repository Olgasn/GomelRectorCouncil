using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using GomelRectorCouncil.Models;


namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class AchievementsViewModel
    {
        public IEnumerable<Achievement> Achievements { get;set;}
        //�������� ��� ��������� �� ���������
        public PageViewModel PageViewModel { get; set; }
        //�������� ��� ����������
        public SortViewModel SortViewModel { get; set; }
        //������ �������� �����
        public SelectList ListYears {get;set;}
        
    }
}
