﻿using System;

namespace GameStoreApi.Data.Games.Dtos
{
	public class CartDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Picture { get; set; }
		public decimal Price { get; set; }
		public DateTime? CreationDate { get; set; }
	}
}
