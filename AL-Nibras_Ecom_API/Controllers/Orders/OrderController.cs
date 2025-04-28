using AL_Nibras_Ecom_API.Classes;
using AL_Nibras_Ecom_API.Models.General;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using static AL_Nibras_Ecom_API.Model.Masters.Masters;
using static AL_Nibras_Ecom_API.Model.Order.Order;

namespace AL_Nibras_Ecom_API.Controllers.Orders
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected APIResponse _response;
        private readonly JwtKey _key;
        private readonly IDbConnection _dbcontext;

        public OrderController(IDbConnection dbcontext, IOptions<JwtKey> options)
        {
            this._response = new();
            this._key = options.Value;
            _dbcontext = dbcontext;
        }

        //Wishlist//

        #region POST ProductWishlist
        [HttpPost("postProductWishlist")]
        public IActionResult postProductWishlist([FromHeader] int ProductId, [FromHeader] int VariantId)
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
                parameters.Add("@Flag", 125);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@ProductId", ProductId);
                parameters.Add("@VrId", VariantId);
                //parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region GET ProductWishlist
        [HttpGet("getProductWishlist")]
        public IActionResult getProductWishlist()
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
                parameters.Add("@Flag", 126);
                parameters.Add("@UserId", tokenClaims.UserId);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

                if (data == null || !data.Any())
                {
                    _response.isSucess = false;
                    _response.message = "No data found";
                    return NotFound(_response);
                }

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

        #region DELETE ProductWishlist
        [HttpDelete("DeleteProductWishlist")]
        public IActionResult deleteProductWishlist([FromHeader] int ProductId, [FromHeader] int varientID)
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
                parameters.Add("@Flag", 127);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@ProductId", ProductId);
                parameters.Add("@VrId", varientID);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);
                
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

        // Cart

        #region POST CART
        [HttpPost("postCart")]
        public IActionResult postCart([FromBody] cartMaster _item)
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
                parameters.Add("@Flag", 128);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@ProductId", _item.ProductId);
                parameters.Add("@VrId", _item.VariantId);
                parameters.Add("@Quantity", _item.Quantity);
                //parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region GET Cart
        [HttpGet("getCart")]
        public IActionResult getCart()
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
                parameters.Add("@Flag", 129);
                parameters.Add("@UserId", tokenClaims.UserId);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region DELETE Cart
        [HttpDelete("DeleteCart")]
        public IActionResult deleteCart([FromHeader] int ProductId, [FromHeader] int CartId, [FromHeader] int VariantId)
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

                var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, IsEncrypted: true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 130);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@ProductId", ProductId);
                parameters.Add("@CartId", CartId);
                parameters.Add("@VrId", VariantId);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region PUT CART
        [HttpPut("putCart")]
        public IActionResult putCart([FromForm] cartMaster _item, [FromHeader] int CartId)
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
                parameters.Add("@Flag", 131);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@CartId", CartId);
                parameters.Add("@Quantity", _item.Quantity);
                //parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        // Review 

        #region POST Review & Rating
        [HttpPost("postReview")]
        public IActionResult postReview([FromBody] ReviewRating _item)
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
                parameters.Add("@Flag", 132);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@ProductId", _item.ProductId);
                parameters.Add("@ReviewText", _item.ReviewText);
                parameters.Add("@Rating", _item.Rating);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        // User Address

        #region POST UserAddress
        [HttpPost("postUserAddress")]
        public IActionResult postUserAddress([FromBody] AddressDetails _item)
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
                parameters.Add("@Flag", 133);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region PUT UserAddress
        [HttpPut("putUserAddress")]
        public IActionResult putUserAddress([FromBody] AddressDetails _item, [FromHeader] int AddressId)
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
                parameters.Add("@Flag", 134);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@AddressId", AddressId);
                parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        #region GET UserAddress
        [HttpGet("getUserAddress")]
        public IActionResult getUserAddress()
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
                parameters.Add("@Flag", 135);
                parameters.Add("@UserID", tokenClaims.UserId);
                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);
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

        #region PUT UserAddressDefault
        [HttpPut("putUserAddressDefault")]
        public IActionResult putUserAddressDefault([FromHeader] bool IsDefault, [FromHeader] int AddressId)
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
                parameters.Add("@Flag", 136);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@AddressId", AddressId);
                parameters.Add("@IsDefault", IsDefault);

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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

        // Order
        
        #region postOrders
        [HttpPost("postOrders")]
        public IActionResult postOrders([FromBody] Order_Header _item)
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
                parameters.Add("@Flag", 137);
                parameters.Add("@UserId", tokenClaims.UserId);
                parameters.Add("@JsonData", JsonConvert.SerializeObject(_item));

                var data = _dbcontext.Query("SP_Orders", parameters, commandType: CommandType.StoredProcedure);

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
