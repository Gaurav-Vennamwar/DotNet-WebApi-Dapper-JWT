using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostCompleteController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public PostCompleteController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        //endpoint to get all posts
        [HttpGet("Post/{postId}/{userId}/{searchTerm}")]
        public IEnumerable<Post> GetPosts(int postId = 0 , int userId = 0 , string searchTerm = "None")
        {

            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string stringparameters = "";

            DynamicParameters sqlParametrs = new DynamicParameters();


            if (postId != 0)
            {
                 stringparameters += ", @PostId = @PostIdParameter"; 
                 sqlParametrs.Add("@PostIdParameter", postId, DbType.Int32);
            }
            if (userId != 0)
            {
                stringparameters += ", @UserId = @UserIdParameter";
                sqlParametrs.Add("@UserIdParameter", userId, DbType.Int32);

            }
            if (searchTerm.ToLower() != "none")
            {
                stringparameters += ", @SearchValue = @SearchValueParameter";
                sqlParametrs.Add("@SearchValueParameter", searchTerm, DbType.String);
            }
            if (stringparameters.Length > 0)
            {
                sql += stringparameters.Substring(1);//, parameters.Length);
            }

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParametrs);
        }

        

       
        //endpoint to get my posts 
        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {

            string sql = @" EXEC TutorialAppSchema.spPosts_Get @UserId = @UserIdParameter";
           
            DynamicParameters sqlParametrs = new DynamicParameters();
           
            sqlParametrs.Add("@UserIdParameter", (this.User.FindFirst("userId")?.Value), DbType.Int32);
        
              return _dapper.LoadDataWithParameters<Post>(sql, sqlParametrs);
        }

   
        //endpoint to add a post
        [HttpPut("UpSertPost")]
        public IActionResult AddPost(Post postToAdd)
        {
            string sql = @"TutorialAppSchema.spPosts_Upsert
            @UserId = @UserIdParameter,
            @PostTitle = @PostTitleParameter,
            @PostContent = @PostContentParameter";

            DynamicParameters sqlParametrs = new DynamicParameters();
            sqlParametrs.Add("@UserIdParameter", (this.User.FindFirst("userId")?.Value), DbType.Int32);
            sqlParametrs.Add("@PostTitleParameter", postToAdd.PostTitle, DbType.String);
            sqlParametrs.Add("@PostContentParameter", postToAdd.PostContent, DbType.String);

            if(postToAdd.PostId > 0)
            {
                sql += ", @PostId = @PostIdParameter";
                sqlParametrs.Add("@PostIdParameter", postToAdd.PostId, DbType.Int32);
            }

            if (_dapper.ExecuteSqlWithParameters<string>(sql, sqlParametrs))
            {
                return Ok();
            }
            throw new Exception("Failed to CREATE NEW post.");
        }

      
//now an endpoint to delete a post
        [HttpDelete("ToDeletePost/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Delte 
            @PostId = @PostIdParameter, 
            @UserId = @UserIdParameter";

            DynamicParameters sqlParametrs = new DynamicParameters();
            sqlParametrs.Add("@PostIdParameter", postId, DbType.Int32);
            sqlParametrs.Add("@UserIdParameter", (this.User.FindFirst("userId")?.Value), DbType.Int32);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParametrs))
            {
                return Ok();
            }
            throw new Exception("Failed to DELETE post.");
        }

}
}



