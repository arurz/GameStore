using GameStoreApi.Application.Nomenclatures.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Data.Users;

namespace GameStoreApi.Hosting.Controllers.Nomenclatures
{
	public class GenresController : BaseNomenclatureController<Genre>
	{
		public GenresController(INomenclatureService<Genre> nomenclatureService) : base(nomenclatureService)
		{
		}
	}

	public class RolesController : BaseNomenclatureController<Role>
	{
		public RolesController(INomenclatureService<Role> nomenclatureService) : base(nomenclatureService)
		{
		}
	}

	public class CompaniesController : BaseNomenclatureController<Company>
	{
		public CompaniesController(INomenclatureService<Company> nomenclatureService) : base(nomenclatureService)
		{
		}
	}
}
