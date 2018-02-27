using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
  public class Note : PropertyChangedBase
  {
    private string _page;
    private string _body;
    private bool _isPrivate;

    public string Page
    {
      get { return _page; }
      set { if (_page != value) { _page = value; NotifyOfPropertyChange(() => Page); } }
    }

    public string Body
    {
      get { return _body; }
      set { if (_body != value) { _body = value; NotifyOfPropertyChange(() => Body); } }
    }

    public bool IsPrivate
    {
      get { return _isPrivate; }
      set { if (_isPrivate != value) { _isPrivate = value; NotifyOfPropertyChange(() => IsPrivate); } }
    }

  }
}
