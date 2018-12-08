using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/Stylist")]
    public ActionResult Index()
    {
      List<Stylist> allStylist = Stylist.GetAll();
      return View(allStylist);
    }
    [HttpGet("/Stylist/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/Stylist")]
    public ActionResult Create(string StylistName)
    {
      Stylist newStylist = new Stylist(StylistName, 1);
      newStylist.Save();
      List<Stylist> allStylist = Stylist.GetAll();
      return View("Index", allStylist);
    }
    [HttpGet("/Stylist/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> StylistClients = selectedStylist.GetClient();
      model.Add("Stylist", selectedStylist);
      model.Add("Client", StylistClients);
      return View(model);
    }
    [HttpPost("/Stylist/{Stylist}/Client")]
    public ActionResult Create(int  StylistID, string ClientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(StylistID);
      Client newClient = new Client(ClientName, StylistID);
      newClient.Save();
    
      List<Client> StylistClient = foundStylist.GetClient();
      model.Add("Client", StylistClient);
      model.Add("Stylist", foundStylist);
      return View("Show", model);
    }
  }
}
