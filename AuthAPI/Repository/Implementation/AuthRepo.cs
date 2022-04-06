using AuthAPI.Model;
using AuthAPI.Repository.Interface;
using AuthAPI.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AuthAPI.Repository.Implementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ILogger<AuthRepo> _logger;
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public AuthRepo(ILogger<AuthRepo> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            connectionString = _config.GetConnectionString("DBConnection");
        }
        public string GetUserDetail(UserLoginModel login)
        {
            const string methodName = "GetProductsRepo";
            _logger.LogDebug($"Method Entry : {methodName}");
            string result = string.Empty;
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = cnn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Constants.SP_LOGIN
                };
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@pLoginName", login.LoginID),
                    new SqlParameter("@pPassword", login.Password),
                    new SqlParameter{
                    ParameterName = "@responseMessage",
                    DbType = DbType.String,
                    Size = 30,
                    Direction = ParameterDirection.Output
                }
                };
                cmd.Parameters.AddRange(sqlParameters);
                cnn.Open();
                cmd.ExecuteScalar();
                result = Convert.ToString(sqlParameters[2].Value);
                cnn.Close();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public UserModel getUserRoleByID(UserLoginModel login)
        {
            const string methodName = "GetProductsRepo";
            _logger.LogDebug($"Method Entry : {methodName}");
            //string conn
            UserModel userDetail = new UserModel();
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                string query = string.Format("select user_name,role_id from [User] where login_id  = '{0}' ", login.LoginID);
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    userDetail.Username = sqlDataReader.GetString(0);
                    userDetail.UserRole = sqlDataReader.GetInt32(1);
                    userDetail.LoginID = login.LoginID;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return userDetail;
        }
    }
}
