using DAL;
using MODEL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public static class UserBLO
    {
        public static string ValidationMessage;


        public static List<UserModel> Get()
        {
            try
            {
                return UserDAO.Get();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserModel GetById(int id)
        {
            try
            {
                if (ValidateId(id))
                    return UserDAO.GetById(id);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResultModel Insert(UserModel model)
        {
            try
            {
                var result = new ResultModel();

                if (ValidateModel(model))
                    result = UserDAO.Insert(model);
                else
                {
                    result.Code = (int)ResultCode.Fail;
                    result.Message = ValidationMessage;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResultModel Update(UserModel model)
        {
            try
            {
                var result = new ResultModel();

                if (ValidateModel(model))
                    return UserDAO.Update(model);
                else
                {
                    result.Code = (int)ResultCode.Fail;
                    result.Message = ValidationMessage;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResultModel UpdatePassword(int id, string password)
        {
            try
            {
                var result = new ResultModel();

                if (ValidateId(id) && ValidatePassword(password))
                    return UserDAO.UpdatePassword(id, password);
                else
                {
                    result.Code = (int)ResultCode.Fail;
                    result.Message = ValidationMessage;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResultModel Delete(int id)
        {
            try
            {
                var result = new ResultModel();

                if (ValidateId(id))
                    return UserDAO.Delete(id);
                else
                {
                    result.Code = (int)ResultCode.Fail;
                    result.Message = ValidationMessage;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidateModel(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ValidationMessage = "Invalid name";
                return false;
            }

            else if (string.IsNullOrEmpty(model.Phone) || !ValidatePhone(model.Phone)) //Considerando um telefone com DDD e sem máscara. EX: 41998292419
            {
                ValidationMessage = "Invalid phone";
                return false;
            }

            else if (string.IsNullOrEmpty(model.CPF) || !ValidateCPF(model.CPF))
            {
                ValidationMessage = "Invalid CPF";
                return false;
            }

            else if (string.IsNullOrEmpty(model.Email))
            {
                ValidationMessage = "Invalid email";
                return false;
            }

            else if (string.IsNullOrEmpty(model.Password))
            {
                ValidationMessage = "Invalid password";
                return false;
            }


            return true;
        }

        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            else
            {
                phone = phone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();

                if (phone.Length != 11)
                    return false;
                else
                    return true;
            }
        }

        public static bool ValidateCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;
            else
            {
                var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                cpf = cpf.Replace(".", "").Replace("-", "").Trim();
                if (cpf.Length != 11)
                    return false;

                var tempCpf = cpf.Substring(0, 9);
                var soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                var resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                var digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito += resto.ToString();
                return cpf.EndsWith(digito);
            }
        }

        public static bool ValidateId(int id)
        {
            if (id > 0)
                return true;
            else
            {
                ValidationMessage = "Invalid Id";
                return false;
            }
        }

        public static bool ValidatePassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
                return true;
            else
            {
                ValidationMessage = "Invalid Password";
                return false;
            }
        }
    }
}
