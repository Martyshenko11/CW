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
    public class MainController : ControllerBase
    {
       

        private readonly ILogger<MainController> _logger;
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IDynamoDbClient _dynamoDbClient;

        public MainController(ILogger<MainController> logger, IAmazonDynamoDB dynamoDb, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDb = dynamoDb;
            _dynamoDbClient = dynamoDbClient;
        }

        [HttpPatch("update/phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] UserPhoneResponse mainUser)
        {
            var data = new UserDbRepository
            {
                User_Id = mainUser.User_Id,
                Phone = mainUser.Phone
            };

            await _dynamoDbClient.UpdatePhoneToDb(data);

            return Ok();
        }
        

        [HttpPatch("update/city")]
        public async Task<IActionResult> UpdateData([FromBody] UserCityResponse mainUser)
        {
            var data = new UserDbRepository()
            {
                User_Id = mainUser.User_Id,
                City = mainUser.City
            };

            await _dynamoDbClient.UpdateDataIntoDb(data);

            return Ok();
        }


        [HttpPatch("update/address")]
        public async Task<IActionResult> UpdateAddress([FromBody] UserAddressResponse mainUser)
        {
            var data = new UserDbRepository()
            {
                User_Id = mainUser.User_Id,
                Address = mainUser.Address
            };

            await _dynamoDbClient.UpdateAddressIntoDb(data);

            return Ok();
        }
        

        [HttpPatch("update/checker")]
        public async Task<IActionResult> AddToDelivery([FromBody] UserStatusResponse mainOrder)
        {
            var data = new UserDbRepository
            {
                User_Id = mainOrder.User_Id,
                Checker = mainOrder.Checker
            };

            await _dynamoDbClient.UpdateDeliveryToDb(data);

            return Ok();
        }


        [HttpGet("get/userverify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVerify([FromQuery] string User_Id)
        {
            var result = await _dynamoDbClient.GetVerifyInfoByUser(User_Id);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            var orderInfoResponse = new UserVerifyResponse
            {
                Phone = result.Phone,
                City = result.City,
                Address = result.Address
            };

            return Ok(orderInfoResponse);
        }


        [HttpGet("get/statuschecker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChecker([FromQuery] string User_Id)
        {
            var result = await _dynamoDbClient.GetStatusInfoByUser(User_Id);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            var orderInfoResponse = new UserStatusResponse
            {
                Checker = result.Checker             
            };

            return Ok(orderInfoResponse);
        }

        //[HttpGet]
        //public async Task<GetItemResponse> GetDataFromDb()
        //{
        //    var tableName = "wd-db";

        //    var item = new GetItemRequest
        //    {
        //        TableName = tableName,
        //        Key = new Dictionary<string, AttributeValue>
        //        {
        //            {"User_Id", new AttributeValue{S = "12" } }
        //        }
        //    };

        //    var response = await _dynamoDb.GetItemAsync(item);
        //    //if (response.Item == null || !response.IsItemSet)
        //    //    return null;

        //   // var result = response.Item.ToClass<CityDbRepository>();

        //    return response;

        //}

    }
}
