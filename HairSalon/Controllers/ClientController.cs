using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/Stylist/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
       Stylist stylist = Stylist.Find(stylistId);
       return View(stylist);
    }
    [HttpGet("/Stylist/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }
    [HttpPost("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return View();
    }
    [HttpGet("/Stylist/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Client client = Client.Find(clientId);
      model.Add("client", client);
      return View(model);
    }
    [HttpPost("/Stylist/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string newClientName)
    {
      Client client = Client.Find(clientId);
      client.Edit(newClientName);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      model.Add("client", client);
      return View("Show", model);
    }

  }
}
