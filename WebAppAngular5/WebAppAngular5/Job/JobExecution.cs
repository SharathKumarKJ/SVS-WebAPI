using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAppAngular5.WindowService
{
    public class JobExecution
    {
        private static readonly string jobName = "SVSJob";
        private static readonly string groupName = "SVSGroup";
        private static readonly string triggerName = "SVSTrigger";
        private static readonly int hours = 24;
        public static async Task Start()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };

            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var job = CreateJob(jobName, groupName);

            var trigger = CreateTrigger(triggerName,groupName);

            await scheduler.ScheduleJob(job, trigger);
        }

        private static ITrigger CreateTrigger(string triggerName, string groupName)
        {
            return TriggerBuilder.Create()
                .WithIdentity(triggerName, groupName)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(hours)
                    .RepeatForever())
                    .Build();
        }

        private static IJobDetail CreateJob(string jobName, string groupName)
        {
            return JobBuilder.Create<Job>()
                .WithIdentity(jobName, groupName)
                .Build();
        }
    }
}