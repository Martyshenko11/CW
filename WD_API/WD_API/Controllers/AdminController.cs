using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WD_API.Clients;
using WD_API.Extensions;
using WD_API.Model;

namespace WD_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IDynamoDbClient _dynamoDbClient;

        public AdminController(ILogger<AdminController> logger, IAmazonDynamoDB dynamoDb, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDbClient;
        }

        [HttpGet("get/allordersbydate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOrdersInfoByDate(string Order_Date)
        {
            var result = await _dynamoDbClient.GetAllOrdersByDate(Order_Date);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            return Ok(result);
        }


        [HttpGet("get/userinfobyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string User_Id)
        {
            var result = await _dynamoDbClient.GetUserInfoById(User_Id);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            var orderInfoResponse = new UserDbRepository
            {
                Phone = result.Phone, 
                City = result.City,
                Address = result.Address,
                Checker = result.Checker
            };

            return Ok(orderInfoResponse);
        }


        [HttpGet("get/userinfobyphone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByPhone(string Phone)
        {
            var result = await _dynamoDbClient.GetUserInfoByPhone(Phone);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            return Ok(result);
        }


    }
}
