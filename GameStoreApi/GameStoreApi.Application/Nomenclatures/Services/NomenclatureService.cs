using GameStoreApi.Application.Nomenclatures.Interfaces;
using GameStoreApi.Data.Nomenclatures;
using GameStoreApi.Data.Nomenclatures.Dtos;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Nomenclatures.Services
{
	public class NomenclatureService<T> : INomenclatureService<T> where T : Nomenclature
	{
		private readonly AppDbContext context;
        public NomenclatureService(AppDbContext context)
        {
			this.context = context;
        }

        public async Task<T> CreateNomenclature(T entity)
		{
			var maxViewOrder = await context.Set<T>().MaxAsync(e => e.ViewOrder);

			entity.ViewOrder = maxViewOrder.HasValue ? maxViewOrder.Value + 1 : 1;

			await context.AddAsync(entity);
			await context.SaveChangesAsync();

			return entity;
		}

		public async Task DeactivateNomenclature(int id)
		{
			var entity = await context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
			entity.IsActive = false;

			await context.SaveChangesAsync();
		}

		public async Task<List<T>> GetAllNomenclatures() => await context.Set<T>().ToListAsync();

		public async Task<T> GetNomenclature(int id) => await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);

		public async Task<List<NomenclatureDto>> GetNomenclatureDtos()
		{
			var nomenclatures = new List<NomenclatureDto>();
			foreach (var nomenclature in context.Set<T>().OrderByDescending(e => e.ViewOrder))
			{
				if (nomenclature.IsActive)
				{
					var dto = new NomenclatureDto()
					{
						Id = nomenclature.Id,
						Name = nomenclature.Name,
						ViewOrder = (int)nomenclature.ViewOrder
					};
					nomenclatures.Add(dto);
				}
			}
			return nomenclatures;
		}

		public async Task<T> UpdateNomenclature(T entity)
		{
			context.Update(entity);
			await context.SaveChangesAsync();

			return entity;
		}
	}
}
