using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using TimeTracker.Models;

namespace TimeTracker.Business
{
  public static class DB
  {
    internal static void LoadData(SessionInfo session)
    {
      using (var db = new LiteDatabase(session.SettingsPath))
      {
        // get collection
        var clientCollection = db.GetCollection<Client>("clients");
        var clients = clientCollection.FindAll();

        session.Clients = new Caliburn.Micro.BindableCollection<Client>(clients);
      }
    }

    internal static void SaveData(SessionInfo session)
    {
      using (var db = new LiteDatabase(session.SettingsPath))
      {
        // get collection
        var clientCollection = db.GetCollection<Client>("clients");
        foreach (Client client in session.Clients)
        {
          Client dbClient = clientCollection.FindById(client.Id);
          if (dbClient != null)
          {
            clientCollection.Update(client);
          }
          else
          {
            clientCollection.Insert(client);
          }
        }
      }
    }

    internal static void SaveClient(SessionInfo session, Client selectedClient)
    {
      using (var db = new LiteDatabase(session.SettingsPath))
      {
        // get collection
        var clientCollection = db.GetCollection<Client>("clients");
        Client dbClient = clientCollection.FindById(selectedClient.Id);
        if (dbClient != null)
        {
          clientCollection.Update(selectedClient);
        }
        else
        {
          clientCollection.Insert(selectedClient);
        }
      }
    }
  }
}
