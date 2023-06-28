using GameStoreApi.Application.Nomenclatures.Interfaces;
using GameStoreApi.Data.Nomenclatures;
using GameStoreApi.Data.Nomenclatures.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Nomenclatures
{
	[ApiController]
	public class BaseNomenclatureController<T> : ControllerBase
		where T : Nomenclature
	{
        private readonly INomenclatureService<T> nomenclatureService;

        public BaseNomenclatureController(INomenclatureService<T> nomenclatureService)
        {
            this.nomenclatureService = nomenclatureService; 
        }

        [HttpGet("api/[controller]")]
        public async Task<ActionResult<List<NomenclatureDto>>> GetNomenclatureDtos() => await nomenclatureService.GetNomenclatureDtos();

		[HttpGet("api/[controller]/{id}")]
		public async Task<ActionResult<T>> GetNomenclatureById([FromRoute] int id)	=> await nomenclatureService.GetNomenclature(id);

		[HttpGet("api/[controller]/all")]
		public async Task<List<T>> GetAllNomenclaturesByType() => await nomenclatureService.GetAllNomenclatures();

		//[HttpGet("api/admin/[controller]")]
		//public async Task<ActionResult<List<NomenclatureIdNameDto>>> GetNomenclatureIdNameDtos() => await nomenclatureService.GetNomenclatureIdNameDtos();

		//[HttpGet("api/admin/[controller]/dto")]
		//public async Task<ActionResult<List<NomenclatureIdDto>>> GetNomenclatureDtos()
		//{
		//	return await nomenclatureService.GetNomenclatureIdDtos();
		//}

		[HttpPost("api/admin/[controller]/create")]
		public async Task<IActionResult> CreateNomenclature([FromBody] T entity)
		{
			await nomenclatureService.CreateNomenclature(entity);
			return Ok();
		}

		[HttpDelete("api/admin/[controller]/delete/{id}")]
		public async Task<IActionResult> DeactivateNomenclature([FromRoute] int id)
		{
			await nomenclatureService.DeactivateNomenclature(id);
			return Ok();
		}

		[HttpPut("api/admin/[controller]/update")]
		public async Task<IActionResult> UpdateNomenclature([FromBody] T entity)
		{
			await nomenclatureService.UpdateNomenclature(entity);
			return Ok();
		}
	}
}
