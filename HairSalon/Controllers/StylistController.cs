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
      Stylist newStylist = new Stylist(StylistName);
      newStylist.Save();
      List<Stylist> allStylist = Stylist.GetAll();
      return View("Index", allStylist);
    }
    [HttpGet("/Stylist/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> StylistClients = selectedStylist.GetClients();
      model.Add("Stylist", selectedStylist);
      model.Add("Client", StylistClient);
      return View(model);
    }
    [HttpPost("/Stylist/{Stylist}/Client")]
    public ActionResult Create(int Stylist, string ClientDescription)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(Stylist);
      Client newClient = new Client(ClientDescription, Stylist);
      newClient.Save();
      foundStylist.Save();
      List<Client> StylistClient = foundStylist.GetClient();
      model.Add("Client", StylistClient);
      model.Add("Stylist", foundStylist);
      return View("Show", model);
    }
