using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolGuide.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGuide
{        
    [Authorize]
    public class SchoolController : Controller
    {
        private readonly ISchoolActions _schoolActions;
        private readonly IHostingEnvironment hostingEnvironment;

        public SchoolController(ISchoolActions schoolActions, IHostingEnvironment hostingEnvironment)
        {
            _schoolActions = schoolActions;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult NotFound404()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorViewModel model = new ErrorViewModel
            {
                ExceptionMessage = exceptionDetails.Error.Message,
                ExceptionPath = exceptionDetails.Path,
                StackTrace = exceptionDetails.Error.StackTrace,
                Source = exceptionDetails.Error.Source,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(model);
        }

        public IActionResult AddSchool()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Schools()
        {
            var model = _schoolActions.GetAllSchools();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/School/{schoolId}")]
        public IActionResult School(int schoolId)
        {
            School model = _schoolActions.GetSchool(schoolId);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("NotFound");
        }

        [HttpGet]
        [Route("/School/Edit/{schoolId}")]
        public IActionResult EditSchool(int schoolId)
        {
            School dbModel = _schoolActions.GetSchool(schoolId);
            AddSchoolVM model = new AddSchoolVM
            {
                SchoolName = dbModel.SchoolName,
                SchoolDescription = dbModel.SchoolDescription,
                SchoolAddress = dbModel.SchoolAddress,
                SchoolCity = dbModel.SchoolCity,
                SchoolState = dbModel.SchoolState,
                SchoolEmail = dbModel.SchoolEmail,
                PrincipalName = dbModel.PrincipalName,
                SchoolPhone = dbModel.SchoolPhone,
                SchoolPhone2 = dbModel.SchoolPhone2,
                SchoolWeb = dbModel.SchoolWeb,
                SchoolId = dbModel.SchoolId,
                ProfileImagePath = dbModel.ProfileImagePath,
                IsBoarding = dbModel.IsBoarding,
                IsReligious = dbModel.IsReligious,
                AgeRange = dbModel.AgeRange,
                NumberOfStudents = dbModel.NumberOfStudents,
                NumberOfTeachers = dbModel.NumberOfTeachers,
                YearFounded = dbModel.YearFounded,
            };

            return View("AddSchool", model);
        }

        [HttpGet]
        [Route("/School/Delete/{schoolId}")]
        public IActionResult DeleteSchool(int schoolId)
        {
            School model = _schoolActions.DeleteSchool(schoolId);
            return RedirectToAction("Schools");
        }

        [HttpPost]
        [Route("/School/Add")]
        public IActionResult AddSchool(AddSchoolVM modelData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (modelData != null)
                    {
                        string fileName = null;
                        if (modelData.ProfileImage != null)
                        {
                            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                            fileName = Guid.NewGuid().ToString() + "_" + modelData.ProfileImage.FileName;
                            string filePath = Path.Combine(uploadsFolder, fileName);
                            // For proper disposal of fileStream
                            using ( var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                modelData.ProfileImage.CopyTo(fileStream);
                            }
                        }

                        School schoolData = new School
                        {
                            SchoolId = modelData.SchoolId,
                            SchoolName = modelData.SchoolName,
                            SchoolDescription = modelData.SchoolDescription,
                            SchoolAddress = modelData.SchoolAddress,
                            SchoolCity = modelData.SchoolCity,
                            SchoolState = modelData.SchoolState,
                            SchoolEmail = modelData.SchoolEmail,
                            SchoolPhone = modelData.SchoolPhone,
                            SchoolPhone2 = modelData.SchoolPhone2,
                            PrincipalName = modelData.PrincipalName,
                            SchoolWeb = modelData.SchoolWeb,
                            IsBoarding = modelData.IsBoarding,
                            IsReligious = modelData.IsReligious,
                            AgeRange = modelData.AgeRange,
                            NumberOfStudents = modelData.NumberOfStudents,
                            NumberOfTeachers = modelData.NumberOfTeachers,
                            YearFounded = modelData.YearFounded,
                            ProfileImagePath = modelData.SchoolId > 0 ? _schoolActions.GetSchoolProfileImagePath(modelData.SchoolId) : fileName
                            //ProfileImagePath = !string.IsNullOrWhiteSpace(fileName) ? fileName : _schoolActions.GetSchoolProfileImagePath(modelData.SchoolId),
                        };

                        var res = _schoolActions.SaveSchool(schoolData);
                        return View("School", res);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                    return View();
            }
            catch
            {
                return RedirectToAction("Error");
            }
            //catch
            //{
            //    throw;
            //}

        }

    }
}
