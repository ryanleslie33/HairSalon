using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private string _ClientName;
    private int _id;
    private int _StylistId;

    public Client (string ClientName, int StylistId, int id = 0)
    {
      _ClientName = ClientName;
      _StylistId = StylistId;
      _id = id;
    }
    public string GetClientName()
    {
      return _ClientName;
    }

    public void SetClientName(string newClientName)
    {
      _ClientName = newClientName;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _StylistId;
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Client;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int ClientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(ClientName, ClientStylistId, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static void ClearAll()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"DELETE FROM Client;";
    cmd.ExecuteNonQuery();
    conn.Close();
    if (conn != null)
    {
     conn.Dispose();
    }
  }
  public static Client Find(int id)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM Client WHERE id = (@searchId);";
    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = id;
    cmd.Parameters.Add(searchId);
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int ClientId = 0;
    string ClientName = "";
    int ClientStylistId = 0;
    while(rdr.Read())
    {
      ClientId = rdr.GetInt32(0);
      ClientName = rdr.GetString(1);
      ClientStylistId = rdr.GetInt32(2);
    }
    Client newClient = new Client(ClientName, ClientStylistId, ClientId);
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return newClient;
  }
  public override bool Equals(System.Object otherClient)
{
  if (!(otherClient is Client))
  {
    return false;
  }
  else
  {
     Client newClient = (Client) otherClient;
     bool idEquality = this.GetId() == newClient.GetId();
     bool ClientEquality = this.GetClientName() == newClient.GetClientName();
     bool StylistEquality = this.GetStylistId() == newClient.GetStylistId();
     return (idEquality && ClientNameEquality && StylistEquality);
   }
}

  }
