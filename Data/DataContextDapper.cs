using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;

        // abh ek constructor bna rhe hai jisme config pass kr rhe hai
        public DataContextDapper(IConfiguration config)
        {
            _config = config; //pahile vala config banaya usko private variable me assign kr diya
        }
        //to load multiple lines of data
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }
        //to load single line of data 
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }
        //TO RETURN THE ROW COUNT AFFECTED
        public bool ExecuteSql<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0; //if greater than 0 then true else false
        }
        //to return the exact row count affected
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql); //returning the exact row count affected}
        }

        //to execute sql with parameters and return boolean if rows affected greater than 0
        public bool ExecuteSqlWithParameters(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters) > 0; //if greater than 0 then true else false
        
        }
         //to load multiple lines of data
        public IEnumerable<T> LoadDataWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql, parameters);
        }
        //to load single line of data 
        public T LoadDataSingleWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql, parameters);
        }

        internal bool ExecuteSqlWithParameters<T>(string sql, DynamicParameters sqlParametrs)
        {
            throw new NotImplementedException();
        }
    }
}   