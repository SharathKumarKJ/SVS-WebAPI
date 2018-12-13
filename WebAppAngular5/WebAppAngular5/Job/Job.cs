using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;
using WebAppAngular5.Models;

namespace WebAppAngular5.WindowService
{
    public class Job : IJob
    {
        private const string _systemAdmin = "System.Admin";
        private int count = 1;
        private Repository repository = new Repository();

        public Task Execute(IJobExecutionContext context)
        {


            //if (DateTime.Now.Day == 1)
            //{

            try
            {
                var students = repository.Students.Where(x => x.IsActive).ToList();

                foreach (var student in students)
                {
                    var feeDetail = new FeeDetail
                    {
                        Created = DateTime.UtcNow,
                        CreatedBy = repository.Users.FirstOrDefault(x => x.UserName == _systemAdmin),
                        IsActive = true,
                        FeeStatus = FeeStatusValue.Pending,
                        DueDate = DateTime.Now,
                        TotalAmount = 1400,
                        Student = student,
                        PaidDate = null

                    };

                    repository.FeeDetails.Add(feeDetail);
                    repository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter streamWriter = File.CreateText("C:\\Users\\1024228\\Mywork\\Log\\SVSLog" + count++ + ".txt"))
                {
                    streamWriter.WriteLine("Message : "+ex.Message);
                    streamWriter.WriteLine("InnerException : " + ex.InnerException);
                    streamWriter.WriteLine("StackTrace : " + ex.StackTrace);
                    streamWriter.WriteLine("Target Site : " + ex.TargetSite);
                }

            }


            return Task.CompletedTask;

        }

      
    }
}