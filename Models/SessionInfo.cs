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

    private string _userDisplayName;
    private IDataAccess _dataAccess;

    #endregion


    #region " Constructor "

    public SessionInfo(IWindowManager windowManager, IEventAggregator eventAggregator, string server, string database)
    {
      WindowManager = windowManager;
      EventAggregator = eventAggregator;
      UserGuid = Guid.Parse(core.Windows.Environment.GetGuidFromUserName(core.Windows.Environment.GetWindowsUserName()));
      UserDisplayName = core.Windows.Environment.GetDisplayNameFromUserName(core.Windows.Environment.GetWindowsUserName());
      Server = server;
      Database = database;
    }

    #endregion


    #region " Properties "

    public IWindowManager WindowManager { get; private set; }
    public IEventAggregator EventAggregator { get; private set; }
    internal string Server { get; set; }
    internal string Database { get; set; }

    public static Guid ToolGuid { get { return new Guid("00000000-0000-0000-0000-000000000000"); } }

    [DAWrite("@userGuid")]
    public Guid UserGuid { get; private set; }

    [DAWrite("@insertedBy")]
    [DAWrite("@updatedBy")]
    public string UserDisplayName
    {
      get { return _userDisplayName; }
      set { if (_userDisplayName == null || _userDisplayName.Length == 0) { _userDisplayName = value; } }
    }

    public string SettingsPath
    {
      get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.db"); }
    }

    public BindableCollection<Client> Clients { get; set; }
    #endregion

  }
}
