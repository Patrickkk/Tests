using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redbus;
using Redbus.Events;
using RestberryPiApi.PinAccess;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestberryPiApi.HostedService
{
    public class RestBerryBackgroundService : IHostedService
    {
        private readonly Registry registry;
        private readonly IServiceProvider services;

        public RestBerryBackgroundService(IServiceProvider services)
        {
            JobManager.JobException += JobManager_JobException;
            this.registry = new Registry();
            this.registry.NonReentrantAsDefault();
            this.services = services;
        }

        private void JobManager_JobException(JobExceptionInfo obj)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var triggerConfig = new ScheduledIntervalTriggerConfiguration
                {
                    IntervalInMilliseconds = TimeSpan.FromSeconds(5).Milliseconds,
                    Name = "MoistureMeasureInterval"
                };

                var actionConfig = new ActionConfig
                {
                    Name = "MeasureMoisture",
                    Trigger = new EventTrigger
                    {
                        TriggeringEventName = "MoistureMeasureInterval"
                    },
                    Action = new ReadI2CPinActionConfig
                    {
                        Name = "MoistureMeasurement"
                    }
                };

                var uiToggleTriggerConfig = new UiToggleTriggerConfiguration
                {
                    EventName = "FiveSencondsInterval",
                };

                this.services.GetRequiredService<ReadPinAction>();

                registry.Schedule(() =>
                {
                    try
                    {
                        var job = this.services.GetRequiredService<SomeJob>();
                        job.Execute();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }).ToRunEvery(2).Seconds();
                JobManager.Initialize(registry);
            }
            catch (Exception ex)
            {
                var a = ex;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class SomeJob
    {
        private readonly IPiPinsService pins;

        public SomeJob(IPiPinsService pins)
        {
            this.pins = pins;
        }

        public void Execute()
        {
            var pinnumber = 1;
            Console.WriteLine($"reading pin {pinnumber} value: {pins.ReadModeAndRead(pinnumber)}");
        }
    }

    public interface IEventAction
    {
        void Execute(EventBase eventMessage);
    }

    public class ReadPinAction : IEventAction, INamedEventEmitter
    {
        private readonly IPiPinsService pins;
        private readonly EventBus eventBus;

        public ReadPinAction(IPiPinsService pins, EventBus eventBus)
        {
            this.pins = pins;
            this.eventBus = eventBus;
        }

        public string EventName { get; set; }

        public void Execute(EventBase eventMessage)
        {
            var pinnumber = 1;
            var pinValue = pins.ReadModeAndRead(pinnumber);
            Console.WriteLine($"reading pin {pinnumber} value: {pinValue}");
            this.eventBus.PublishAsync(new NamedEvent { Name = EventName, Value = pinValue });
        }
    }

    public class ScheduledJob : IJob, INamedEventEmitter
    {
        private readonly IPiPinsService pins;
        private readonly EventBus eventBus;

        public ScheduledJob(IPiPinsService pins, EventBus eventBus)
        {
            this.pins = pins;
            this.eventBus = eventBus;
        }

        public string EventName { get; set; }

        public void Execute()
        {
            eventBus.Publish(new ScheduledEvent { Name = EventName });
        }
    }

    public interface INamedEventEmitter
    {
        string EventName { get; set; }
    }

    public class ScheduledEvent : NamedEvent
    {
        public DateTime TriggerTime { get; set; } = DateTime.UtcNow;
    }

    public class NamedEvent : EventBase
    {
        public string Name { get; set; }

        public dynamic Value { get; set; }
    }

    public class ScheduledIntervalTriggerConfiguration
    {
        public int IntervalInMilliseconds { get; set; }

        public string Name { get; set; }

        public string FullEventName { get; set; }
    }

    public class NamedEventEmitterConfiguration
    {
    }
}