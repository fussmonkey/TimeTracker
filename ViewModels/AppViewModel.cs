// Copyright (c) 2018 Applied Systems, Inc.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using Caliburn.Micro;
using core = ASI.Q.Core;
using TimeTracker.Models;

namespace TimeTracker.ViewModels
{
  [Export(typeof(AppViewModel))]
  public class AppViewModel : Screen
  {

    #region " Private Variables " 
    private Client _selectedClient;
    ////////private Note _newNote;
    private Note _selectedNote;
    private FileInfo _selectedFile;
    private bool _isClientHeaderSelected;

    #endregion


    #region " Constructor "

    [ImportingConstructor]
    public AppViewModel(SessionInfo session)
    {
      Session = session;
      Business.DB.LoadData(Session);
    }

    #endregion


    #region " Properties "

    public SessionInfo Session { get; private set; }
    public Client SelectedClient
    {
      get { return _selectedClient; }
      set
      {
        if (_selectedClient != value)
        {
          _selectedClient = value;
          NotifyOfPropertyChange(() => SelectedClient);
          ////////NewNote = new Note();
        }
      }
    }

    public Note SelectedNote
    {
      get { return _selectedNote; }
      set { if (_selectedNote != value) { _selectedNote = value; NotifyOfPropertyChange(() => SelectedNote); } }
    }

    public FileInfo SelectedFile
    {
      get { return _selectedFile; }
      set { if (_selectedFile != value) { _selectedFile = value; NotifyOfPropertyChange(() => SelectedFile); } }
    }

    public bool IsClientHeaderSelected
    {
      get { return _isClientHeaderSelected; }
      set { if (_isClientHeaderSelected != value) { _isClientHeaderSelected = value; NotifyOfPropertyChange(() => IsClientHeaderSelected); } }
    }

    ////////public Note NewNote
    ////////{
    ////////  get { return _newNote; }
    ////////  set { if (_newNote != value) { _newNote = value; NotifyOfPropertyChange(() => NewNote); } }
    ////////}

    #endregion


    #region " Private Methods " 

    protected override void OnViewLoaded(object view)
    {
      base.OnViewLoaded(view);

      if (Session == null)
      {
        Session = IoC.Get<SessionInfo>();
      }

      Session.EventAggregator.Subscribe(this);
    }

    #endregion


    #region " Public Methods "
    public void SaveAll()
    {
      Business.DB.SaveData(Session);
    }

    public void SaveClient()
    {
      Business.DB.SaveClient(Session, SelectedClient);
      SelectedNote = null;
    }

    public void Cancel()
    {
      SelectedClient = null;
    }

    public void AddClient()
    {
      SelectedClient = new Client();
      Session.Clients.Add(SelectedClient);
    }

    public void StartWork()
    {
      SelectedClient.WorkStarted = DateTime.Now;
      if (!SelectedClient.DateStarted.HasValue)
      {
        SelectedClient.DateStarted = DateTime.Now;
      }
    }
    public void StopWork()
    {
      SelectedClient.WorkStopped = DateTime.Now;
      TimeSpan ts = (SelectedClient.WorkStopped.Value - SelectedClient.WorkStarted.Value);
      SelectedClient.TotalTime += ts.TotalMinutes;
      SelectedClient.WorkStarted = null;
      SelectedClient.WorkStopped = null;
    }

    public void AddNote()
    {
      Note newNote = new Note(); // { Page = SelectedNote.Page, Body = SelectedNote.Body, IsPrivate = SelectedNote.IsPrivate };
      SelectedClient.Notes.Add(newNote);
      SelectedNote = newNote;
    }

    public void DeleteNote()
    {
      if (SelectedClient != null && SelectedNote != null)
      {
        if (MessageBox.Show("Are you sure you want to delete this note?", "Confirm Delete Note", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
          SelectedClient.Notes.Remove(SelectedNote);
        }
      }
    }

    public void DeleteFile()
    {

    }

    public void GetNotes()
    {
      StringBuilder sb = new StringBuilder();
      //sb.Append("<html><body>");
      foreach (Client client in Session.Clients.Where(c => c.IsSelected))
      {
        sb.AppendLine("<div>");
        sb.AppendLine(client.FormattedNotes);
        sb.AppendLine("</div>");
      }
      //sb.Append("</body></html>");
      ////////Clipboard.SetText(sb.ToString());
      ////////Clipboard.SetDataObject(new DataObject(DataFormats.Html, sb.ToString()), true);
      ClipboardHelper.CopyToClipboard(sb.ToString(), "blah");
    }

    public void MarkReturned()
    {
      foreach (Client client in Session.Clients.Where(c => c.IsSelected))
      {
        client.DateReturned = DateTime.Now;
      }
    }

    public void ClientChecked()
    {
      foreach (Client c in Session.Clients)
      {
        c.IsSelected = IsClientHeaderSelected;
      }
    }
    #endregion

  }
}
