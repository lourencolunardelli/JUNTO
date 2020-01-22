using HELPER;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public static class UserDAO 
    {
        public static SqlConnection connection;
        public static SqlCommand command;


        public static List<UserModel> Get()
        {
            try
            {
                var list = new List<UserModel>();

                var query = Queries.Get;

                OpenConnection();
                command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = LoadModel(reader);
                        list.Add(model);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static UserModel GetById(int id)
        {
            try
            {
                var model = new UserModel();

                var query = string.Format(Queries.GetById, id);

                OpenConnection();
                command = new SqlCommand(query, connection);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = LoadModel(reader);
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static ResultModel Insert(UserModel model)
        {
            try
            {
                var query = string.Format(Queries.Insert, model.Name, model.CPF, model.Phone, model.Email, Helper.Encrypt(model.Password));

                OpenConnection();
                command = new SqlCommand(query, connection);

                try
                {
                    var rowsAffected = command.ExecuteNonQuery();

                    var result = new ResultModel();

                    if (rowsAffected > 0)
                    {
                        result.Code = (int)ResultCode.Success;
                        result.Message = "User inserted";
                    }
                    else
                    {
                        result.Code = (int)ResultCode.Fail;
                        result.Message = "User was not inserted";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return GetExceptionResult(ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static ResultModel Update(UserModel model)
        {
            try
            {
                var query = string.Format(Queries.Update, model.Name, model.CPF, model.Phone, model.Email, model.Id);

                OpenConnection();
                command = new SqlCommand(query, connection);

                try
                {
                    var rowsAffected = command.ExecuteNonQuery();

                    var result = new ResultModel();

                    if (rowsAffected > 0)
                    {
                        result.Code = (int)ResultCode.Success;
                        result.Message = "User updated";
                    }
                    else
                    {
                        result.Code = (int)ResultCode.Fail;
                        result.Message = "User was not updated";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return GetExceptionResult(ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static ResultModel UpdatePassword(int id, string password)
        {
            try
            {
                var query = string.Format(Queries.UpdatePassword, Helper.Encrypt(password), id);

                OpenConnection();
                command = new SqlCommand(query, connection);

                try
                {
                    var rowsAffected = command.ExecuteNonQuery();

                    var result = new ResultModel();

                    if (rowsAffected > 0)
                    {
                        result.Code = (int)ResultCode.Success;
                        result.Message = "Password updated";
                    }
                    else
                    {
                        result.Code = (int)ResultCode.Fail;
                        result.Message = "Password was not updated";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return GetExceptionResult(ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public static ResultModel Delete(int id)
        {
            var query = string.Format(Queries.Delete, id);

            OpenConnection();
            command = new SqlCommand(query, connection);

            try
            {
                var rowsAffected = command.ExecuteNonQuery();

                var result = new ResultModel();

                if (rowsAffected > 0)
                {
                    result.Code = (int)ResultCode.Success;
                    result.Message = "User deleted";
                }
                else
                {
                    result.Code = (int)ResultCode.Fail;
                    result.Message = "User was not deleted";
                }

                return result;
            }
            catch (Exception ex)
            {
                return GetExceptionResult(ex);
            }
        }

        private static UserModel LoadModel(SqlDataReader reader)
        {
            var model = new UserModel
            {
                Id = (int)reader["ID"],
                Name = (string)reader["NAME"],
                CPF = (string)reader["CPF"],
                Phone = (string)reader["PHONE"],
                Email = (string)reader["EMAIL"],
                Password = Helper.Decrypt((string)reader["PASSWORD"])
            };

            return model;
        }

        private static void OpenConnection()
        {
            try
            {
                connection = new SqlConnection(Resources.ConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CloseConnection()
        {
            try
            {
                connection.Close();
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ResultModel GetExceptionResult(Exception ex)
        {
            var result = new ResultModel
            {
                Code = 99,
                Message = ex.InnerException.Message
            };

            return result;
        }
    }
}
