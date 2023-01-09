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
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IDynamoDbClient _dynamoDbClient;

        public OrderController(ILogger<OrderController> logger, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDbClient;
        }


        [HttpPost("add/order")]
        public async Task<IActionResult> AddToOrder([FromBody] OrderMainResponse mainOrder)
        {
            var data = new OrderDbRepository
            {
                Order_Id = mainOrder.Order_Id,
                User_Id = mainOrder.User_Id,
                Water_Brand = mainOrder.Water_Brand,
                Volume = mainOrder.Volume,
                Num_Water = mainOrder.Num_Water,
                Price = mainOrder.Price             
            };

            var result = await _dynamoDbClient.PostOrderToDb(data);

            if (result == false)
            {
                return BadRequest("Cannot insert value to database. Please see console log");
            }

            return Ok("Value has been successfully added to DB");
        }


        [HttpGet("get/totalorderinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderInfo([FromQuery] string User_Id)
        {
            var result = await _dynamoDbClient.GetOrderInfoByUser(User_Id);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            return Ok(result);
        }


        [HttpGet("get/checkisorderinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCheckIsOrderInfo([FromQuery] string User_Id, string Water_Brand, string Volume)
        {
            var result = await _dynamoDbClient.GetCheckIsOrderInfoByUser(User_Id, Water_Brand, Volume);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            return Ok(result);
        }

        [HttpGet("get/tempinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTempInfo([FromQuery] string User_Id)
        {
            var result = await _dynamoDbClient.GetTempInfoByUser(User_Id);

            if (result == null)
                return NotFound("Record doesn't exist in database");

            var orderInfoResponse = new OrderTempResponse
            {
                Water_Brand = result.Water_Brand,
                Volume = result.Volume,
                Num_Water = result.Num_Water
            };

            return Ok(orderInfoResponse);
        }


        [HttpPatch("update/water_brand")]
        public async Task<IActionResult> UpdateBrandOrder ([FromBody] OrderBrandResponse mainOrder)
        {
            var data = new TempDbRepository
            {
                User_Id = mainOrder.User_Id,
                Water_Brand = mainOrder.Water_Brand
            };

            await _dynamoDbClient.UpdateBrandToDb(data);

            return Ok();
        }

        [HttpPatch("update/volume")]
        public async Task<IActionResult> UpdateVolumeOrder([FromBody] OrderVolumeResponse mainOrder)
        {
            var data = new TempDbRepository
            {
                User_Id = mainOrder.User_Id,
                Volume = mainOrder.Volume
            };

            await _dynamoDbClient.UpdateVolumeToDb(data);

            return Ok();
        }

        [HttpPatch("update/number")]
        public async Task<IActionResult> UpdateNumberOrder([FromBody] OrderNumberResponse mainOrder)
        {
            var data = new TempDbRepository
            {
                User_Id = mainOrder.User_Id,
                Num_Water = mainOrder.Num_Water,
            };
            
            await _dynamoDbClient.UpdateNumberToDb(data);

            return Ok();
        }


        [HttpPatch("update/date")]
        public async Task<IActionResult> UpdateDateOrder([FromBody] OrderDateResponse mainOrder)
        {
            var data = new OrderDbRepository
            {
                Order_Id = mainOrder.Order_Id,
                Order_Date = mainOrder.Order_Date
            };

            await _dynamoDbClient.UpdateDateIntoOrder(data);

            return Ok();
        }


        [HttpPatch("update/time")]
        public async Task<IActionResult> UpdateTimeOrder([FromBody] OrderTimeResponse mainOrder)
        {
            var data = new OrderDbRepository
            {
                Order_Id = mainOrder.Order_Id,
                Order_Time = mainOrder.Order_Time
            };

            await _dynamoDbClient.UpdateTimeIntoOrder(data);

            return Ok();
        }


        [HttpDelete("delete/orderinfo")]
        public async Task<IActionResult> Delete([FromQuery] string User_Id)
        {
            var result = await _dynamoDbClient.GetOrderInfoByUser(User_Id);
            if (result == null)
                return NotFound("Record doesn't exist in database");
            await _dynamoDbClient.DeleteTrashOrder(User_Id);

            return Ok();
        }

    }
}
