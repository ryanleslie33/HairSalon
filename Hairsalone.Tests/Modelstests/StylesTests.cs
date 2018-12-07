using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=ryanleslie_Tests;";
    }
    public void Dispose()
    {
      Category.ClearAll();
      Item.ClearAll();
    }
