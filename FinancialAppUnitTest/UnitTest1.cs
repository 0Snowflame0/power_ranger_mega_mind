using _22SevenFincancialApp.Controllers;
using _22SevenFincancialApp.Models.Context;
using _22SevenFincancialApp.Models.EntitySets;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinancialAppUnitTest
{
  public class UnitTest1
  {
    DbContextOptions<FinancialContext> options = new DbContextOptions<FinancialContext>();
    /// <summary>
    /// Used to populate the database with some accounts. and to see if the call works ofcoarse 
    /// </summary>
    [Fact]
    public async void TestCreateUser()
    {
      //lets create a little object with our request
      var request = new List<CustomerData>{ 
        new CustomerData()
      {
        Id = 1,
        Name = "Arisha Barron",
        Address = "We dont really care",
        Age = 25,
        Email = "AB@gmail.com"
      },new CustomerData(){
        Id = 2,
        Name = "Branden Gibson",
        Address = "We dont really care",
        Age = 25,
        Email = "AB@gmail.com"
      },new CustomerData(){
        Id = 3,
        Name = "Rhonda Church",
        Address = "We dont really care",
        Age = 25,
        Email = "AB@gmail.com"
      } };
      //to test api we will need to build a little magic
      var controller = new FinancialController(new FinancialContext(options));

      foreach (var customer in request) 
      {
        //populate all users.
        var response = await controller.PostCreateCustomer(customer);
        Assert.True(response?.Result?.Equals(JsonConvert.SerializeObject(new { responseCode = 00, responseMessage = "Customer Created Successfully" })));
      }
    }
  }
}