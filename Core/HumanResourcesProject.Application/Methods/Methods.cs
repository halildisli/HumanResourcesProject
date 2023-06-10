using HumanResourcesProject.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Methods
{
	public static class Methods
	{
		public static string GenerateEmail(Employee person)
		{
			if (person.Email == null)
			{
				if (string.IsNullOrWhiteSpace(person.SecondName) & string.IsNullOrWhiteSpace(person.SecondSurname))
				{
					person.Email = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}@bilgeadamboost.com";
				}
				else if (string.IsNullOrWhiteSpace(person.SecondSurname) & !string.IsNullOrWhiteSpace(person.SecondName))
				{
					person.Email = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}@bilgeadamboost.com";
				}
				else if (!string.IsNullOrWhiteSpace(person.SecondSurname) & string.IsNullOrWhiteSpace(person.SecondName))
				{
					person.Email = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}@bilgeadamboost.com";
				}
				else
				{
					person.Email = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}@bilgeadamboost.com";
				}
				return person.Email;
			}
			return person.Email;

		}
        public static string GenerateEmailManager(Person person)
        {
            if (person.Email == null)
            {
                if (string.IsNullOrWhiteSpace(person.SecondName) & string.IsNullOrWhiteSpace(person.SecondSurname))
                {
                    person.Email = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}@bilgeadam.com";
                }
                else if (string.IsNullOrWhiteSpace(person.SecondSurname) & !string.IsNullOrWhiteSpace(person.SecondName))
                {
                    person.Email = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}@bilgeadam.com";
                }
                else if (!string.IsNullOrWhiteSpace(person.SecondSurname) & string.IsNullOrWhiteSpace(person.SecondName))
                {
                    person.Email = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}@bilgeadam.com";
                }
                else
                {
                    person.Email = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}@bilgeadam.com";
                }
                return person.Email;
            }
            return person.Email;

        }
        public static string GenerateTempPassword(Employee person)
		{
			var password = "";

			if (string.IsNullOrWhiteSpace(person.SecondName) & string.IsNullOrWhiteSpace(person.SecondSurname))
			{
				password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.Surname + "123456789*";
			}
			else if (string.IsNullOrWhiteSpace(person.SecondSurname) & !string.IsNullOrWhiteSpace(person.SecondName))
			{
				password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.Surname+person.SecondSurname + "123456789*";
			}
			else if (!string.IsNullOrWhiteSpace(person.SecondSurname) & string.IsNullOrWhiteSpace(person.SecondName))
			{
				password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.SecondName+person.Surname + "123456789*";
			}
			else
			{
				password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) +person.SecondName+ person.Surname + person.SecondSurname+"123456789*";
			}
			return password;

		}
        //KontrolEdilecek
        public static string GenerateTempPasswordManager(Person person)
        {
            var password = "";

            if (string.IsNullOrWhiteSpace(person.SecondName) & string.IsNullOrWhiteSpace(person.SecondSurname))
            {
                password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.Surname.ToLower() + "987654321*";
            }
            else if (string.IsNullOrWhiteSpace(person.SecondSurname) & !string.IsNullOrWhiteSpace(person.SecondName))
            {
                password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.SecondName.ToLower() + person.Surname.ToLower() + "987654321*";
            }
            else if (!string.IsNullOrWhiteSpace(person.SecondSurname) & string.IsNullOrWhiteSpace(person.SecondName))
            {
                password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.Surname.ToLower()+person.SecondSurname.ToLower() + "987654321*";
            }
            else
            {
                password = person.FirstName.ToUpper()[0] + person.FirstName.ToLower().Substring(1, person.FirstName.Length - 1) + person.SecondName.ToLower() + person.Surname.ToLower() + person.SecondSurname.ToLower() + "987654321*";
            }
            return password;

        }
        public static string GenerateUsername(Person person)
        {
            if (person.UserName == null)
            {
                if (string.IsNullOrWhiteSpace(person.SecondName) & string.IsNullOrWhiteSpace(person.SecondSurname))
                {
					person.UserName = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}";
                }
                else if (string.IsNullOrWhiteSpace(person.SecondSurname) & !string.IsNullOrWhiteSpace(person.SecondName))
                {
                    person.UserName = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}";
                }
                else if (!string.IsNullOrWhiteSpace(person.SecondSurname) & string.IsNullOrWhiteSpace(person.SecondName))
                {
                    person.UserName = $"{person.FirstName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}";
                }
                else
                {
                    person.UserName = $"{person.FirstName.ToLower()}{person.SecondName.ToLower()}.{person.Surname.ToLower()}{person.SecondSurname.ToLower()}";
                }
                return person.UserName;
            }
            return person.UserName;

        }
    }
}
