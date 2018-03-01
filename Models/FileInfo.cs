using Caliburn.Micro;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
  public class FileInfo : PropertyChangedBase
  {
    private string _filePath;
    private long _length;
    private string _fileName;

    public FileInfo() { }

    public FileInfo(System.IO.FileInfo fi)
    {
      FilePath = fi.FullName;
      Length = fi.Length;
      //FileName = fi.Name;
    }

    public string FilePath
    {
      get { return _filePath; }
      set { if (_filePath != value) { _filePath = value; NotifyOfPropertyChange(() => FilePath); } }
    }

    [BsonIgnore]
    public string FileName
    {
      get { return System.IO.Path.GetFileName(_filePath); }
      ////////get { return _fileName; }
      ////////set { if (_fileName != value) { _fileName = value; NotifyOfPropertyChange(() => FileName); } }
    }

    public long Length
    {
      get { return _length; }
      set { if (_length != value) { _length = value; NotifyOfPropertyChange(() => Length); } }
    }

    [BsonIgnore]
    public string LengthFormatted
    {
      get { return $"{_length / 1024} KB"; }
    }
  }
}
