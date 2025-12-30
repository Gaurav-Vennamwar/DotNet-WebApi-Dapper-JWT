// using System.Data;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Security.Cryptography;
// using System.Text;
// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Helpers;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Cryptography.KeyDerivation;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.SqlClient;
// using Microsoft.IdentityModel.Tokens;

// namespace DotnetAPI.Controllers
// {
//     [Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class AuthController : ControllerBase
//     {
//         private readonly DataContextDapper _dapper;
      
//         private readonly AuthHelper _authHelper;

//         public AuthController(IConfiguration config)
//         {

//             _dapper = new DataContextDapper(config);
//             _authHelper = new AuthHelper(config);
//         }

//         //this endpoint will be accessible without authentication no need of having token
//         [AllowAnonymous]
//         //this reggister end point will register the user check if he already exists or not cherck if password and confirm password are same 
//         //hash and salt the password and store the user in the database
//         [HttpPost("Register")]
//         public IActionResult Register(UserForRegisterationDto userForRegisteration)
//         {

//             //firstly going to check the password and confirm password are same
//             if (userForRegisteration.Password == userForRegisteration.ConfirmPassword)
//             {
//                 //now we will check if the user already exists 
//                 string sqlCheckUSerExists = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" +
//                   userForRegisteration.Email + "'";
//                 //return ienumerable of string with dapper to store existing users so that we can know that this all user already exists
//                 IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUSerExists);
//                 //agar user already exists nahi hai toh return ok karega agar user Already exists hai toh exception throw karega
//                 //if statement to check if the user does not exist
//                 if (existingUsers.Count() == 0)
//                 {
//                     //creating a password salt
//                     byte[] passwordSalt = new byte[128 / 8];
//                     //creating a random number generator
//                     using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
//                     {
//                         //accss the rng to fill the password salt of non zero random bytes
//                         rng.GetNonZeroBytes(passwordSalt);
//                     }



//                     //hashing the password with the help of our helper method
//                     byte[] passwordHash = _authHelper.getPasswordHash(userForRegisteration.Password, passwordSalt);

//                     //now we have to store the users email password hash and password salt in the database
//                     string sqlAddAuthUser = @"
//                     INSERT INTO TutorialAppSchema.Auth ([Email],
//                      [PasswordHash],
//                       [PasswordSalt]) VALUES('" + userForRegisteration.Email +
//                       "',@PasswordHash, @PasswordSalt)"; //USING @ TO DECLARE NEW VARIABLES

//                     //NOW we will pass the  list of parameters to the dapper to execute the query parameters are passwordhash and passwordsalt
//                     List<SqlParameter> sqlParameters = new List<SqlParameter>();

//                     //for password salt\
//                     SqlParameter passswordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
//                     //setting the value of the parameter to our password salt
//                     passswordSaltParameter.Value = passwordSalt;


//                     //for password hash
//                     SqlParameter passswordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
//                     //setting the value of the parameter to our password salt
//                     passswordHashParameter.Value = passwordHash;

//                     //add them both to our sql parameters list
//                     sqlParameters.Add(passswordSaltParameter);
//                     sqlParameters.Add(passswordHashParameter);


//                     //to check the row affected is greater than 0 or not so we can know that registration is successful or not
//                     if (_dapper.ExecuteSqlWithParameters(sqlAddAuthUser, new Dapper.DynamicParameters(sqlParameters)))
//                     {
//                         //so after the user is registered successfully we will return ok
//                         //and add the user by thier details in the user table
//                         string sqlAddUsers = @"INSERT INTO TutorialAppSchema.Users(
//                                             [FirstName],
//                                             [LastName],
//                                             [Email],
//                                             [Gender],
//                                             [Active]
//                                             ) VALUES (" +
//                                             "'" + userForRegisteration.FirstName +
//                                             "', '" + userForRegisteration.LastName +
//                                             "', '" + userForRegisteration.Email +
//                                             "', '" + userForRegisteration.Gender +
//                                             "',  1 )";
//                         if (_dapper.ExecuteSql<string>(sqlAddUsers))
//                         {
//                             //returning ok if user is added successfully
//                             return Ok();
//                         }
//                         throw new Exception("Failed to add the user");
//                     }
//                     throw new Exception("Failed to register the user");

//                 }
//                 throw new Exception("User with this email already exists");
//             }
//             throw new Exception("Passwords do not match");
//         }

//         //this endpoint will be accessible without authentication no need of having token
//         [AllowAnonymous]
//         //now setting up the login endpoint
//         //here we will veriy the user passwoed is correct or not and hash the password 
//         //entered by the user and compare it with the hashed password stored in the database
//         [HttpPost("Login")]
//         public IActionResult Login(UserForLoginDto userForLogin)
//         {
//             //first thing we gonna do for login is take thier email and pull the password hash and salt from the database
//             string sqlForHashAndSalt = @"SELECT
//             [PasswordHash],
//             [PasswordSalt]  FROM TutorialAppSchema.Auth WHERE Email = '" +
//             userForLogin.Email + "'";

//             //now we will load the data from the database with the help of dapper
//             UserForLoginConfirmationDto userForConfirmation = _dapper.LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

//             //now we have to hash the password entered by the user with the help of our helper method
//             byte[] passwordHashToCheck = _authHelper.getPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);
//             //basically this passwaordHashToCheck is the hashed password entered by the user
//             //now we will compare this passwordHashToCheck with the password hash stored in the database which is userforconfirmation.PasswordHash
//             //comparing both the byte arrays, so this is ths basic logic behind login
//             //we will not compare directly because they are objects so we will compare them byte by byte
//             for (int index = 0; index < passwordHashToCheck.Length; index++)
//             {
//                 if (passwordHashToCheck[index] != userForConfirmation.PasswordHash[index])
//                 {
//                     return StatusCode(401, "invalid password");
//                 }
//             }

//             //if we reach here that means the password is correct
//             //now we will generate a token for the user
//             //firstly we have to get the user id of the user from the database
//             string userIdSql = @"
//             SELECT UserId FROM TutorialAppSchema.Users WHERE Email = '" +
//             userForLogin.Email + "'";
//             //loading the user id from the database
//             int userId = _dapper.LoadDataSingle<int>(userIdSql);


//             return Ok(new Dictionary<string, string>
//             {
//                 {"token",_authHelper.CreateToken(userId)}
//             });
//         }
//         //new endpoint to refresh the token
//         [Authorize]
//         [HttpGet("RefreshToken")]
//         public IActionResult RefreshToken()
//         {
//             //getting the user id from the claims
//             string userId = User.FindFirst("userId")?.Value + "";

//             //now getting the user id as string
//             string userIdSql = "SELECT UserId FROM TutorialAppSchema.Users WHERE UserId = '" +
//             userId + "'";
//             //loading the user id from the database
//             int userIdFromDb = _dapper.LoadDataSingle<int>(userIdSql);

//             //returning the new token
//             return Ok(new Dictionary<string, string>
//             {
//                 {"token", _authHelper.CreateToken(userIdFromDb)}
//             });
//         }

//     }
// }