using GameStoreApi.Data.Common.Interfaces;

namespace GameStoreApi.Data.Nomenclatures
{
	public class Nomenclature : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ViewOrder { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
