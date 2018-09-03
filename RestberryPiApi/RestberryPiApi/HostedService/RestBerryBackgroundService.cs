using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redbus;
using Redbus.Events;
using RestberryPiApi.HostedService.Ifttt;
using RestberryPiApi.PinAccess;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace RestberryPiApi.HostedService
{
    public class RestBerryBackgroundService : IHostedService
    {
        private readonly Registry registry;
        private readonly IServiceProvider services;
        private EventBus eventBus;
        private IftttService iftttService;

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

                var actionConfig = new TriggeredActionConfig
                {
                    Name = "MeasureMoisture",
                    Trigger = new EventTrigger
                    {
                        TriggeringEventName = "MoistureMeasureInterval"
                    },
                    Action = new ReadI2CPinActionConfig
                    {
                        Name = "MoistureMeasurement",
                        I2cAddress = 123
                    }
                };

                //var uiToggleTriggerConfig = new UiToggleTriggerConfiguration
                //{
                //    EventName = "FiveSencondsInterval",
                //};

                this.eventBus = this.services.GetRequiredService<EventBus>();
                this.iftttService = this.services.GetRequiredService<IftttService>();
                eventBus.Subscribe<IftttConfigurationChange>(x => { this.iftttService.StartAndStopPreviousConfiguration(new ConfigurationWithCancellationToken(x.NewConfiguration)); });

                var configuation = new List<object> { triggerConfig, actionConfig };
                var message = new IftttConfigurationChange { NewConfiguration = configuation };
                this.eventBus.Publish(message);

                registry.Schedule(() =>
                {
                    try
                    {
                        Console.WriteLine("LALAL");
                        //var job = this.services.GetRequiredService<SomeJob>();
                        //job.Execute();
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

    public class IfffConfigurationSource
    {
        public static IObservable<IEnumerable<object>> FromEventBus(EventBus eventBus)
        {
            var subject = new Subject<IEnumerable<object>>();

            return subject;
        }
    }

    public class IftttConfigurationChange : EventBase
    {
        public IEnumerable<object> NewConfiguration { get; set; }
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
}