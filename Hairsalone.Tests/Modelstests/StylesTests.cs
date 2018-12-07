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
      Stylist newStylist = new Stylist("test stylist");
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
    [TestMethod]
  public void GetAll_ReturnsAllStylistObjects_StylistList()
  {
    //Arrange
    string name01 = "Work";
    string name02 = "School";
    Stylist newStylist1 = new Stylist(name01);
    newStylist1.Save();
    Stylist newStylist2 = new Stylist(name02);
    newStylist2.Save();
    List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

    //Act
    List<Stylist> result = Stylist.GetAll();

    //Assert
    CollectionAssert.AreEqual(newList, result);
  }
  [TestMethod]
  public void Find_ReturnsStylistInDatabase_Stylist()
  {
    //Arrange
    Stylist testStylist = new Stylist("Household chores");
    testStylist.Save();

    //Act
    Stylist foundStylist = Stylist.Find(testStylist.GetId());

    //Assert
    Assert.AreEqual(testStylist, foundStylist);
  }
  }
}
