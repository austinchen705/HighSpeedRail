using HighSpeedRail.DB;
using HighSpeedRail.Dto;
using HighSpeedRail.Enum;
using HighSpeedRail.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace HighSpeedRail.DAO
{
    public class CanibetDAO : IDisposable
    {
        private SQLiteConnection _conn =  DBConn.getConnection();

        public List<CanibetDto> GetCanibetList()
        {
            List<CanibetDto> canibetList = new List<CanibetDto>();
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT ID, FunctionType, CreateDate, UpdateDate FROM Canibet";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    canibetList.Add(new CanibetDto()
                    {
                         ID = reader["ID"].ToString().ParseIntOrDefault(),
                         FunctionType = (FunctionTypeEnum) System.Enum.Parse(typeof(FunctionTypeEnum), reader["FunctionType"].ToString()),
                         CreateDate = reader["CreateDate"].ToString().ParseDateTimeOrDefault(),
                         UPdateDate = reader["UpdateDate"].ToString().ParseDateTimeOrDefault(),
                    });
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
            return canibetList;
        }

        public CanibetDto GetCanibet(int canibetID)
        {
            CanibetDto canibet = new CanibetDto();
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT ID, FunctionType, CreateDate, UpdateDate FROM Canibet WHERE ID = @CanibetID";
                cmd.Parameters.AddWithValue("@CanibetID", canibetID);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    canibet.ID = reader["ID"].ToString().ParseIntOrDefault();
                    canibet.FunctionType = (FunctionTypeEnum)System.Enum.Parse(typeof(FunctionTypeEnum), reader["FunctionType"].ToString());
                    canibet.CreateDate = reader["CreateDate"].ToString().ParseDateTimeOrDefault();
                    canibet.UPdateDate = reader["UpdateDate"].ToString().ParseDateTimeOrDefault();
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
            return canibet;
        }

        public CanibetDto GetUsingCanibet()
        {
            CanibetDto canibet = null;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT ID, FunctionType, CreateDate, UpdateDate FROM Canibet WHERE IsUse=1";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    canibet = new CanibetDto();
                    canibet.ID = reader["ID"].ToString().ParseIntOrDefault();
                    canibet.FunctionType = (FunctionTypeEnum)System.Enum.Parse(typeof(FunctionTypeEnum), reader["FunctionType"].ToString());
                    canibet.CreateDate = reader["CreateDate"].ToString().ParseDateTimeOrDefault();
                    canibet.UPdateDate = reader["UpdateDate"].ToString().ParseDateTimeOrDefault();
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
            return canibet;
        }

        public int UpdateCanibet(int id, int functionType)
        {
            int result = 0;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "UPDATE Canibet SET IsUse =0";
                cmd.ExecuteNonQuery();
                

                cmd.CommandText = "UPDATE Canibet SET FunctionType = @FunctionType, UpdateDate = @UpdateDate, IsUse =1  WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@FunctionType", functionType);
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

        public int ResetCanibet()
        {
            int result = 0;
            _conn.Open();
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "UPDATE Canibet SET IsUse =0";
                cmd.ExecuteNonQuery();
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

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}