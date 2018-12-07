
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {

    private string _StylistName;
    private int _id;
    private List<Client> _Client;

    public Stylist(string StylistName, int id = 0)
    {
      _StylistName = StylistName;
      _id = id;
      _Client = new List<Client>{};
    }
    public string GetStylistName()
    {
      return _StylistName;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId().Equals(newStylist.GetId());
        bool nameEquality = this.GetStylistName().Equals(newStylist.GetStylistName());
        return (idEquality && nameEquality);
      }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Stylist;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Stylist> GetAll()
{
  List<Stylist> allStylist = new List<Stylist> {};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT * FROM Stylist;";
  var rdr = cmd.ExecuteReader() as MySqlDataReader;
  while(rdr.Read())
  {
    int StylistId = rdr.GetInt32(0);
    string StylistName = rdr.GetString(1);
    Stylist newStylist = new Stylist(StylistName, StylistId);
    allStylist.Add(newStylist);
  }
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
  return allStylist;
}
public void Save()
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"INSERT INTO Stylist (name) VALUES (@name);";
  MySqlParameter name = new MySqlParameter();
  name.ParameterName = "@name";
  name.Value = this._StylistName;
  cmd.Parameters.Add(name);
  cmd.ExecuteNonQuery();
  _id = (int) cmd.LastInsertedId;
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }

}
public static Stylist Find(int id)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM Stylist WHERE id = (@searchId);";
    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = id;
    cmd.Parameters.Add(searchId);
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int StylistId = 0;
    string StylistName = "";
    while(rdr.Read())
    {
      StylistId = rdr.GetInt32(0);
      StylistName = rdr.GetString(1);
    }
    Stylist newStylist = new Stylist(StylistName, StylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }
    public List<Client> GetClient()
{
  List<Client> allStylistClients = new List<Client> {};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT * FROM Client WHERE stylist_id = @Stylist_id;";
  MySqlParameter StylistId = new MySqlParameter();
  StylistId.ParameterName = "@Stylist_id";
  StylistId.Value = this._id;
  cmd.Parameters.Add(StylistId);
  var rdr = cmd.ExecuteReader() as MySqlDataReader;
  while(rdr.Read())
  {
    int ClientId = rdr.GetInt32(0);
    string ClientDescription = rdr.GetString(1);
    int ClientStylistId = rdr.GetInt32(2);
    Client newClient = new Client(ClientDescription, ClientStylistId, ClientId);
    allStylistClients.Add(newClient);
  }
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
  return allStylistClients;
}






  }
}
