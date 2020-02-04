using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGuide
{
    public class AddSchoolVM
    {
        public int SchoolId { get; set; }

        [Display(Name = "School Name")]
        [Required(ErrorMessage = "School Name is required.")]
        public string SchoolName { get; set; }

        [Display(Name = "School Description")]
        [Required(ErrorMessage = "Please provide a short description of your school")]
        public string SchoolDescription { get; set; }

        [Display(Name = "School Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string SchoolAddress { get; set; }

        [Display(Name = "School City")]
        [Required(ErrorMessage = "City is required.")]
        public string SchoolCity { get; set; }

        [Display(Name = "School State")]
        [Required(ErrorMessage = "State is required.")]
        public string SchoolState { get; set; }

        [Display(Name = "Principal Name")]
        [Required(ErrorMessage = "Principal Name is required.")]
        public string PrincipalName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string SchoolPhone { get; set; }

        [Display(Name = "Phone Number 2")]
        public string SchoolPhone2 { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string SchoolEmail { get; set; }

        [Display(Name = "Web URL")]
        public string SchoolWeb { get; set; }

        [Display(Name = "Religious")]
        public bool IsReligious { get; set; }

        [Display(Name = "Boarding")]
        public bool IsBoarding { get; set; }

        public string ProfileImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }

        [Display(Name = "Year Founded")]
        [Required(ErrorMessage = "Year Founded is required. Ex. 1923")]
        public string YearFounded { get; set; }

        [Display(Name = "Age Range")]
        [Required(ErrorMessage = "Age Range of students is required. Ex. 3 - 17")]
        public string AgeRange { get; set; }

        public bool GovtApproved { get; set; }

        [Display(Name = "Number Of Students")]
        public int NumberOfStudents { get; set; }

        [Display(Name = "Number Of Teachers")]
        public int NumberOfTeachers { get; set; }

        public int NumberOfCampuses { get; set; }

    }
}
