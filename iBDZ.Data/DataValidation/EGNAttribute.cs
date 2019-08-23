using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace iBDZ.Data.DataValidation
{
	public class EGNAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{
			if (value == null) return false;
			
			if (typeof(string).IsAssignableFrom(value.GetType())) return false;

			string v = (string)value;
			if (v.Length != 10)
				return false;

			if (!v.All(x => x >= '0' && x <= '9'))
				return false;

			if (!HasValidDate(v))
				return false;

			if (!HasValidChecksum(v))
				return false;

			return true;
		}

		bool HasValidDate(string v)
		{
			int date = int.Parse(v.Substring(4, 2));
			int month = int.Parse(v.Substring(2, 2));
			int year = int.Parse(v.Substring(0, 2));

			if (date == 0 || date > 32) return false;

			if (month >= 1 && month <= 12)
			{
				year += 1900;
			}
			else if (month >= 21 && month <= 32)
			{
				year += 1800;
				month -= 20;
			}
			else if (month >= 41 && month <= 52)
			{
				year += 2000;
				month -= 40;
			}
			else
			{
				return false;
			}

			if (date > DateTime.DaysInMonth(year, month))
				return false;

			return true;
		}

		bool HasValidChecksum(string v)
		{
			int[] Weights = new int[] {
				2, 4, 8, 5, 10, 9, 7, 3, 6
			};

			int expectedValue = v.Last() - '0';

			int actualValue = 0;
			for (int i = 0; i < 9; i++)
			{
				actualValue += (v[i] - '0') * Weights[i];
			}
			actualValue %= 11;
			actualValue = (actualValue == 10 ? 0 : actualValue);

			return actualValue == expectedValue;
		}
	}
}