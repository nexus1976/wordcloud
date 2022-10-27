using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wordcloud.Data;
using wordcloud.Domain;
using wordcloud.Models;

namespace wordcloud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IModelMappingService _modelMappingService;
        private readonly ICommandContext _commandContext;
        private readonly IDomainServices _domainServices;

        public CommentsController(ICommandContext commandContext, IModelMappingService modelMappingService, 
            IDomainServices domainServices, ILogger<CommentsController> logger)
        {
            _commandContext = commandContext;
            _modelMappingService = modelMappingService;
            _domainServices = domainServices;
            _logger = logger;
        }

        // GET /comments?page=1&pageSize=50&fromDate=10%2F27%2F2022&toDate=10%2F31%2F2022
        [HttpGet("")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery]FilterModel model, CancellationToken cancellationToken)
        {
            try
            {
                model ??= new FilterModel();
                if (model.PageSize < 1)
                    model.PageSize = FilterModel.DEFAULT_PAGESIZE;
                if (model.Page < 1)
                    model.Page = FilterModel.DEFAULT_PAGE;
                var query = _commandContext.Comments.AsNoTracking();
                if (model.FromDate.HasValue)
                    query = query.Where(d => d.CommentDate >= model.FromDate.Value);
                if (model.ToDate.HasValue)
                    query = query.Where(d => d.CommentDate <= model.ToDate.Value);
                var results = await query.Skip(model.SkipSize).Take(model.PageSize).ToListAsync(cancellationToken);
                var response = _modelMappingService.MapCommentEntitiesToCommentModels(results);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET /comments/2ced1003-fda1-400f-8cc4-119217305563
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _commandContext.Comments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
                if (query == null)
                    return NotFound();
                var result = _modelMappingService.MapCommentEntityToCommentModel(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST /comments
        // {
        //  "comment": "this is a comment of some sort"
        // }
        [HttpPost("")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]CommentModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.Comment))
                    return BadRequest();
                var entity = _modelMappingService.MapCommentModelToCommentEntity(model);
                if (entity == null)
                    return BadRequest();
                entity.Id = Guid.NewGuid();
                entity.CommentDate = DateTime.UtcNow;
                entity.CommentParsed = _domainServices.ParseComment(entity.CommentText);
                _commandContext.Comments.Add(entity);
                var wordCounts = await _domainServices.GetWordCountsAsync(entity.CommentParsed, _commandContext, cancellationToken);
                await _commandContext.SaveChangesAsync(cancellationToken);
                var response = _modelMappingService.MapCommentEntityToCommentModel(entity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
