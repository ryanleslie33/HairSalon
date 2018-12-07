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
      Stylist.ClearAll();
      Client.ClearAll();
    }


    [TestMethod]
    public void StylistConstructor_CreastesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test string");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }
    [TestMethod]
    public void GetStylistName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Stylist";
      Stylist newStylist = new Stylist(name);

      //Act
      string result = newStylist.GetStylistName();

      //Assert
      Assert.AreEqual(name, result);

    }
  }
}
