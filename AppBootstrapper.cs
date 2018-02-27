// Copyright (c) 2018 Applied Systems, Inc.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using MahApps.Metro.Controls;

using TimeTracker.Models;

namespace TimeTracker
{
  class AppBootstrapper : BootstrapperBase
  {
    private CompositionContainer _container;

    public AppBootstrapper()
    {
      Initialize();
    }

    ////////private string GetSettingsPath()
    ////////{
    ////////  return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");
    ////////}

    protected override void Configure()
    {
      _container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));
      CompositionBatch batch = new CompositionBatch();

      IWindowManager windowManager = new WindowManager();
      IEventAggregator eventAggregator = new EventAggregator();

      SessionInfo session = new SessionInfo(windowManager, eventAggregator, ConfigurationManager.AppSettings["server"], ConfigurationManager.AppSettings["database"]);
      batch.AddExportedValue(session);

      _container.Compose(batch);
    }

    protected override object GetInstance(Type serviceType, string key)
    {
      string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
      var exports = _container.GetExportedValues<object>(contract);

      if (exports.Count() > 0)
      {
        return exports.First();
      }

      throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
    }

    protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
    {
      var viewModel = IoC.Get<ViewModels.AppViewModel>();
      IWindowManager windowManager;

      try
      {
        windowManager = IoC.Get<IWindowManager>();
      }
      catch
      {
        windowManager = new WindowManager();
      }

      Dictionary<string, object> settings = new Dictionary<string, object>();
      settings.Add("Title", "T3 - Tax Time Tracker");

      windowManager.ShowWindow(viewModel, null, settings);
    }

    protected override void OnExit(object sender, EventArgs e)
    {
      base.OnExit(sender, e);
    }

    protected async override void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      MetroWindow window = (MetroWindow)IoC.Get<ViewModels.AppViewModel>().GetView();

      if (window != null)
      {
        await ASI.Q.Core.WPF.MahApps.Metro.Help.ShowError(window, "Unhandled Exception!", e.Exception.Message);
      }
      else
      {
        ASI.Q.Tool.SDK.Help.Error.Show(e.Exception, "Unhandled Exception!");
      }

      e.Handled = true;
    }
  }
}
