namespace GameStoreApi.Data.Games.Dtos
{
	public class SearchDto
	{
		public string Name { get; set; }

		public string Genres { get; set; }

		public string Companies { get; set; }

		public int PriceBottomBound { get; set; }

		public int PriceTopBound { get; set; }
	}
}
