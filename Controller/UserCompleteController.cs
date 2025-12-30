//the sub namespaces which we have created and used in the project
using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers; 

[Authorize]
[ApiController]
[Route("[controller]")]
//setting up dapper in controller
public class UserCompleteController : ControllerBase
{
    private readonly DataContextDapper _dapper;
    private readonly ResuseableSQL _resuseableSQL;
    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
        _resuseableSQL = new ResuseableSQL(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    //endpoint to get all users
    [HttpGet("GetUsers/{userId}/{isActive}")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        string stringparameters = "";
       
        DynamicParameters sqlParametrs = new DynamicParameters();

        if (userId != 0)
        {
            stringparameters += ", @UserId = @UserIdParameter"; 
            sqlParametrs.Add("@UserIdParameter", userId, DbType.Int32);
        }
        if (isActive)
        {
            stringparameters += ", @Active = @ActiveParameter";
            sqlParametrs.Add("@ActiveParameter", isActive, DbType.Boolean);
        }

        if (stringparameters.Length > 0)
        {
            sql += stringparameters.Substring(1);//, parameters.Length);
            
        }

        IEnumerable<UserComplete> users = _dapper.LoadDataWithParameters<UserComplete>(sql, sqlParametrs);
        return users;
    }


    //endpoint to edit user
    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
 
        if (_resuseableSQL.UpsertUser(user))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    
    //endpoint to delete user
    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Delete
                    @UserId = @UserIdParameter";

        DynamicParameters sqlParametrs = new DynamicParameters();
        sqlParametrs.Add("@UserIdParameter", userId, DbType.Int32);


        if (_dapper.ExecuteSqlWithParameters(sql, sqlParametrs))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }


    
}
