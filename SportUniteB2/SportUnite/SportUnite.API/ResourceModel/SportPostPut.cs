using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
	public class SportPostPut
	{
		public SportPostPut() { }

		public SportPostPut(Sport s)
		{
			SportId = s.SportId;
		}

		public int SportId { get; set; }
	}
}
