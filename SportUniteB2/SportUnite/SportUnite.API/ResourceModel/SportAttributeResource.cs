using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
	public class SportAttributeResource
	{
		public SportAttributeResource(SportAttribute sportAttribute)
		{
			SportAttributeId = sportAttribute.SportAttributeId;
			Type = sportAttribute.Type;
			Name = sportAttribute.Name;
			Approved = sportAttribute.Approved;
			Archived = sportAttribute.Archived;
			sportId = sportAttribute.Sport.SportId;
		}

		public int SportAttributeId { get; set; }

		[Required(ErrorMessage = "Please enter an attribute type")]
		[StringLength(50, ErrorMessage = "Type must be less than 50 characters long")]
		public string Type { get; set; }

		[Required(ErrorMessage = "Please enter a detailed name for the attribute")]
		[StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }
		public bool Approved { get; set; }
		public bool Archived { get; set; }
		public int sportId { get; set; }
	}
}
