using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGuide
{
    public interface ISchoolActions
    {
        School GetSchool(int ischoolIdd);
        IEnumerable<School> GetAllSchools();
        School NewSchool(School schoolData);
        School UpdateSchool(School schoolData);
        School SaveSchool(School schoolData);
        School DeleteSchool(int schoolId);
        List<School> SchoolSearch(string searchWord);
        string GetSchoolProfileImagePath(int schoolId);
    }
}
