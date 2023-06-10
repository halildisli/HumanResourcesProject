using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
	public class YearlyPermissonValidator: ValidationAttribute
	{
      
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
       {
		//	ArrayList arrayList =(ArrayList)value;
		//	Permission permission = (Permission)arrayList[0];
		//	Employee employee = (Employee)arrayList[1];
		//	if (permission.PermissionType==PermissionType.AnnualLeave)
		//	{

		//		if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
		//		{
		//			double diff = (DateTime.Now - ((DateTime)employee.StartingDate)).TotalDays/365;
					
		//			if (diff < 1)
		//			{
		//				return new ValidationResult("Employees with less than 1 user cannot take annual leave.");
		//			}
		//			if (diff >= 1 && diff < 6)
		//			{
		//				if (permission.CountOfPermittedDays > TimeSpan.FromDays(14))
		//				{
		//					return new ValidationResult("Employees with 1-6 years of experience can only take up to 14 days of yearly permission.");
		//				}
		//			}
		//			else if (diff >= 6)
		//			{
		//				if (permission.CountOfPermittedDays > TimeSpan.FromDays(20))
		//				{
		//					return new ValidationResult("Employees with more than 6 years of experience can take up to 20 days of yearly permission.");				
		//				}
		//			}
		//		}
		//	}
		//	else if (permission.PermissionType==PermissionType.MaternityLeave)
		//	{
		//		if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
		//		{
		//			if (permission.CountOfPermittedDays > TimeSpan.FromDays(180))
		//			{
		//				return new ValidationResult("Employees on maternity leave can only take up to 180 days of yearly permission.");
		//			}
		//		}
		//	}
		//	else if (permission.PermissionType == PermissionType.PaternityLeave)
		//	{
		//		if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
		//		{
		//			if (permission.CountOfPermittedDays > TimeSpan.FromDays(30))
		//			{
		//				return new ValidationResult("Employees on paternity leave can only take up to 30 days of yearly permission");
		//			}
		//		}
		//	}
		//	else if (permission.PermissionType == PermissionType.MarriageLeave)
		//	{
		//		if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
		//		{
		//			if (permission.CountOfPermittedDays > TimeSpan.FromDays(15))
		//			{
		//				return new ValidationResult("Employees on marriage leave can only take up to 15 days of yearly permission");
		//			}
		//		}
		//	}
		//	else if (permission.PermissionType == PermissionType.UnpaidLeave)
		//	{
		//		if (employee.StartingDate != null && permission.StartOfPermissionDate != null)
		//		{
		//			if (permission.CountOfPermittedDays > TimeSpan.FromDays(5))
		//			{
		//				return new ValidationResult("Employees on unpaid leave can only take up to 5 days of yearly permission");
		//			}
		//		}
		//	}

			return ValidationResult.Success;
		}
	}
}

