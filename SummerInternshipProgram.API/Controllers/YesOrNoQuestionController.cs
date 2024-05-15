﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerInternshipProgram.API.Constants;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Helpers.Implementation;
using SummerInternshipProgram.API.Helpers.Interface;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;
using SummerInternshipProgram.API.Services.Interfaces;

namespace SummerInternshipProgram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YesOrNoQuestionController : ControllerBase
    {
        private readonly ILogHelper _logger;
        private string classname = nameof(YesOrNoQuestionController);
        private IYesOrNoQuestionService _dbService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public YesOrNoQuestionController(ILogHelper logger, IYesOrNoQuestionService db, IWebHostEnvironment webHostEnvironment)
        {
            _dbService = db;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllData([FromHeader] string? applicantId)
        {
            var methodName = $" {classname}/{nameof(GetAllData)}";
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestId = GeneralHelper.GetNewRequestId();
            _logger.LogInformation(requestId, "New Process", Ip, methodName);
            try
            {
                var dbResponse = await _dbService.GetAll(new BaseRequestDto<string>()
                {
                    Ip = Ip,
                    Request = "",
                    RequestId = requestId
                },
                applicantId);
                if (dbResponse.ResponseCode == GeneralResponse.sucessCode)
                {
                    return Ok(dbResponse);
                }
                else
                {
                    return BadRequest(dbResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestId, $"Failed", Ip, methodName, ex);
                return BadRequest();
            }


        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetSingle([FromBody] string Id, [FromHeader] string? applicantId)
        {
            var methodName = $" {classname}/{nameof(GetSingle)}";
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestId = GeneralHelper.GetNewRequestId();
            _logger.LogInformation(requestId, "New Process", Ip, methodName);
            try
            {
                var dbResponse = await _dbService.GetSingle(new BaseRequestDto<IdPayload>()
                {
                    Ip = Ip,
                    Request = new IdPayload()
                    {
                        Id = Id
                    },
                    RequestId = requestId
                }, applicantId);
                _logger.LogInformation(requestId, $"New Process", Ip, methodName);
                if (dbResponse.ResponseCode == GeneralResponse.sucessCode)
                {
                    return Ok(dbResponse);
                }
                else
                {
                    return BadRequest(dbResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestId, $"Failed", Ip, methodName, ex);
                return BadRequest();
            }

        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddSingle([FromBody] YesOrNoQuestion input, [FromHeader] string? applicantId)
        {
            var methodName = $" {classname}/{nameof(AddSingle)}";
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestId = GeneralHelper.GetNewRequestId();
            _logger.LogInformation(requestId, "New Process", Ip, methodName);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var dbResponse = await _dbService.Add(new BaseRequestDto<YesOrNoQuestion>()
                {
                    Ip = Ip,
                    Request = input,
                    RequestId = requestId
                }, applicantId);
                if (dbResponse.ResponseCode == GeneralResponse.sucessCode)
                {
                    return Ok(dbResponse);
                }
                else
                {
                    return BadRequest(dbResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestId, $"Failed", Ip, methodName, ex);
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSingle([FromBody] YesOrNoQuestion input, [FromForm] string? applicantId)
        {
            var methodName = $" {classname}/{nameof(UpdateSingle)}";
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestId = GeneralHelper.GetNewRequestId();
            _logger.LogInformation(requestId, "New Process", Ip, methodName);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var dbResponse = await _dbService.Update(new BaseRequestDto<YesOrNoQuestion>()
                {
                    Ip = Ip,
                    Request = input,
                    RequestId = requestId
                },
                 applicantId);
                if (dbResponse.ResponseCode == GeneralResponse.sucessCode)
                {
                    return Ok(dbResponse);
                }
                else
                {
                    return BadRequest(dbResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestId, $"Failed", Ip, methodName, ex);
                return BadRequest();
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSingle([FromBody] IdPayload Id, [FromHeader] string? applicantId)
        {
            var methodName = $" {classname}/{nameof(DeleteSingle)}";
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var requestId = GeneralHelper.GetNewRequestId();
            _logger.LogInformation(requestId, "New Process", Ip, methodName);
            try
            {
                var dbResponse = await _dbService.Delete(new BaseRequestDto<IdPayload>()
                {
                    Ip = Ip,
                    Request = Id,
                    RequestId = requestId
                }, applicantId);
                _logger.LogInformation(requestId, $"New Process", Ip, methodName);
                if (dbResponse.ResponseCode == GeneralResponse.sucessCode)
                {
                    return Ok(dbResponse);
                }
                else
                {
                    return BadRequest(dbResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestId, $"Failed", Ip, methodName, ex);
                return BadRequest();
            }

        }
    }
}
