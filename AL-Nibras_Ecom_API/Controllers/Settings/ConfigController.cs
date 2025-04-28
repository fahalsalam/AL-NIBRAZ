using AL_Nibras_Ecom_API.Classes;
using AL_Nibras_Ecom_API.Models.General;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Data;
using static AL_Nibras_Ecom_API.Model.Masters.Masters;

namespace AL_Nibras_Ecom_API.Controllers.Settings
{
    [Route("api/")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        protected APIResponse _response;
        private readonly JwtKey _key;
        private readonly IDbConnection _dbcontext;
        public ConfigController(IDbConnection dbcontext, IOptions<JwtKey> options)
        {
            _response = new();
            _key = options.Value;
            _dbcontext = dbcontext;
        }

        //Ecommerce Config

        #region POST EcommerceConfig
        [HttpPost("postEcommerceConfig")]
        public IActionResult postEcommerceConfig([FromBody] EcommerceConfiy _item)
        {
            try
            {
                // Read the token from the cookie
                string token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    _response.isSucess = false;
                    _response.message = "Authorization token is missing in cookies.";
                    return Unauthorized(_response);
                }

                var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 109);
                parameters.Add("@ClientId", tokenClaims.ClientId);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                _response.isSucess = true;
                _response.message = "Success";
                _response.data = data;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSucess = false;
                _response.message = ex.Message;

                return StatusCode(500, _response);
            }
        }

        #endregion

        #region PUT EcommerceConfig
        [HttpPut("putEcommerceConfig")]
        public IActionResult putEcommerceConfig([FromBody] EcommerceConfiy _item)
        {
            try
            {
                // Read the token from the cookie
                string token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    _response.isSucess = false;
                    _response.message = "Authorization token is missing in cookies.";
                    return Unauthorized(_response);
                }

                var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 110);
                parameters.Add("@ClientId", tokenClaims.ClientId);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                _response.isSucess = true;
                _response.message = "Success";
                _response.data = data;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSucess = false;
                _response.message = ex.Message;

                return StatusCode(500, _response);
            }
        }
        #endregion

        #region GET EcommerceConfig
        [HttpGet("getEcommerceConfig")]
        public IActionResult getEcommerceConfig()
        {
            try
            {
                //// Read the token from the cookie
                //string token = Request.Cookies["AuthToken"];

                //if (string.IsNullOrEmpty(token))
                //{
                //    _response.isSucess = false;
                //    _response.message = "Authorization token is missing in cookies.";
                //    return Unauthorized(_response);
                //}

                //var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 111);
                //parameters.Add("@ClientId", tokenClaims.ClientId);
                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);
                _response.isSucess = true;
                _response.message = "Success";
                _response.data = data;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSucess = false;
                _response.message = ex.Message;
                return StatusCode(500, _response);
            }
        }
        #endregion 
    }
}
