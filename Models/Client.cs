using Caliburn.Micro;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
  public class Client : PropertyChangedBase
  {
    #region " Private Variables "
    private string _nameFirst;
    private string _nameLast;
    private DateTime _dateReceived;
    private DateTime? _dateStarted;
    private DateTime? _dateReturned;
    private DateTime? _workStarted;
    private DateTime? _workStopped;
    private double _totalTime;
    private BindableCollection<Note> _notes;
    private bool _localClient;
    private DateTime? _datePaid;
    private int _paidAmount;
    private bool _isSelected;
    private BindableCollection<FileInfo> _files;
    #endregion

    public Client()
    {
      Id = Guid.NewGuid();
      DateReceived = DateTime.Now;
      Notes = new BindableCollection<Note>();
      Files = new BindableCollection<FileInfo>();
    }

    #region " Properties "

    public Guid Id { get; set; }

    [LiteDB.BsonIgnore()]
    public string Name
    {
      get { return $"{LastName}, {FirstName}"; }
    }

    [LiteDB.BsonIgnore()]
    public bool IsSelected
    {
      get { return _isSelected; }
      set { if (_isSelected != value) { _isSelected = value; NotifyOfPropertyChange(() => IsSelected); } }
    }

    public string FirstName
    {
      get { return _nameFirst; }
      set
      {
        if (_nameFirst != value)
        {
          _nameFirst = value;
          NotifyOfPropertyChange(() => FirstName);
          NotifyOfPropertyChange(() => Name);
        }
      }
    }

    public string LastName
    {
      get { return _nameLast; }
      set
      {
        if (_nameLast != value)
        {
          _nameLast = value;
          NotifyOfPropertyChange(() => LastName);
          NotifyOfPropertyChange(() => Name);
        }
      }
    }

    public DateTime DateReceived
    {
      get { return _dateReceived; }
      set { if (_dateReceived != value) { _dateReceived = value; NotifyOfPropertyChange(() => DateReceived); } }
    }

    public DateTime? DateStarted
    {
      get { return _dateStarted; }
      set { if (_dateStarted != value) { _dateStarted = value; NotifyOfPropertyChange(() => DateStarted); } }
    }

    public DateTime? DateReturned
    {
      get { return _dateReturned; }
      set { if (_dateReturned != value) { _dateReturned = value; NotifyOfPropertyChange(() => DateReturned); } }
    }

    public DateTime? WorkStarted
    {
      get { return _workStarted; }
      set
      {
        if (_workStarted != value)
        {
          _workStarted = value;
          NotifyOfPropertyChange(() => WorkStarted);
          NotifyOfPropertyChange(() => CanStart);
          NotifyOfPropertyChange(() => CanStop);
        }
      }
    }

    public DateTime? WorkStopped
    {
      get { return _workStopped; }
      set
      {
        if (_workStopped != value)
        {
          _workStopped = value;
          NotifyOfPropertyChange(() => WorkStopped);
          NotifyOfPropertyChange(() => CanStart);
          NotifyOfPropertyChange(() => CanStop);
        }
      }
    }

    public double TotalTime
    {
      get { return _totalTime; }
      set { if (_totalTime != value) { _totalTime = value; NotifyOfPropertyChange(() => TotalTime); } }
    }

    public BindableCollection<Note> Notes
    {
      get { return _notes; }
      set { if (_notes != value) { _notes = value; NotifyOfPropertyChange(() => Notes); } }
    }

    public BindableCollection<FileInfo> Files
    {
      get { return _files; }
      set { if (_files != value) { _files = value; NotifyOfPropertyChange(() => Files); } }
    }

    public bool LocalClient
    {
      get { return _localClient; }
      set { if (_localClient != value) { _localClient = value; NotifyOfPropertyChange(() => LocalClient); } }
    }

    public DateTime? PaidDate
    {
      get { return _datePaid; }
      set { if (_datePaid != value) { _datePaid = value; NotifyOfPropertyChange(() => PaidDate); } }
    }

    public int PaidAmount
    {
      get { return _paidAmount; }
      set { if (_paidAmount != value) { _paidAmount = value; NotifyOfPropertyChange(() => PaidAmount); } }
    }

    [BsonIgnore]
    public bool CanStart
    {
      get { return !_workStarted.HasValue || _workStopped.HasValue; }
    }

    [BsonIgnore]
    public bool CanStop
    {
      get { return _workStarted.HasValue && !_workStopped.HasValue; }
    }

    [LiteDB.BsonIgnore()]
    public string FormattedNotes
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"<h4>{Name}</h4>");
        sb.AppendLine();
        sb.AppendLine("<ul>");
        if (Notes.Count == 0)
        {
          sb.AppendLine($"<li>No questions/notes on this one</li>");
        }
        else
        {
          foreach (Note note in Notes.Where(n => !n.IsPrivate))
          {
            if (string.IsNullOrWhiteSpace(note.Page))
            {
              sb.AppendLine($"<li>{note.Body}</li>");
            }
            else
            {
              sb.AppendLine($"<li><i>Page {note.Page}</i>: {note.Body}</li>");
            }
          }
        }
        sb.AppendLine("</ul>");
        return sb.ToString();
      }
    }
    #endregion

  }
}
