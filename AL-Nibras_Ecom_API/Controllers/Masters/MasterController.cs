using AL_Nibras_Ecom_API.Classes;
using AL_Nibras_Ecom_API.Models.General;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using static AL_Nibras_Ecom_API.Model.Masters.Masters;

namespace AL_Nibras_Ecom_API.Controllers.Masters
{
    [Route("api/")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        protected APIResponse _response;
        private readonly JwtKey _key;
        private readonly IDbConnection _dbcontext;

        public MasterController(IDbConnection dbcontext, IOptions<JwtKey> options)
        {
            this._response = new();
            this._key = options.Value;
            _dbcontext = dbcontext;
        }

        // Category Master

        #region POST CategoryMaster
        [HttpPost("postCategoryMaster")]
        public IActionResult PostItemMaster([FromBody] CategoryMaster _item)
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
                parameters.Add("@Flag", 101);
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

        #region PUT CategoryMaster
        [HttpPut("putCategoryMaster")]
        public IActionResult putCategoryMaster([FromHeader] int? categoryID, [FromHeader] bool? bulkUpdate, [FromBody] List<CategoryMaster> _item)
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
                parameters.Add("@Flag", 102);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@IsBulkUpdate", bulkUpdate);
                parameters.Add("@CategoryId", categoryID);
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

        #region GET Categorie
        [AllowAnonymous]
        [HttpGet("getCategoryMaster")]
        public IActionResult getCategoryMaster()
        {
            try
            {
                // Read the token from the cookie
                //string token = Request.Cookies["AuthToken"];

                //if (string.IsNullOrEmpty(token))
                //{
                //    _response.isSucess = false;
                //    _response.message = "Authorization token is missing in cookies.";
                //    return Unauthorized(_response);
                //}

                //var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 104);
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

        #region Delete Categories Master
        [HttpDelete("deleteCategoryMaster")]
        public IActionResult deleteCategoryMaster([FromHeader] int? categoryID)
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
                parameters.Add("@Flag", 103);
                parameters.Add("@ClientID", tokenClaims.ClientId);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@CategoryId", categoryID);

                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                if (data != null && data.AsList().Count > 0)
                {
                    _response.isSucess = false;
                    _response.message = "Failed to delete category. The category may be in use or does not exist.";
                    _response.data = data;
                    return StatusCode(500, _response);
                }

                _response.isSucess = true;
                _response.message = "Category deleted successfully.";
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

        // Brand Master

        #region POST BrandMaster
        [HttpPost("BrandMaster")]
        public IActionResult BrandMaster([FromBody] BrandMaster _item)
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
                parameters.Add("@Flag", 105);
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

        #region PUT BrandMaster
        [HttpPut("putBrandMaster")]
        public IActionResult putBrandMaster([FromHeader] int? brandID, [FromBody] BrandMaster _item)
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
                parameters.Add("@Flag", 106);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@BrandId", brandID);
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

        #region GET BrandMaster
        [AllowAnonymous]
        [HttpGet("getBrandMaster")]
        public IActionResult getBrandMaster()
        {
            try
            {
                // Read the token from the cookie
                //string token = Request.Cookies["AuthToken"];

                //if (string.IsNullOrEmpty(token))
                //{
                //    _response.isSucess = false;
                //    _response.message = "Authorization token is missing in cookies.";
                //    return Unauthorized(_response);
                //}

                //var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 108);

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

        #region Delete BrandMaster
        [HttpDelete("deleteBrandMaster")]
        public IActionResult deleteBrandMaster([FromHeader] int? brandID)
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
                parameters.Add("@Flag", 107);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@BrandID", brandID);

                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                if (data != null)
                {
                    _response.isSucess = false;
                    _response.message = "Failed to delete brand. The brand may be in use or does not exist.";
                    _response.data = data;
                    return StatusCode(500, _response);
                }

                _response.isSucess = true;
                _response.message = "Brand deleted successfully.";
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


        //Colors Master

        #region POST ColorsMaster
        [HttpPost("postColorsMaster")]
        public IActionResult postColorsMaster([FromBody] ColorsMaster _item)
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
                parameters.Add("@Flag", 112);
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

        #region PUT ColorsMaster
        [HttpPut("putColorsMaster")]
        public IActionResult putColorsMaster([FromHeader] int ColorId, [FromBody] Colors _item)
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
                parameters.Add("@Flag", 113);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@ColorId", ColorId);
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

        #region GET ColorsMaster
        [HttpGet("getColorsMaster")]
        public IActionResult getColorsMaster()
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
                parameters.Add("@Flag", 114);

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

        // Size Master

        #region POST SizeMaster
        [HttpPost("postSizeMaster")]
        public IActionResult postSizeMaster([FromHeader] string? sizeLable)
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
                parameters.Add("@Flag", 115);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@SizeLable", sizeLable);

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

        #region PUT SizeMaster
        [HttpPut("putSizeMaster")]
        public IActionResult putSizeMaster([FromHeader] int SizeId, [FromHeader] string? sizeLable, [FromHeader] bool? isActive)
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
                parameters.Add("@Flag", 116);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@SizeId", SizeId);
                parameters.Add("@SizeLable", sizeLable);
                parameters.Add("@IsActive", isActive);


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

        #region GET SizeMaster
        [HttpGet("getSizeMaster")]
        public IActionResult getSizeMaster()
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
                parameters.Add("@Flag", 119);

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

        #region PUT SizeActive
        [HttpPut("putSizeActive")]
        public IActionResult putSizeActive([FromHeader] int SizeId, [FromHeader] bool IsActive)
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
                parameters.Add("@Flag", 117);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@SizeId", SizeId);
                parameters.Add("@IsActive", IsActive);

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

        #region PUT ColorActive
        [HttpPut("putColorActive")]
        public IActionResult putColorActive([FromHeader] int ColorId, [FromHeader] bool IsActive)
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
                parameters.Add("@Flag", 118);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@ColorId", ColorId);
                parameters.Add("@IsActive", IsActive);

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

        #region GET AllMaster
        [HttpGet("GetAllMaster")]
        public IActionResult GetAllMaster()
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
                parameters.Add("@Flag", 120);

                var data = _dbcontext.QueryMultiple("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                var Category = data.Read<dynamic>().ToList();
                var Brand = data.Read<dynamic>().ToList();
                var Colors = data.Read<dynamic>().ToList();
                var Size = data.Read<dynamic>().ToList();

                var _data = new
                {
                    _category = Category,
                    _brand = Brand,
                    _Colors = Colors,
                    _Size = Size
                };

                _response.isSucess = true;
                _response.message = "Success";
                _response.data = _data;

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

        // Product Master

        #region POST ProductMaster
        [HttpPost("postProductMaster")]
        public IActionResult postProductMaster([FromBody] ProductCreateDto _item)
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
                parameters.Add("@Flag", 121);
                parameters.Add("@ClientID", tokenClaims.ClientId);
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

        #region PUT Product Master
        [HttpPut(template: "putProductMaster")]
        public IActionResult putProductMaster([FromBody] ProductCreateDto _item, [FromHeader] int? productID)
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
                parameters.Add("@ClientID", tokenClaims.ClientId);
                parameters.Add("@UserID", tokenClaims.UserId);
                parameters.Add("@ProductId", productID); 
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

        #region GET Product Master
        [AllowAnonymous]
        [HttpGet("getProductMaster")]
        public IActionResult getProductMaster([FromHeader] int page, [FromHeader] int pageSize, [FromHeader] int? productID, [FromHeader] string? productName,
            [FromHeader] int? categoryID, [FromHeader] int? brandID)
        {
            try
            {
                // Read the token from the cookie
                //string token = Request.Cookies["AuthToken"];

                //if (string.IsNullOrEmpty(token))
                //{
                //    _response.isSucess = false;
                //    _response.message = "Authorization token is missing in cookies.";
                //    return Unauthorized(_response);
                //}

                //var tokenClaims = DBOperation.GetJWTTokenClaims(token, _key._jwtKey, true);

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 122);
                parameters.Add("@ClientID", 1001);
                parameters.Add("@ProductId", productID);
                parameters.Add("@ProductName", productName); 
                parameters.Add("@BrandID", brandID);
                parameters.Add("@CategoryId", categoryID);
                parameters.Add("@PageNumber", page);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@UserID", 1);

                var result = _dbcontext.QueryMultiple("SP_Masters", parameters, commandType: CommandType.StoredProcedure);


                var productList = result.Read<dynamic>().ToList(); // Base Products
                var variantList = result.Read<dynamic>().ToList(); // Variants
                var priceList = result.Read<dynamic>().ToList();   // Prices
                var stockList = result.Read<dynamic>().ToList();   // Stocks
                var imageList = result.Read<dynamic>().ToList();   // Images
                var filterproductCount = result.Read<dynamic>().ToList();   // Filter Product Count
                var productCount = result.Read<dynamic>().ToList();   // Product Count



                var products = productList.Select(prod => new ProductDto
                {
                    ProductId = prod.ProductId,
                    ProductName = prod.ProductName,
                    Description = prod.Description,
                    CategoryId = prod.CategoryId,
                    CategoryName = prod.CategoryName,
                    TaxCode = prod.TaxCode,
                    Images = prod.Images, 
                    BrandID = prod.BrandID,
                    BrandName = prod.BrandName,
                    Price = prod.ProductPrice,
                    DiscountPrice = prod.DiscountPrice,
                    StockQty = prod.StockQty,

                    Variants = variantList
                    .Where(v => v.ProductId == prod.ProductId)
                    .GroupBy(v => v.VariantId)
                    .Select(vGroup =>
                    {
                        var v = vGroup.First();
                        return new ProductVariantGetDto
                        {
                            VariantId = v.VariantId,
                            BrandId = v.VariantBrandId,
                            BrandName = v.VariantBrandName,
                            ColorId = v.ColorId,
                            ColorName = v.ColorName,
                            HexCode = v.HexCode,
                            SizeId = v.SizeId,
                            SizeLabel = v.SizeLabel,
                            Sku = v.Sku,

                            Price = priceList.Where(p => p.VariantId == v.VariantId)
                                .Select(p => new ProductPriceDto
                                {
                                    PriceId = p.PriceId,
                                    Price = p.Price,
                                    DiscountPercentage = p.DiscountPercentage,
                                    DiscountPrice = p.DiscountPrice,
                                    Currency = p.Currency
                                }).FirstOrDefault(),

                            Stock = stockList.Where(s => s.VariantId == v.VariantId)
                                .Select(s => new ProductStockDto
                                {
                                    StockId = s.StockId,
                                    Onhand = s.Onhand
                                }).FirstOrDefault(),

                            Images = imageList.Where(i => i.VariantId == v.VariantId)
                                .Select(i => new ProductImageDto
                                {
                                    imageId = i.ImageId,
                                    imageUrl = i.ImageUrl,
                                    isPrimary = i.IsPrimary
                                }).ToList()
                        };
                    }).ToList()
                }).ToList();

                var filtertotalCount = filterproductCount.FirstOrDefault()?.TotalCount ?? 0;
                var _totalCount = productCount.FirstOrDefault()?.TotalCount ?? 0;


                var _response = new
                {
                    isSucess = true,
                    message = "Success",
                    filterTotalCount = filtertotalCount,
                    totalCount = _totalCount,
                    data = products
                };

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

        #region Product Filter
        [HttpGet("getProductFilter")]
        public IActionResult getProductFilter([FromBody] ProductFilterRequest request)
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

                var brandIDs = string.IsNullOrWhiteSpace(request.BrandIDs) ? null : request.BrandIDs;
                var categoryIDs = string.IsNullOrWhiteSpace(request.CategoryIDs) ? null : request.CategoryIDs;
                var minPrice = request.MinPrice;
                var maxPrice = request.MaxPrice;

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 126);
                parameters.Add("@ClientID", tokenClaims.ClientId);
                parameters.Add("@BrandIDs", request.BrandIDs);
                parameters.Add("@CategoryIDs", request.CategoryIDs);
                parameters.Add("@MinPrice", request.MinPrice);
                parameters.Add("@MaxPrice", request.MaxPrice);
                parameters.Add("@PageNumber", request.PageNumber);
                parameters.Add("@PageSize", request.PageSize);
                parameters.Add("@SortByPrice", request.SortByPrice);
                var data = _dbcontext.Query("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                if(data == null || !data.Any())
                {
                    _response.isSucess = false;
                    _response.message = "No products found.";
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
    }
}
