using BLL;
using Crud.DotNet.Controllers;
using MODEL;
using NUnit.Framework;
using System.Transactions;

namespace Crud.DotNet.Tests
{
    public class Tests
    {
        [TestFixture]
        public class UserTest
        {
            [TestCase(-1)]
            [TestCase(0)]
            public void ShoudlNotGetById(int id)
            {
                var result = UserBLO.GetById(id);
                Assert.AreEqual(null, result);
            }


            [TestCase("", "04957728950", "41999999999", "email@test.com", "testpassword")]
            [TestCase("Lourenço", "123", "41999999999", "email@test.com", "testpassword")]
            [TestCase("Lourenço", "04957728950", "5984628732668", "email@test.com", "testpassword")]
            [TestCase("Lourenço", "04957728950", "41999999999", null, "testpassword")]
            [TestCase("Lourenço", "04957728950", "41999999999", "email@test.com", null)]
            public void ShouldNotInsertUser(string name, string CPF, string phone, string email, string password)
            {
                var model = new UserModel
                {
                    Name = name,
                    CPF = CPF,
                    Phone = phone,
                    Email = email,
                    Password = password
                };

                var result = UserBLO.Insert(model);
                Assert.AreEqual((int)ResultCode.Fail, result.Code);
            }

            [TestCase(-1, "Lourenço", "04957728950", "41999999999", "email@test.com", "testpassword")]
            [TestCase(0, "", "04957728950", "41999999999", "email@test.com", "testpassword")]
            [TestCase(1, "Lourenço", "123", "41999999999", "email@test.com", "testpassword")]
            [TestCase(2, "Lourenço", "04957728950", "59846287326", "email@test.com", "testpassword")]
            [TestCase(3, "Lourenço", "04957728950", "41999999999", null, "testpassword")]
            [TestCase(4, "Lourenço", "04957728950", "41999999999", "email@test.com", null)]
            public void ShouldNotUpdateUser(int id, string name, string CPF, string phone, string email, string password)
            {
                var model = new UserModel
                {
                    Id = id,
                    Name = name,
                    CPF = CPF,
                    Phone = phone,
                    Email = email,
                    Password = password
                };

                var result = UserBLO.Update(model);
                Assert.AreEqual((int)ResultCode.Fail, result.Code);
            }

            [TestCase(2, null)]
            [TestCase(1, "")]
            [TestCase(0, "password")]
            [TestCase(-1, "password")]
            [TestCase(-2, null)]
            public void ShouldNotUpdatePassword(int id, string password)
            {
                var result = UserBLO.UpdatePassword(id, password);
                Assert.AreEqual((int)ResultCode.Fail, result.Code);
            }

            [TestCase(0)]
            [TestCase(-1)]
            public void ShoudlNotDelete(int id)
            {
                var result = UserBLO.Delete(id);
                Assert.AreEqual((int)ResultCode.Fail, result.Code);
            }

            [TestCase("", "", "", "", "")]
            public void ShouldNotValidateModel(string name, string CPF, string phone, string email, string password)
            {
                var model = new UserModel
                {
                    Name = name,
                    CPF = CPF,
                    Phone = phone,
                    Email = email,
                    Password = password
                };

                var result = UserBLO.ValidateModel(model);
                Assert.AreEqual(false, result);
            }

            [TestCase("")]
            [TestCase(null)]
            [TestCase("123")]
            [TestCase("1234567891011")]
            public void ShouldNotValidatePhone(string phone)
            {
                var result = UserBLO.ValidatePhone(phone);
                Assert.AreEqual(false, result);
            }

            [TestCase("")]
            [TestCase(null)]
            [TestCase("123")]
            [TestCase("1234567891011")]
            [TestCase("04957728951")]
            public void ShouldNotValidateCPF(string cpf)
            {
                var result = UserBLO.ValidateCPF(cpf);
                Assert.AreEqual(false, result);
            }

            [TestCase(-1)]
            [TestCase(0)]
            public void ShouldNotValidateId(int id)
            {
                var result = UserBLO.ValidateId(id);
                Assert.AreEqual(false, result);
            }

            [TestCase("")]
            [TestCase(null)]
            public void ShouldNotValidatePassword(string password)
            {
                var result = UserBLO.ValidatePassword(password);
                Assert.AreEqual(false, result);
            }
        }
    }
}