using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteT2S.WebApi.Features.Containers.Data;
using TesteT2S.WebApi.Features.Containers.Models;
using TesteT2S.WebApi.Features.Containers.ViewModels;

namespace TesteT2S.WebApi.Features.Containers
{
    [ApiController]
    [Route("api/v1/containers")]
    public class ContainerController : ControllerBase
    {
        private readonly ContainerContext _containerContext;
        private readonly IMapper _mapper;

        public ContainerController(ContainerContext containerContext, IMapper mapper)
        {
            _containerContext = containerContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo container
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /containers
        ///     {
        ///         "numero": "1234abcdefg,
        ///         "cliente": "Fernando",
        ///         "tipo": 0,
        ///         "status": 0,
        ///         "categoria": 0
        ///     }
        /// </remarks>
        /// <returns> O container criado </returns>
        /// <response code="201"> Retorna o container criado </response>
        /// <response code="400"> Retorna os erros de validação </response>
        /// <response code="409"> O número do container já está em uso </response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ContainerViewModel>> Create(CreateContainerViewModel model)
        {
            Container container = _mapper.Map<Container>(model);
            Container databaseContainer = await _containerContext.Containers
                .AsNoTracking()
                .FirstOrDefaultAsync(_container => _container.Number == container.Number);
            if (databaseContainer is not null)
            {
                return Conflict();
            }
            _ = await _containerContext.Containers.AddAsync(container);
            _ = await _containerContext.SaveChangesAsync();
            ContainerViewModel viewModel = _mapper.Map<ContainerViewModel>(container);
            return CreatedAtAction(nameof(GetById), new { id = container.Id }, viewModel);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ContainerViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{number}")]
        public ActionResult<ContainerViewModel> GetByNumber(string number)
        {
            throw new NotImplementedException();
        }
    }
}
