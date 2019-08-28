using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data
{
	public enum LocomotiveType
	{
		Diesel = 1,
		Electric = 2,
		Hybrid = 3,
	}

	public class LocomotiveData
    {
		// Id (Int32)
		public int Id { get; set; }

		// Name (string, max length 30)
		[StringLength(30)]
		public string Name { get; set; }

		public LocomotiveType Type { get; set; }
	}
}
