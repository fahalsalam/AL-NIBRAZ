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
        public IActionResult postSizeMaster([FromBody] SizeMaster _item)
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

        #region PUT SizeMaster
        [HttpPut("putSizeMaster")]
        public IActionResult putSizeMaster([FromHeader] int SizeId, [FromBody] Size _item)
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

        #region GET Product Master
        [AllowAnonymous]
        [HttpGet("getProductMaster")]
        public IActionResult getProductMaster([FromHeader] int page, [FromHeader] int pageSize, [FromHeader] int? productID, [FromHeader] int? categoryID, [FromHeader] int? brandID)
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
                parameters.Add("@BrandID", brandID);
                parameters.Add("@CategoryId", categoryID);
                parameters.Add("@PageNumber", page);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@UserID", 1);

                var result = _dbcontext.Query<dynamic>("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                if (result == null || !result.Any())
                {
                    _response.isSucess = false;
                    _response.message = "No data found.";
                    return NotFound(_response);
                }

                var products = result
                .GroupBy(p => p.ProductId)
                .Select(pGroup =>
            {
                var product = new ProductDto
                {
                    ProductId = pGroup.Key,
                    ProductName = pGroup.First().ProductName,
                    Description = pGroup.First().Description,
                    CategoryId = pGroup.First().CategoryId,
                    CategoryName = pGroup.First().CategoryName,
                    TaxCode = pGroup.First().TaxCode,
                    ImageUrl = pGroup.First().ImageUrl,
                    ImageUr2 = pGroup.First().ImageUr2,
                    ImageUr3 = pGroup.First().ImageUr3,
                    ImageUrl4 = pGroup.First().ImageUrl4,
                    BrandID = pGroup.First().BrandID,
                    BrandName = pGroup.First().BrandName, 
                    Price = pGroup.First().Price,
                    Discount = pGroup.First().Discount,
                    StockQty = pGroup.First().StockQty,  
                    Variants = pGroup
                        .GroupBy(v => v.VariantId)
                        .Select(vGroup =>
                        {
                            var variant = new ProductVariantGetDto
                            {
                                VariantId = vGroup.Key,
                                BrandId = vGroup.First().VariantBrandId,
                                BrandName = vGroup.First().VariantBrandName,
                                ColorId = vGroup.First().ColorId,
                                ColorName = vGroup.First().ColorName,
                                HexCode = vGroup.First().HexCode,
                                SizeId = vGroup.First().SizeId,
                                SizeLabel = vGroup.First().SizeLabel,
                                Sku = vGroup.First().Sku, // add this column in your SQL if needed

                                Price = new ProductPriceDto
                                {
                                    PriceId = vGroup.First().PriceId,
                                    Price = vGroup.First().Price,
                                    DiscountPercentage = vGroup.First().DiscountPercentage,
                                    DiscountPrice = vGroup.First().DiscountPrice,
                                    Currency = vGroup.First().Currency
                                },

                                Stock = new ProductStockDto
                                {
                                    StockId = vGroup.First().StockId,
                                    Onhand = vGroup.First().Onhand
                                },

                                Images = vGroup
                                    .Where(i => i.ImageId != null)
                                    .Select(i => new ProductImageDto
                                    {
                                        ImageId = i.ImageId,
                                        ImageUrl = i.ImageUrl,
                                        IsPrimary = i.IsPrimary
                                    }).ToList()
                            };

                            return variant;
                        }).ToList()
                };

                return product;
            }).ToList();


            _response.isSucess = true;
            _response.message = "Success";
            _response.data = products;

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
