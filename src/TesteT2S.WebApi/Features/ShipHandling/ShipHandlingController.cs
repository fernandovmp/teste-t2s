using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteT2S.WebApi.Data;
using TesteT2S.WebApi.Features.Containers.Models;
using TesteT2S.WebApi.Features.ShipHandling.Models;
using TesteT2S.WebApi.Features.ShipHandling.ViewModels;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.ShipHandling
{
    [ApiController]
    [Route("api/v1/containers/{containerId:int}/movimentacao")]
    [Route("api/v1/containers/{containerNumber}/movimentacao")]
    public class ShipHandlingController : ControllerBase
    {
        private readonly ContainerContext _containerContext;
        private readonly IMapper _mapper;

        public ShipHandlingController(ContainerContext containerContext, IMapper mapper)
        {
            _containerContext = containerContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma movimentação a um container
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /containers/{numero}/movimentacao
        ///     {
        ///         "navio": "Navio N",
        ///         "tipoMovimentaco": 0,
        ///         "dataInicio": "10/10/10,
        ///         "dataFim": "11/10/10"
        ///     }
        /// </remarks>
        /// <returns> A movimentação adicionada </returns>
        /// <response code="201"> Retorna a movimentação adicionada </response>
        /// <response code="400"> Retorna os erros de validação </response>
        /// <response code="404"> O container especificado não existe </response>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HandlingViewModel>> Create(string containerNumber,
            CreateHandlingViewModel model)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Id = _container.Id,
                    Number = _container.Number
                })
                .FirstOrDefaultAsync(_container => _container.Number == containerNumber);
            if (container is null)
            {
                return NotFound();
            }
            Handling handling = _mapper.Map<Handling>(model);
            handling.ContainerId = container.Id;
            _ = await _containerContext.Handlings.AddAsync(handling);
            _ = await _containerContext.SaveChangesAsync();
            HandlingViewModel viewModel = _mapper.Map<HandlingViewModel>(handling);
            return CreatedAtAction(nameof(GetById),
                new { containerId = container.Id, handlingId = handling.Id },
                viewModel);
        }

        /// <summary>
        /// Busca uma movimentação pelo Id
        /// </summary>
        /// <returns> A movimentação solicitada </returns>
        /// <response code="200"> Retorna a movimentação solicitada </response>
        /// <response code="404"> O container ou movimentação solicitada não existe </response>
        [HttpGet("/api/v1/containers/{containerNumber}/movimentacao/{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HandlingViewModel>> GetById(string containerNumber, int handlingId)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Number = _container.Number
                })
                .FirstOrDefaultAsync(_container => _container.Number == containerNumber);
            if (container is null)
            {
                return NotFound();
            }
            Handling handling = await _containerContext.Handlings
                .AsNoTracking()
                .FirstOrDefaultAsync(_handling => _handling.Id == handlingId);
            if (handling is null)
            {
                return NotFound();
            }
            return _mapper.Map<HandlingViewModel>(handling);
        }

        /// <summary>
        /// Busca uma movimentação pelo Id
        /// </summary>
        /// <returns> A movimentação solicitada </returns>
        /// <response code="200"> Retorna a movimentação solicitada </response>
        /// <response code="404"> O container ou movimentação solicitada não existe </response>
        [HttpGet("/api/v1/containers/{containerId:int}/movimentacao{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HandlingViewModel>> GetById(int containerId, int handlingId)
        {
            Handling handling = await _containerContext.Handlings
                .AsNoTracking()
                .Where(handling => handling.Id == handlingId && handling.ContainerId == containerId)
                .FirstOrDefaultAsync();
            if (handling is null)
            {
                return NotFound();
            }
            return _mapper.Map<HandlingViewModel>(handling);
        }

        /// <summary>
        /// Lista as movimentações de forma paginada
        /// </summary>
        /// <returns> As informações de paginação e os dados das movimentações </returns>
        /// <response code="200"> Retorna as informações de paginação e os dados das movimentações </response>
        /// <response code="404"> O container solicitado não existe </response>
        [HttpGet("/api/v1/containers/{containerNumber}/movimentacao")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedViewModel<HandlingViewModel>>> GetWithPagination(
                string containerNumber,
                [FromQuery(Name = "pagina")] int page = 1,
                [FromQuery(Name = "tamanho")] int size = 10,
                [FromQuery(Name = "ordenar_por")] string sortBy = "inicio"
            )
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Id = _container.Id,
                    Number = _container.Number
                })
                .FirstOrDefaultAsync(_container => _container.Number == containerNumber);
            if (container is null)
            {
                return NotFound();
            }
            IQueryable<Handling> query = _containerContext.Handlings
                .AsNoTracking();

            string[] sortValues = sortBy.ToLower().Split('_');
            query = ResolveSortingParam(query, sortValues);

            IEnumerable<HandlingViewModel> handlings = await query
                .Where(handling => handling.ContainerId == container.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(handling => _mapper.Map<HandlingViewModel>(handling))
                .ToListAsync();
            int containersCount = await _containerContext.Containers.CountAsync();
            return new PaginatedViewModel<HandlingViewModel>(page, size, containersCount, handlings);
        }

        /// <summary>
        /// Lista as movimentações de forma paginada
        /// </summary>
        /// <returns> As informações de paginação e os dados das movimentações </returns>
        /// <response code="200"> Retorna as informações de paginação e os dados das movimentações </response>
        /// <response code="404"> O container solicitado não existe </response>
        [HttpGet("/api/v1/containers/{containerId:int}/movimentacao")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedViewModel<HandlingViewModel>>> GetWithPagination(
                int containerId,
                [FromQuery(Name = "pagina")] int page = 1,
                [FromQuery(Name = "tamanho")] int size = 10,
                [FromQuery(Name = "ordenar_por")] string sortBy = "inicio"
            )
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Id = _container.Id
                })
                .FirstOrDefaultAsync(_container => _container.Id == containerId);
            if (container is null)
            {
                return NotFound();
            }
            IQueryable<Handling> query = _containerContext.Handlings
                .AsNoTracking();

            string[] sortValues = sortBy.ToLower().Split('_');
            query = ResolveSortingParam(query, sortValues);

            IEnumerable<HandlingViewModel> handlings = await query
                .Where(handling => handling.ContainerId == containerId)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(handling => _mapper.Map<HandlingViewModel>(handling))
                .ToListAsync();
            int containersCount = await _containerContext.Containers.CountAsync();
            return new PaginatedViewModel<HandlingViewModel>(page, size, containersCount, handlings);
        }

        private static IQueryable<Handling> ResolveSortingParam(IQueryable<Handling> query, string[] sortValues)
        {
            if (sortValues.Length >= 2)
            {
                return ResolveSortingParam(query, sortKey: sortValues[0], sortPattern: sortValues[1]);
            }
            return ResolveSortingParam(query, sortKey: sortValues[0], sortPattern: "asc");

        }

        private static IQueryable<Handling> ResolveSortingParam(IQueryable<Handling> query,
            string sortKey,
            string sortPattern)
        {
            if (sortPattern == "asc")
            {
                query = query
                    .OrderBy(SelectSortKey(sortKey))
                    .ThenBy(container => container.Start);
                return query;
            }
            return query
                .OrderByDescending(SelectSortKey(sortKey))
                .ThenBy(container => container.Start);
        }

        private static Expression<Func<Handling, object>> SelectSortKey(string key)
        {
            return key switch
            {
                "tipo" => (Handling handling) => handling.HandlingType,
                "navio" => (Handling handling) => handling.Ship,
                "inicio" => (Handling handling) => handling.Start,
                "fim" => (Handling handling) => handling.End,
                _ => (Handling handling) => handling.Id
            };
        }

        /// <summary>
        /// Deleta uma movimentação pelo seu Id
        /// </summary>
        /// <response code="204"> A movimentação foi deletado</response>
        /// <response code="404"> O container ou movimentação solicitada não existe </response>
        [HttpDelete("/api/v1/containers/{containerId:int}/{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int containerId, int handlingId)
        {
            Handling handling = await _containerContext.Handlings
                .Where(handling => handling.Id == handlingId && handling.ContainerId == containerId)
                .FirstOrDefaultAsync();
            if (handling is null)
            {
                return NotFound();
            }
            _ = _containerContext.Handlings.Remove(handling);
            _ = await _containerContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deleta uma movimentação pelo seu Id
        /// </summary>
        /// <response code="204"> A movimentação foi deletado</response>
        /// <response code="404"> O container ou movimentação solicitada não existe </response>
        [HttpDelete("/api/v1/containers/{containerNumber}/{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(string containerNumber, int handlingId)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Number = _container.Number
                })
                .FirstOrDefaultAsync(_container => _container.Number == containerNumber);
            if (container is null)
            {
                return NotFound();
            }
            Handling handling = await _containerContext.Handlings
                .FirstOrDefaultAsync(_handling => _handling.Id == handlingId);
            if (handling is null)
            {
                return NotFound();
            }
            _ = _containerContext.Handlings.Remove(handling);
            _ = await _containerContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de uma movimentação
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /containers/{numero}/movimentacao/1
        ///     {
        ///         "navio": "Navio N",
        ///         "tipoMovimentaco": 0,
        ///         "dataInicio": "10/10/10,
        ///         "dataFim": "11/10/10"
        ///     }
        /// </remarks>
        /// <response code="204"> A movimentação foi atualizada</response>
        /// <response code="400"> Retorna os erros de validações </response>
        /// <response code="404"> O container ou a movimentação solicitada não existe </response>
        [HttpPut("/api/v1/containers/{containerId:int}/{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateById(int containerId,
            int handlingId,
            CreateHandlingViewModel model)
        {
            Handling handlingInDatabase = await _containerContext.Handlings
                .AsNoTracking()
                .Where(handling => handling.Id == handlingId && handling.ContainerId == containerId)
                .FirstOrDefaultAsync();
            if (handlingInDatabase is null)
            {
                return NotFound();
            }
            Handling handling = _mapper.Map<Handling>(model);
            handling.Id = handlingInDatabase.Id;
            handling.ContainerId = handlingInDatabase.ContainerId;
            _containerContext.Entry(handling).State = EntityState.Modified;
            _ = await _containerContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de uma movimentação
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /containers/{numero}/movimentacao/1
        ///     {
        ///         "navio": "Navio N",
        ///         "tipoMovimentaco": 0,
        ///         "dataInicio": "10/10/10,
        ///         "dataFim": "11/10/10"
        ///     }
        /// </remarks>
        /// <response code="204"> A movimentação foi atualizada</response>
        /// <response code="400"> Retorna os erros de validações </response>
        /// <response code="404"> O container ou a movimentação solicitada não existe </response>
        [HttpPut("/api/v1/containers/{containerNumber}/{handlingId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateById(string containerNumber,
            int handlingId,
            CreateHandlingViewModel model)
        {
            Container container = await _containerContext.Containers
                .AsNoTracking()
                .Select(_container => new Container
                {
                    Id = _container.Id,
                    Number = _container.Number
                })
                .FirstOrDefaultAsync(_container => _container.Number == containerNumber);
            if (container is null)
            {
                return NotFound();
            }
            Handling handlingInDatabase = await _containerContext.Handlings
                .AsNoTracking()
                .FirstOrDefaultAsync(_handling => _handling.Id == handlingId);
            if (handlingInDatabase is null)
            {
                return NotFound();
            }
            Handling handling = _mapper.Map<Handling>(model);
            handling.Id = handlingInDatabase.Id;
            handling.ContainerId = container.Id;
            _containerContext.Entry(handling).State = EntityState.Modified;
            _ = await _containerContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
