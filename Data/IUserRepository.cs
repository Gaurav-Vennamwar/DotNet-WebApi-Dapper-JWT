// //basically an interface to connect our user repositoy to controller in a secure way for ef core
// using DotnetAPI.Models;

// namespace DotnetAPI.Data
// {

//     public interface IUserRepository
//     {
//         //then the methods created in repository will be called here
//         public bool SaveChanges();
//         public void addentity<T>(T entityToAdd);
//         public void removeentity<T>(T entityToRemove);
//         public IEnumerable<User> GetUsers();
//         public User GetSingleUser(int userId);
//         public UserSalary GetUserSalary(int userId);

//     }
// }