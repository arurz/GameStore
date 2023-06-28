using GameStoreApi.Data.Nomenclatures;
using GameStoreApi.Data.Nomenclatures.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Nomenclatures.Interfaces
{
	public interface INomenclatureService<T> where T : Nomenclature
	{
		Task<List<NomenclatureDto>> GetNomenclatureDtos();

		//Task<List<NomenclatureIdNameDto>> GetNomenclatureIdNameDtos();

		//Task<List<NomenclatureIdDto>> GetNomenclatureIdDtos();

		Task<T> CreateNomenclature(T entity);

		Task<List<T>> GetAllNomenclatures();

		Task<T> GetNomenclature(int id);

		Task<T> UpdateNomenclature(T entity);

		Task DeactivateNomenclature(int id);
	}
}
