// using DotnetAPI.Models;

// namespace DotnetAPI.Data
// {

//     public class UserRepository : IUserRepository//inheriting interface
//     {
//         //dataccontext ef ko dala here
//         DataConntextEF _entityframework;


//         //phir contructor banaya aur config pass kiya connection build karne ke liye
//         public UserRepository(IConfiguration config)
//         {
//             _entityframework = new DataConntextEF(config);
//         }

//         //method to save changes to database
//         public bool SaveChanges()
//         {
//             return (_entityframework.SaveChanges() > 0);
//         }

//         //method to add 
//         //generic type T jimeh we can pass any type of model
//         // public bool addentity<T>( T entityToAdd)
//         public void addentity<T>(T entityToAdd)
//         {
//             if (entityToAdd != null)
//             {
//                 _entityframework.Add(entityToAdd);
//                 // return true:
//             }
//             // return false;
//         }

//         //method to remove 
//         //generic type T jimeh we can pass any type of model
//         // public bool addentity<T>( T entityToAdd)
//         public void removeentity<T>(T entityToRemove)
//         {
//             if (entityToRemove != null)
//             {
//                 _entityframework.Remove(entityToRemove);
//                 // return true:
//             }
//             // return false;
//         }

//         public IEnumerable<User> GetUsers()
//         {
//             IEnumerable<User> users = _entityframework.Users.ToList<User>();
//             return users;
//         }


//         //endpoint to get single user by id
//         // public IEnumerable<User> GetUsers()
//         public User GetSingleUser(int userId)
//         {

//             User? user = _entityframework.Users
//             .Where(u => u.UserId == userId) //parameter of userid value asking to the user
//             .FirstOrDefault<User>(); //will take the first order value only
//             if (user != null)
//             {

//                 return user;
//             }

//             throw new Exception("User not found");
//         }
//         public UserSalary GetUserSalary(int userId)
//         {

//             UserSalary? usersSalary = _entityframework.UserSalary
//             .Where(u => u.UserId == userId) //parameter of userid value asking to the user
//             .FirstOrDefault<UserSalary>(); //will take the first order value only
//             if (usersSalary != null)
//             {

//                 return usersSalary;
//             }

//             throw new Exception("User not found");
//         }


//     }




// }