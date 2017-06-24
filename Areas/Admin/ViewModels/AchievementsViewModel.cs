using Microsoft.AspNetCore.Mvc.Rendering;
using GomelRectorCouncil.Models;
using System.Collections.Generic;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class AchievementsViewModel
    {
        public IEnumerable<Achievement> Achievements { get;set;}
        //�������� ��� ��������� �� ���������
        public PageViewModel PageViewModel { get; set; }
        //������ �������� �����
        public SelectList ListYears {get;set;}
        
    }
}
