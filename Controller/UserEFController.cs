// //the sub namespaces which we have created and used in the project
// using AutoMapper;
// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// //setting up dapper in controller
// public class UserEFController : ControllerBase

// {

//     // DataConntextEF _entityframework;//got rid of the direct dependency here and  used repository pattern
//     // IMapper _mapper;

//     //adding the IUserRepository interface to the controller via dependency injection
//     IUserRepository _userRepository;
//     public UserEFController(IConfiguration config, IUserRepository userRepository)//inkected userrepository here
//     {
//         // _entityframework = new DataConntextEF(config);

//         _userRepository = userRepository;

//         // _mapper = new Mapper(new MapperConfiguration(cfg =>
//         // {
//         //     cfg.CreateMap<UserToAddDto, User>();
//         // }));


//     }



//     //endpoint to get all users
//     [HttpGet("GetUsers")]
//     // public IEnumerable<User> GetUsers()
//     public IEnumerable<User> GetUsers()
//     {

//         IEnumerable<User> users = _userRepository.GetUsers();
//         return users;

//     }

//     //endpoint to get single user by id
//     [HttpGet("GetSingleUser/{userId}")]
//     // public IEnumerable<User> GetUsers()
//     public User GetSingleUser(int userId)
//     {

//         // User? user = _entityframework.Users
//         // .Where(u => u.UserId == userId) //parameter of userid value asking to the user
//         // .FirstOrDefault<User>(); //will take the first order value only
//         // if (user != null)
//         // {

//         //     return user;
//         // }

//         // throw new Exception("User not found");

//         return _userRepository.GetSingleUser(userId); //firom repository
//     }


//     //endpoint to edit user
//     [HttpPut("EditUser")]
//     public IActionResult EditUser(User user)
//     {

//         User? userDB = _userRepository.GetSingleUser(user.UserId);
//         //    .Where(u => u.UserId == user.UserId) //parameter of userid value asking to the user
//         //    .FirstOrDefault<User>(); //will take the first order value only
//         if (userDB != null)
//         {
//             userDB.FirstName = user.FirstName;
//             userDB.LastName = user.LastName;
//             userDB.Email = user.Email;
//             userDB.Gender = user.Gender;
//             userDB.Active = user.Active;
//             if (_userRepository.SaveChanges())
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to Update User");
//         }
//         // 👇 This final return fixes the compile error
//         throw new Exception("User not found");
//     }



//     //endpoint to add user
//     [HttpPost("AddUser")]
//     public IActionResult AddUser(UserToAddDto user)
//     {

//         // Validate input
//         if (user == null)
//         {
//             return BadRequest("User data is required.");
//         }
//         //used auto mapper to map dto to model
//         // User userDB = _mapper.Map<User>(user);
//         User userDB = new User();
//         if (userDB != null)
//         {

//             userDB.FirstName = user.FirstName;
//             userDB.LastName = user.LastName;
//             userDB.Email = user.Email;
//             userDB.Gender = user.Gender;
//             userDB.Active = user.Active;

//             _userRepository.addentity<User>(userDB);

//             try
//             {
//                 if (_userRepository.SaveChanges())
//                 {
//                     return Ok();
//                 }
//                 else
//                 {
//                     return StatusCode(500, "Failed to add user.");
//                 }

//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, "An error occurred: " + ex.Message);
//             }
//         }
//         return BadRequest("Invalid user data.");
//     }

//     //endpoint to delete user
//     [HttpDelete("DeleteUser/{userId}")]
//     public IActionResult DeleteUser(int userId)
//     {
//         User? userDB = _userRepository.GetSingleUser(userId);
//         // .Where(u => u.UserId == userId)
//         if (userDB != null)
//         {
//             _userRepository.removeentity<User>(userDB);
//             if (_userRepository.SaveChanges())
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to delete User");
//         }

//         throw new Exception("User not found");
//     }

//     [HttpGet("UserSalary/{userId}")]
//     public UserSalary GetUserSalaryEF(int userId)
//     {
//         // return _entityframework.UserSalary
//         //     .Where(u => u.UserId == userId)
//         //     .ToList();
//         return _userRepository.GetUserSalary(userId);
//     }

//     [HttpPost("UserSalary")]
//     public IActionResult PostUserSalaryEf(UserSalary userForInsert)
//     {
//         _userRepository.addentity<UserSalary>(userForInsert);
//         if (_userRepository.SaveChanges())
//         {
//             return Ok();
//         }
//         throw new Exception("Adding UserSalary failed on save");
//     }


//     [HttpDelete("UserSalary/{userId}")]
//     public IActionResult DeleteUserSalaryEf(int userId)
//     {
//         UserSalary? userToDelete = _userRepository.GetUserSalary(userId);
//         // .Where(u => u.UserId == userId)
//         // .FirstOrDefault();

//         if (userToDelete != null)
//         {
//             _userRepository.removeentity<UserSalary>(userToDelete);
//             if (_userRepository.SaveChanges())
//             {
//                 return Ok();
//             }
//             throw new Exception("Deleting UserSalary failed on save");
//         }
//         throw new Exception("Failed to find UserSalary to delete");
//     }


// }

