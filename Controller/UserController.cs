// //the sub namespaces which we have created and used in the project
// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;



// using Microsoft.AspNetCore.Mvc;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// //setting up dapper in controller
// public class UserController : ControllerBase
// {
//     DataContextDapper _dapper;
//     public UserController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }
    
//     [HttpGet("TestConnection")]
//     public DateTime TestConnection()
//     {
//         return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
//     }

// //endpoint to get all users
//     [HttpGet("GetUsers")]
//     // public IEnumerable<User> GetUsers()
//     public IEnumerable<User> GetUsers()
//     {
//         string sql = @"
//             SELECT [UserId],
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active] 
//             FROM TutorialAppSchema.Users";
//         IEnumerable<User> users = _dapper.LoadData<User>(sql);
//         return users;
//         // return new string[] {"user1", "user2" };
//         // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//         // {
//         //     Date = DateTime.Now.AddDays(index),
//         //     TemperatureC = Random.Shared.Next(-20, 55),
//         //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//         // })
//         // .ToArray();
//     }

//     //endpoint to get single user by id
//     [HttpGet("GetSingleUser/{userId}")]
//     // public IEnumerable<User> GetUsers()
//     public User GetSingleUser(int userId)
//     {
//         string sql = @"
//             SELECT [UserId],
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active] 
//             FROM TutorialAppSchema.Users
//                 WHERE UserId = " + userId.ToString(); //"7"
//         User user = _dapper.LoadDataSingle<User>(sql);
//         return user;
//     }
    
    
//     //endpoint to edit user
//     [HttpPut("EditUser")]
//     public IActionResult EditUser(User user)
//     {
        
//         string sql = @"
//         UPDATE TutorialAppSchema.Users
//             SET [FirstName] = '" + user.FirstName + 
//                 "', [LastName] = '" + user.LastName +
//                 "', [Email] = '" + user.Email + 
//                 "', [Gender] = '" + user.Gender + 
//                 "', [Active] = '" + user.Active + 
//             "' WHERE UserId = " + user.UserId;

//         Console.WriteLine(sql);
        
//       if (_dapper.ExecuteSql<User>(sql))
//         {
//             return Ok();
//         } 

//         throw new Exception("Failed to Update User");
//     }

// //endpoint to add user
//     [HttpPost("AddUser")]
//     public IActionResult AddUser(UserToAddDto user)
//     {
//         string sql = @"INSERT INTO TutorialAppSchema.Users(
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active]
//             ) VALUES (" +
//                 "'" + user.FirstName + 
//                 "', '" + user.LastName +
//                 "', '" + user.Email + 
//                 "', '" + user.Gender + 
//                 "', '" + user.Active + 
//             "')";
        
//         Console.WriteLine(sql);

//         if (_dapper.ExecuteSql<User>(sql))
//         {
//             return Ok();
//         } 

//         throw new Exception("Failed to Add User");
//     }

//     //endpoint to delete user
//     [HttpDelete("DeleteUser/{userId}")]
//     public IActionResult DeleteUser(int userId)
//     {
//         string sql = @"
//             DELETE FROM TutorialAppSchema.Users 
//                 WHERE UserId = " + userId.ToString();

//         Console.WriteLine(sql);

//         if (_dapper.ExecuteSql<User>(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Delete User");
//     }
//     //to get user salary by user id
//     [HttpGet("UserSalary/{userId}")]
//     public IEnumerable<UserSalary> GetUserSalary(int userId)
//     {
//         return _dapper.LoadData<UserSalary>(@"
//             SELECT UserSalary.UserId
//                     , UserSalary.Salary
//             FROM  TutorialAppSchema.UserSalary
//                 WHERE UserId = " + userId.ToString());
//     }
//     //to add user salary and return the added user salary
//     [HttpPost("UserSalary")]
//     public IActionResult PostUserSalary(UserSalary userSalaryForInsert)
//     {
//         string sql = @"
//             INSERT INTO TutorialAppSchema.UserSalary (
//                 UserId,
//                 Salary
//             ) VALUES (" + userSalaryForInsert.UserId.ToString()
//                 + ", " + userSalaryForInsert.Salary
//                 + ")";

//         if (_dapper.ExecuteSqlWithRowCount(sql) > 0)
//         {
//             return Ok(userSalaryForInsert);
//         }
//         throw new Exception("Adding User Salary failed on save");
//     }
//     //to update user salary and return the updated user salary
//     [HttpPut("UserSalary")]
//     public IActionResult PutUserSalary(UserSalary userSalaryForUpdate)
//     {
//         string sql = "UPDATE TutorialAppSchema.UserSalary SET Salary="
//             + userSalaryForUpdate.Salary
//             + " WHERE UserId=" + userSalaryForUpdate.UserId.ToString();

//         if (_dapper.ExecuteSql<UserSalary>(sql))
//         {
//             return Ok(userSalaryForUpdate);
//         }
//         throw new Exception("Updating User Salary failed on save");
//     }
//     //to delete user salary by user id
//      [HttpDelete("UserSalary/{userId}")]
//     public IActionResult DeleteUserSalary(int userId)
//     {
//         string sql = "DELETE FROM TutorialAppSchema.UserSalary WHERE UserId=" + userId.ToString();

//         if (_dapper.ExecuteSql<UserSalary>(sql))
//         {
//             return Ok();
//         }
//         throw new Exception("Deleting User Salary failed on save");
//     }
// }
