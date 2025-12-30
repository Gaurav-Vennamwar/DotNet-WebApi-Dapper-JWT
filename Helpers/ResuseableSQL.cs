using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Models;

namespace DotnetAPI.Helpers
{
    public class ResuseableSQL
    {
        private readonly DataContextDapper _dapper;
        public ResuseableSQL(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        public bool UpsertUser(UserComplete user)
        {

            string sql = @"EXEC TutorialAppSchema.spUser_Upsert
                    @FirstName = @FirstNameParameter,
                    @LastName = @LastNameParameter,
                    @Email = @EmailParameter,
                    @Gender = @GenderParameter,
                    @JobTitle = @JobTitleParameter,
                    @Department = @DepartmentParameter,
                    @Salary = @SalaryParameter,
                    @Active = @ActiveParameter,
                    @UserId = @UserIdParameter";

            DynamicParameters sqlParametrs = new DynamicParameters();

            sqlParametrs.Add("@UserIdParameter", user.UserId, DbType.Int32);
            sqlParametrs.Add("@FirstNameParameter", user.FirstName, DbType.String);
            sqlParametrs.Add("@LastNameParameter", user.LastName, DbType.String);
            sqlParametrs.Add("@EmailParameter", user.Email, DbType.String);
            sqlParametrs.Add("@GenderParameter", user.Gender, DbType.String);
            sqlParametrs.Add("@JobTitleParameter", user.JobTitle, DbType.String);
            sqlParametrs.Add("@DepartmentParameter", user.Department, DbType.String);
            sqlParametrs.Add("@SalaryParameter", user.Salary, DbType.Decimal);
            sqlParametrs.Add("@ActiveParameter", user.Active, DbType.Boolean);



            return _dapper.ExecuteSqlWithParameters(sql, sqlParametrs);


        }
    }



}