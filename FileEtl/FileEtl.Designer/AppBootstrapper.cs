using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace FileEtl.Designer
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.RegisterSingleton(typeof(EventAggregator), null, typeof(EventAggregator));
            container.RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            container.RegisterSingleton(typeof(MainWindowViewModel), null, typeof(MainWindowViewModel));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}