using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
	public class SportResource
	{
		public SportResource() { }

		public SportResource(Sport s)
		{
			SportId = s.SportId;
			Type = s.Type;
			Archived = s.Archived;
		}

		public int SportId { get; set; }
		[Required]
		[StringLength(50)]
		public string Type { get; set; }
		public bool Archived { get; set; }
	}
}
