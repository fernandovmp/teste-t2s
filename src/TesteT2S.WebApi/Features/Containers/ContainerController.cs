using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        [Produces("application/json")]
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

        /// <summary>
        /// Busca um container pelo Id
        /// </summary>
        /// <returns> O container solicitado </returns>
        /// <response code="200"> Retorna o container solicitado </response>
        /// <response code="404"> O container solicitado não existe </response>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContainerViewModel>> GetById(int id)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .FirstOrDefaultAsync(_container => _container.Id == id);
            if (container is null)
            {
                return NotFound();
            }
            return _mapper.Map<ContainerViewModel>(container);
        }

        /// <summary>
        /// Busca um container pelo número
        /// </summary>
        /// <returns> O container solicitado </returns>
        /// <response code="200"> Retorna o container solicitado </response>
        /// <response code="404"> O container solicitado não existe </response>
        [HttpGet("{number}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContainerViewModel>> GetByNumber(string number)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .FirstOrDefaultAsync(_container => _container.Number == number);
            if (container is null)
            {
                return NotFound();
            }
            return _mapper.Map<ContainerViewModel>(container);
        }

        /// <summary>
        /// Lista os containers de forma paginada
        /// </summary>
        /// <returns> As informações de paginação e os dados containers </returns>
        /// <response code="200"> Retorna as informações de paginação e os dados containers </response>
        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedViewModel<ContainerViewModel>>> GetWithPagination(
            [FromQuery(Name = "pagina")] int page = 1,
            [FromQuery(Name = "tamanho")] int size = 10,
            [FromQuery(Name = "ordenar_por")] string sortBy = "numero"
        )
        {
            IQueryable<Container> query = _containerContext.Containers
                .AsNoTracking();

            string[] sortValues = sortBy.ToLower().Split('_');
            query = ResolveSortingParam(query, sortValues);

            IEnumerable<ContainerViewModel> containers = await query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(container => _mapper.Map<ContainerViewModel>(container))
                .ToListAsync();
            int containersCount = await _containerContext.Containers.CountAsync();
            return new PaginatedViewModel<ContainerViewModel>(page, size, containersCount, containers);
        }

        private static IQueryable<Container> ResolveSortingParam(IQueryable<Container> query, string[] sortValues)
        {
            if (sortValues.Length >= 2)
            {
                return ResolveSortingParam(query, sortKey: sortValues[0], sortPattern: sortValues[1]);
            }
            return query.OrderBy(container => container.Number);

        }

        private static IQueryable<Container> ResolveSortingParam(IQueryable<Container> query,
            string sortKey,
            string sortPattern)
        {
            if (sortPattern == "asc")
            {
                query = query
                    .OrderBy(SelectSortKey(sortKey))
                    .ThenBy(container => container.Number);
                return query;
            }
            return query
                .OrderByDescending(SelectSortKey(sortKey))
                .ThenBy(container => container.Number);
        }

        private static Expression<Func<Container, object>> SelectSortKey(string key)
        {
            return key switch
            {
                "numero" => (Container container) => container.Number,
                "tipo" => (Container container) => container.Type,
                "status" => (Container container) => container.Status,
                "categoria" => (Container container) => container.Category,
                "cliente" => (Container container) => container.Customer,
                _ => (Container container) => container.Id
            };
        }
    }
}
