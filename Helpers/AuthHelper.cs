using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        public AuthHelper(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }



        //methhod to hash the password entered by the user 
        //a helper method to hash the password
        public byte[] getPasswordHash(string password, byte[] passwordSalt)
        {
            //getting aur password key from our app settings to make it little more secure
            //this password key will be in our code not in a database if we deploy this application somewhere 
            //then too it will be on the server not in the database so it will be more secure and 
            //difficult to hack
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value
                    + Convert.ToBase64String(passwordSalt);


            //now we will hash the password with the help of Rfc2898DeriveBytes class
            //this class uses PBKDF2 algorithm to hash the password
            return KeyDerivation.Pbkdf2(
                //we will get our password from the userforregisteration dto and salt will be our password salt plus string converted to byte array
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,//using HMACSHA256 algorithm to hash the password
                iterationCount: 1000000,//number of iterations
                numBytesRequested: 256 / 8);//length of the hash in bytes

        }

        //seting up a new helper method to generate token for the user after login
        //retturning a string token
        public string CreateToken(int userId)
        {
            //to identify the user we will use claims
            Claim[] claims = new Claim[]
            {
                new Claim("userId" , userId.ToString())
            };

            //now first we will create a security key then signing credentials and then the token itself
            //getting the secret key from our app settings   
            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        tokenKeyString != null ? tokenKeyString : ""
                    )
                );

            //now creating signing credentials
            SigningCredentials crtedentials = new SigningCredentials(
                tokenKey,
                SecurityAlgorithms.HmacSha256Signature
                );

            //now we are creating a descriptor for our token
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = crtedentials,
                //after one day this token will expire
                Expires = DateTime.UtcNow.AddDays(1)
            };
            //now setting up a token handler to create the token
            //just a class which will help us to create the token contains methods to create and write the token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //storing the token in a variable
            SecurityToken token = tokenHandler.CreateToken(descriptor);

            //returning the token as a string
            return tokenHandler.WriteToken(token);
        }

        public bool SetPassword(UserForLoginDto userForSetPassword)
        {
            byte[] passwordSalt = new byte[128 / 8];
            //creating a random number generator
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                //accss the rng to fill the password salt of non zero random bytes
                rng.GetNonZeroBytes(passwordSalt);
            }

            //hashing the password with the help of our helper method
            byte[] passwordHash = getPasswordHash(userForSetPassword.Password, passwordSalt);

            //now we have to store the users email password hash and password salt in the database
            string sqlAddAuthUser = @"EXEC TutorialAppSchema.spRegisteration_Upsert
                        @Email = @EmailParams,
                        @PasswordHash = @PasswordHashParam,  
                        @passwordsalt = @PasswordSaltParam "; //USING @ TO DECLARE NEW VARIABLES

            //NOW we will pass the  list of parameters to the dapper to execute the query parameters are passwordhash and passwordsalt and email
            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@EmailParams", userForSetPassword.Email, DbType.String);
            sqlParameters.Add("@PasswordHashParam", passwordHash, DbType.Binary);
            sqlParameters.Add("@PasswordSaltParam", passwordSalt, DbType.Binary);


            //     //NOW we will pass the  list of parameters to the dapper to execute the query parameters are passwordhash and passwordsalt and email
            //     List<SqlParameter> sqlParameters = new List<SqlParameter>();


            //     //for email
            //     SqlParameter emailParameter = new SqlParameter("@EmailParams", SqlDbType.VarChar);
            //     //setting the value of the parameter to our password salt
            //     emailParameter.Value = userForSetPassword.Email;
            //     sqlParameters.Add(emailParameter);


            //    //for password hash
            //     SqlParameter passswordHashParameter = new SqlParameter("@PasswordHashParam", SqlDbType.VarBinary);
            //     //setting the value of the parameter to our password salt
            //     passswordHashParameter.Value = passwordHash;
            //     //add them both to our sql parameters list
            //     sqlParameters.Add(passswordHashParameter);


            //     //for password salt\
            //     SqlParameter passswordSaltParameter = new SqlParameter("@PasswordSaltParam", SqlDbType.VarBinary);
            //     //setting the value of the parameter to our password salt
            //     passswordSaltParameter.Value = passwordSalt;
            //     sqlParameters.Add(passswordSaltParameter);


            //to check the row affected is greater than 0 or not so we can know that registration is successful or not
            return _dapper.ExecuteSqlWithParameters(sqlAddAuthUser, sqlParameters);

        }
    }
}