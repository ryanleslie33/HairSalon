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
    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();
      Client firstClient = new Client("Mow the lawn", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Do the dishes", testStylist.GetId());
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClient();

      //Assert
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Household chores");
      Stylist secondStylist = new Stylist("Household chores");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }
  }
}
