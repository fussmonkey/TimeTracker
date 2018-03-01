// Copyright (c) 2018 Applied Systems, Inc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using core = ASI.Q.Core;
using Caliburn.Micro;
using ASI.Q.Core.DataAccess;
using TimeTracker.Interfaces;

namespace TimeTracker.Models
{
  public class SessionInfo
  {

    #region " Private Variables "

    
    #endregion


    #region " Constructor "

    public SessionInfo(IWindowManager windowManager, IEventAggregator eventAggregator, string server, string database)
    {
      WindowManager = windowManager;
      EventAggregator = eventAggregator;
    }

    #endregion


    #region " Properties "

    public IWindowManager WindowManager { get; private set; }
    public IEventAggregator EventAggregator { get; private set; }
    
    public static Guid ToolGuid { get { return new Guid("00000000-0000-0000-0000-000000000000"); } }
       

    public string SettingsPath
    {
      get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.db"); }
    }

    public BindableCollection<Client> Clients { get; set; }
    #endregion

  }
}
