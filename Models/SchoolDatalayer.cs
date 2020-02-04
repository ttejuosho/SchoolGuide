using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGuide
{
    public class SchoolDatalayer : ISchoolActions
    {
        private readonly SchoolDbContext ctx;
        public SchoolDatalayer(SchoolDbContext ctx)
        {
            this.ctx = ctx;
        }
        public School NewSchool(School schoolData)
        {
            try
            {
                using (var tx = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.Schools.Add(schoolData);
                        ctx.SaveChanges();
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                return schoolData;
            }
            catch
            {
                throw;
            }
        }

        public School GetSchool(int schoolId)
        {
            try
            {
                return ctx.Schools.Find(schoolId);
            }
            catch
            {
                throw;
            }
        }

        public string GetSchoolProfileImagePath(int schoolId)
        {
            try
            {
                return ctx.Schools.Find(schoolId).ProfileImagePath;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<School> GetAllSchools()
        {
            try
            {
                return ctx.Schools;
            }
            catch
            {
                throw;
            }
        }

        public School UpdateSchool(School schoolData)
        {
            try
            {
                var existing = ctx.Schools.Where(s => s.SchoolId == schoolData.SchoolId).FirstOrDefault();
                if (existing != null)
                {
                    ctx.Entry(existing).CurrentValues.SetValues(schoolData);
                    ctx.Entry(existing).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return schoolData;
                }
                else
                {
                    throw new ApplicationException();
                }

            }
            catch
            {
                throw;
            }
        }

        public School SaveSchool(School schoolData)
        {
            try
            {
                using (var tx = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var existing = ctx.Schools.Where(s => s.SchoolId == schoolData.SchoolId).FirstOrDefault();
                        if (existing != null)
                        {
                            // Update existing record
                            ctx.Entry(existing).CurrentValues.SetValues(schoolData);
                            ctx.Entry(existing).State = EntityState.Modified;
                            ctx.SaveChanges();
                            tx.Commit();
                            return schoolData;
                        }
                        else
                        {
                            // Check for Duplicates with SchoolName and SchoolEmail Create new record
                            //var duplicate = ctx.Schools.Where(d => d.SchoolId == schoolData.SchoolId && d.SchoolName.ToLower().Trim() == schoolData.SchoolName.ToLower().Trim() && d.SchoolEmail.ToLower().Trim() == schoolData.SchoolEmail.ToLower().Trim());
                            //if (duplicate == null)
                            //{
                            ctx.Schools.Add(schoolData);
                            ctx.SaveChanges();
                            tx.Commit();
                            //} 
                            //else
                            //{
                            //    throw new Exception("School exists");
                            //}
                        }
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }

                }
                return schoolData;
            }
            catch
            {
                throw;
            }
        }

        public School DeleteSchool(int schoolId)
        {
            try
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var existing = ctx.Schools.Where(s => s.SchoolId == schoolId).FirstOrDefault();
                        if (existing != null)
                        {
                            ctx.Schools.Remove(existing);
                            ctx.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            throw new ApplicationException();
                        }
                        return existing;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public List<School> SchoolSearch(string searchWord)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchWord))
                {
                    using (var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            var results = ctx.Schools.Where(x => x.SchoolName.Contains(searchWord) || x.SchoolCity.Contains(searchWord) || x.SchoolState.Contains(searchWord) || x.SchoolAddress.Contains(searchWord)).ToList();
                            return results;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
