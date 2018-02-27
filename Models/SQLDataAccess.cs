// Copyright (c) 2018 Applied Systems, Inc.

using System;
using System.Data;
using ASI.Q.Core.DataAccess.SQL;
using ASI.Q.Core.DataAccess;

namespace TimeTracker.Models
{
  public class SQLDataAccess : Interfaces.IDataAccess
  {

    #region " Private Variables "

    private Connection Connection { get; set; }

    private SessionInfo Session { get; set; }

    #endregion


    #region " Constructors "
    public SQLDataAccess(InitConfig initConfig, SessionInfo session)
    {
      if (initConfig == null)
      {
        throw new ArgumentNullException(nameof(initConfig));
      }

      Connection = new Connection(initConfig);
      Session = session;
    }

    #endregion


    #region " Public Methods "

    #endregion

  }
}
