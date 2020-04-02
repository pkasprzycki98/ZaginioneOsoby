using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZaginioneOsoby.Models
{
	public class OsobaZaginionaModel
	{
		[Key]
		public int OsobaZaginionaId { get; set; }
		public string UserId { get; set; }
		[Required(ErrorMessage ="Dej byku name")]
		public string Name { get; set; }
		[Required]
		public string Surrname { get; set; }
		[Required]
		public DateTime MissingDate { get; set; }
		[Required]
		public int Age { get; set; }
		public PlecList Sex { get; set; }
		[Required]
		public string HairColor { get; set; }
		[Required]
		public string Height { get; set; }
		[Required]
		public string Descprition { get; set; }
		public string City { get; set; }
		public ProvicesList Provices { get; set; }
		public string Street { get; set; }

		public string FileName { get; set; }
		[Required]
		[NotMapped]
		public IFormFile PhotoUrl { get; set; }

		public enum ProvicesList {Brak,Dolnośląskie, KujawskoPomorskie, Lubelskie, Lubuskie, Łódzkie, Małopolskie, Mazowieckie, Opolskie, Podkarpackie, Podlaskie, Pomorskie, Śląskie, Świętkorzyskie, WarmińskoMazurskie, Wielkopolskie, Zachodniopomorskie }
		public enum PlecList { Brak, M, K };

		[ForeignKey("UserId")]
		public virtual UserModel User { get; set; }
	}
}
