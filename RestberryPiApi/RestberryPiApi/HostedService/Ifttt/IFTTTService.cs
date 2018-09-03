using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestberryPiApi.HostedService.Ifttt
{
    public class IftttService
    {
        public IftttService(IEnumerable<IIfttConfigHandler> configsStarters)
        {
            ConfigsStarters = configsStarters.ToDictionary(x => x.GetType());
        }

        public Dictionary<Type, IIfttConfigHandler> ConfigsStarters { get; }
        private ConfigurationWithCancellationToken currentConfiguration;

        public void StartAndStopPreviousConfiguration(ConfigurationWithCancellationToken newConfig)
        {
            Stop(currentConfiguration);
            Startup(newConfig);
            currentConfiguration = newConfig;
        }

        private void Startup(ConfigurationWithCancellationToken configWithCancellationToken)
        {
            foreach (var config in configWithCancellationToken.Configurations)
            {
                if (this.ConfigsStarters.ContainsKey(config.GetType()))
                {
                    ConfigsStarters[config.GetType()].Start(config, configWithCancellationToken.CancellationToken.Token, configWithCancellationToken.SessionGuid);
                }
                else
                {
                    throw new Exception($"Missing {nameof(IIfttConfigHandler)} for type {config.GetType()}");
                }
            }
        }

        private void Stop(ConfigurationWithCancellationToken configWithCancellationToken)
        {
            if (configWithCancellationToken != null)
            {
                configWithCancellationToken.CancellationToken.Cancel();
                foreach (var config in configWithCancellationToken.Configurations)
                {
                    if (this.ConfigsStarters.ContainsKey(config.GetType()))
                    {
                        ConfigsStarters[config.GetType()].Stop(config, configWithCancellationToken.SessionGuid);
                    }
                    else
                    {
                        throw new Exception($"Missing {nameof(IIfttConfigHandler)} for type {config.GetType()}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// TODO needs a better name
    /// </summary>
    public interface IIfttConfigHandler
    {
        Type handlesConfigFor();

        void Start(object config, CancellationToken token, Guid sessionGuid);

        void Stop(object config, Guid sessionGuid);
    }

    public class ScheduleConfigStarter : ConfigHandlerBase<ScheduledIntervalTriggerConfiguration>
    {
        public ScheduleConfigStarter()
        {
            FluentScheduler.JobManager.Start();
        }

        public override void Start(ScheduledIntervalTriggerConfiguration config, CancellationToken token, Guid sessionGuid)
        {
            var name = Jobname(config, sessionGuid);
            FluentScheduler.JobManager.AddJob(() => { }, s => s.WithName(name).ToRunEvery(config.IntervalInMilliseconds).Milliseconds());
        }

        public override void Stop(ScheduledIntervalTriggerConfiguration config, Guid sessionGuid)
        {
            FluentScheduler.JobManager.RemoveJob(Jobname(config, sessionGuid));
        }

        private static string Jobname(ScheduledIntervalTriggerConfiguration config, Guid sessionGuid)
        {
            return $"{sessionGuid.ToString()}+{config.Name}";
        }
    }

    public abstract class ConfigHandlerBase<T> : IIfttConfigHandler
    {
        public Type handlesConfigFor()
        {
            return typeof(T);
        }

        public void Start(object config, CancellationToken token, Guid sessionGuid)
        {
            Start((T)config, token, sessionGuid);
        }

        public abstract void Start(T config, CancellationToken token, Guid sessionGuid);

        public void Stop(object config, Guid sessionGuid)
        {
            Stop((T)config, sessionGuid);
        }

        public abstract void Stop(T config, Guid sessionGuid);
    }

    public class ConfigurationWithCancellationToken
    {
        public ConfigurationWithCancellationToken(IEnumerable<object> configuration)
        {
            this.Configurations = configuration;
        }

        public Guid SessionGuid { get; } = Guid.NewGuid();

        public CancellationTokenSource CancellationToken { get; } = new CancellationTokenSource();

        public IEnumerable<object> Configurations { get; }
    }

    public static class ObservableExtensions
    {
        public static IObservable<CurrentAndPreviousValue<TSource>>
        PairWithPrevious<TSource>(this IObservable<TSource> source)
        {
            return source.Scan(
                new CurrentAndPreviousValue<TSource> { },
                (acc, current) => new CurrentAndPreviousValue<TSource> { Previous = acc.Current, Current = current });
        }
    }

    public class CurrentAndPreviousValue<T>
    {
        public T Current { get; set; }
        public T Previous { get; set; }
    }
}