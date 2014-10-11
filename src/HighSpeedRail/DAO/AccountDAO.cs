using HighSpeedRail.DB;
using HighSpeedRail.Dto;
using HighSpeedRail.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace HighSpeedRail.DAO
{
    public class AccountDAO : IDisposable
    {
        private SQLiteConnection _conn = DBConn.getConnection();

        public int CreateUser(string userCode, string password)
        {
            int result = 0;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();

                cmd.CommandText = "INSERT INTO User(UserCode, Password, CreateDate, UpdateDate) VALUES (@UserCode, @Password, @CreateDate, @UpdateDate)";
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        public int ChangePassword(string userCode, string currentPassword, string newPassword)
        {
            int result = 0;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "UPDATE USER SET Password = @NewPassword, UpdateDate=@UpdateDate  WHERE UserCode = @UserCode AND Password = @Password";
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@Password", currentPassword);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        public int ResetPassword(string userCode, string newPassword)
        {
            int result = 0;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "UPDATE USER SET Password = @NewPassword, UpdateDate=@UpdateDate  WHERE UserCode = @UserCode";
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        public bool Login(string userCode, string password)
        {
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(1) FROM USER WHERE UserCode=@UserCode AND Password = @Password";
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                cmd.Parameters.AddWithValue("@Password", password);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString().ParseIntOrDefault() > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public UserDto FindUser(string userCode)
        {
            UserDto dto = new UserDto();
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT UserID, UserCode, Password, CreateDate, UpdateDate FROM USER WHERE UserCode=@UserCode";
                cmd.Parameters.AddWithValue("@UserCode", userCode);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dto.UserID = reader["UserID"].ToString().ParseIntOrDefault();
                    dto.UserCode = reader["UserCode"].ToString();
                    dto.Password = reader["Password"].ToString();
                    dto.CreateDate = reader["CreateDate"].ToString().ParseDateTimeOrDefault();
                    dto.UpdateDate = reader["UpdateDate"].ToString().ParseDateTimeOrDefault();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }
            return dto;
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}