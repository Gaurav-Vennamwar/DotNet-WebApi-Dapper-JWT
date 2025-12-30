// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Localization;

// namespace DotnetAPI.Controllers
// {

//     [Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class PostController : ControllerBase
//     {
//         private readonly DataContextDapper _dapper;

//         public PostController(IConfiguration config)
//         {
//             _dapper = new DataContextDapper(config);
//         }

//         //endpoint to get all posts
//         [HttpGet("Post")]
//         public IEnumerable<Post> GetPosts()
//         {

//             string sql = @" SELECT  [PostId],
//                             [UserId],
//                             [PostTitle],
//                             [PostContent],
//                             [PostCreated],
//                             [PostUpdated] 
//                         FROM TutorialAppSchema.Posts";
//             return _dapper.LoadData<Post>(sql);
//         }

//         //endpoint to get a single post by post id
//         [HttpGet("PostSingle/{postId}")]
//         public Post GetSinglePost(int postId)
//         {

//             string sql = @" SELECT  [PostId],
//                             [UserId],
//                             [PostTitle],
//                             [PostContent],
//                             [PostCreated],
//                             [PostUpdated] 
//                         FROM TutorialAppSchema.Posts
//                         WHERE PostId = " + postId.ToString();
//             return _dapper.LoadDataSingle<Post>(sql);
//         }

//         //endpoint to get all posts related by a single user by user id
//         [HttpGet("PostByUser/{userId}")]
//         public IEnumerable<Post> GetPostByUser(int userId)
//         {

//             string sql = @" SELECT  [PostId],
//                             [UserId],
//                             [PostTitle],
//                             [PostContent],
//                             [PostCreated],
//                             [PostUpdated] 
//                         FROM TutorialAppSchema.Posts
//                         WHERE UserId = " + userId.ToString();
//             return _dapper.LoadData<Post>(sql);
//         }

//         //endpoint to get my posts 
//         [HttpGet("MyPosts")]
//         public IEnumerable<Post> GetMyPosts()
//         {

//             string sql = @" SELECT  [PostId],
//                             [UserId],
//                             [PostTitle],
//                             [PostContent],
//                             [PostCreated],
//                             [PostUpdated] 
//                         FROM TutorialAppSchema.Posts
//                         WHERE UserId = " + this.User.FindFirst("userId")?.Value + "";
//             return _dapper.LoadData<Post>(sql);
//         }

//         //endpoint to add a post
//         [HttpPost("ToAddPost")]
//         public IActionResult AddPost(PostToAddDtos postToAdd)
//         {
//             string sql = @"INSERT INTO TutorialAppSchema.Posts(
                                            
//                                             [UserId],
//                                             [PostTitle],
//                                             [PostContent],
//                                             [PostCreated],
//                                             [PostUpdated])VALUES(" + this.User.FindFirst("userId")?.Value //find first basically do is it will search for the claim with the name userId and return its value
//                                             + ", '" + postToAdd.PostTitle
//                                              + "', '" + postToAdd.PostContent
//                                             + "', GETDATE(), GETDATE() )";
//             if (_dapper.ExecuteSql<string>(sql))
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to CREATE NEW post.");
//         }

//         //[AllowAnonymous]
//         //endpoint to edit a post
//         [HttpPut("ToEditPost")]
//         public IActionResult EditPost(PostToEditDto postToEdit)
//         {
//             string sql = @"
//                   UPDATE TutorialAppSchema.Posts
//                   SET PostContent = '" + postToEdit.PostContent +
//                  "' ,PostTitle ='" + postToEdit.PostTitle +
//                  @"',PostUpdated = GETDATE()
//             WHERE PostId = " + postToEdit.PostId.ToString() + //TO CHECK WETHER THE POST BELONGS TO THE USER OR NOT SO THAT NO ONE ELSE CAN EDIT IT
//                  " AND UserId = " + this.User.FindFirst("userId")?.Value; //find first basically do is it will search for the claim with the name userId and return its value
//       ;
//             if (_dapper.ExecuteSql<string>(sql))
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to EDIT post.");
//         }

// //now an endpoint to delete a post
//         [HttpDelete("ToDeletePost/{postId}")]
//         public IActionResult DeletePost(int postId)
//         {
//             string sql = @"
//                   DELETE FROM TutorialAppSchema.Posts
//       WHERE PostId = " + postId.ToString() + //TO CHECK WETHER THE POST BELONGS TO THE USER OR NOT SO THAT NO ONE ELSE CAN DELETE IT
//                  " AND UserId = " + this.User.FindFirst("userId")?.Value; //find first basically do is it will search for the claim with the name userId and return its value
      
//             if (_dapper.ExecuteSql<string>(sql))
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to DELETE post.");
//         }

// //endpoint to search posts by title or content
//         [HttpGet("SearchPosts/{searchTerm}")]
//         public IEnumerable<Post> SearchPosts(string searchTerm)
//         {

//             string sql = @"SELECT [PostId],
//                     [UserId],
//                     [PostTitle],
//                     [PostContent],
//                     [PostCreated],
//                     [PostUpdated] 
//                 FROM TutorialAppSchema.Posts
//                     WHERE PostTitle LIKE '%" + searchTerm + "%'" +
//                         " OR PostContent LIKE '%" + searchTerm + "%'";//%searchTerm% means that the search term can be anywhere in the title or content aage bhi search ho sakta hai peeche bhi search ho sakta hai
//             return _dapper.LoadData<Post>(sql);
//         }
//     }
// }