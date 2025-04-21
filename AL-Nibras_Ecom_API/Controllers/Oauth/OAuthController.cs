using AL_Nibras_Ecom_API.Classes;
using AL_Nibras_Ecom_API.Models.General;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AL_Nibras_Ecom_API.Controllers.Oauth
{
    [Route("api/")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        protected APIResponse _response;
        private readonly JwtKey _key;
        private readonly IDbConnection _dbcontext;
        public OAuthController(IConfiguration configuration, JwtKey key, IDbConnection dbcontext, IOptions<JwtKey> options)
        {
            this._response = new();
            this._key = options.Value; 
            _dbcontext = dbcontext;
        }

        #region Login
        [HttpGet("oauth")]
        public IActionResult oauth([FromHeader] string email, [FromHeader] string Password)
        {
            try
            {
                JwtToken response = new JwtToken();

                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 100);
                parameters.Add("@Email", email);
                parameters.Add("@Password", Password);

                var data = _dbcontext.QueryMultiple("SP_Masters", parameters, commandType: CommandType.StoredProcedure);

                if (data is not null)
                {
                    var _userInfo = data.Read<dynamic>().ToList();
                    var UserID = _userInfo[0].UserId;

                    var refreshToken = DBOperation.RefreshToken();

                    /** Jwt key Decryption **/
                    var jwtKeyDecrypted = DBOperation.DecryptString(_key._jwtKey, DBOperation.APIString);

                    /** JWT Token Creation Begin.. **/
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(jwtKeyDecrypted);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("xC", "1001"),
                            new Claim("xU", UserID.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddYears(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var finalToken = tokenHandler.WriteToken(token);

                    /** Set JWT Token in Cookie **/
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddYears(1)
                    };
                    Response.Cookies.Append("AuthToken", finalToken, cookieOptions);

                    _response.isSucess = true;
                    _response.message = "Success";
                    _response.data = _userInfo;

                    return Ok(_response);
                }
                else
                {
                    _response.message = "something went wrong";
                    _response.isSucess = false;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.message = ex.Message;
                _response.isSucess = false;
                return BadRequest(_response);
            }
        }
        #endregion  
    }
}
