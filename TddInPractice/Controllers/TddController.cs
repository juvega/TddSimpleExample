using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using TddInPractice.Models;

namespace TddInPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TddController : ControllerBase
    {
        //private readonly IDbConnection _connection;

        //public TddController(IDbConnection connection)
        //{
        //    _connection = connection;
        //}

        [HttpPost]        
        public IActionResult AddNewElement([FromBody] Customer customer, [FromServices] IConfiguration configuration = null)        
        {
            if (configuration == null)
                throw new ArgumentException("IConfiguration is null");

            var sqlCommand = @"INSERT INTO Customer
                            (FirstName, LastName, City, Country, Phone)
                            VALUES
                            (@FirstName, @LastName, @City, @Country, @Phone);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(configuration.GetConnectionString("demo")))
            {
                connection.Open();
                var id = connection.QueryFirstOrDefault<int>(sqlCommand, customer);
                customer.Id = id;
            }

            return Ok(customer.Id);
        }
        
        /*
        public IActionResult Post([FromBody] Customer customer)
        {
            var sqlCommand = @"INSERT INTO Customer
                            (FirstName, LastName, City, Country, Phone)
                            VALUES
                            (@FirstName, @LastName, @City, @Country, @Phone);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = _connection.QueryFirstOrDefault<int>(sqlCommand, customer);
            customer.Id = id;

            return Ok(customer.Id);
        }
        */
    }
}
