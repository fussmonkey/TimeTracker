using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.Models;
using TimeTracker.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace TimeTracker.Views
{
  public partial class AppView : MetroWindow
  {
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    public AppView()
    {
      InitializeComponent();
    }

    private void Grid_DragEnter(object sender, System.Windows.DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop) || sender == e.Source)
      {
        e.Effects = DragDropEffects.None;
      }
      else
      {
        Console.WriteLine("drag is working in grid");
      }
    }

    private void Grid_Drop(object sender, System.Windows.DragEventArgs e)
    {
      ///////if (e.Data.GetDataPresent(DataFormats.FileDrop))
      ///////{
      ///////  Contact contact = e.Data.GetData("myFormat") as Contact;
      ///////  ListView listView = sender as ListView;
      ///////  listView.Items.Add(contact);
      ///////}

      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
        {
          if (Path.GetExtension(file) == "pdf")
          {
            //Grid grid = sender as Grid;
            //Client client = grid.DataContext as Client;
            //client.Notes.Add(new Note() { Body = file });
            MessageBox.Show(file);
          }
        }
      }

    }

    private void TextBox_DragEnter(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop) || sender == e.Source)
      {
        e.Effects = DragDropEffects.None;
      }
      else
      {
        Console.WriteLine("drag is working in textbox");
      }
    }

    private void TextBox_Drop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
        {
          if (Path.GetExtension(file) == "pdf")
          {
            //Grid grid = sender as Grid;
            //Client client = grid.DataContext as Client;
            //client.Notes.Add(new Note() { Body = file });
            MessageBox.Show(file);
          }
        }
      }
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
      if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled) // Checks Capslock is on
      {
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
        keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
        (UIntPtr)0);
      }
    }

    private void DataGrid_DragEnter(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop) || sender == e.Source)
      {
        e.Effects = DragDropEffects.None;
      }
      else
      {
        Console.WriteLine("drag is working in datagrid");
      }
    }

    private void DataGrid_Drop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
        {
          if (Path.GetExtension(file) == ".pdf")
          {
            DataGrid grid = sender as DataGrid;
            var ctx = grid.DataContext as AppViewModel;
            System.IO.FileInfo fi = new System.IO.FileInfo(file);
            ctx.SelectedClient.Files.Add(new Models.FileInfo(fi)); //////// { FilePath = file, Length = fi.Length });
          }
        }
      }
    }
  }
}
