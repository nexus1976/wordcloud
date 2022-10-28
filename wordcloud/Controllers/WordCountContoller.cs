using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wordcloud.Data;
using wordcloud.Domain;
using wordcloud.Models;

namespace wordcloud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountsContoller : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IModelMappingService _modelMappingService;
        private readonly ICommandContext _commandContext;

        public WordCountsContoller(ILogger<CommentsController> logger, IModelMappingService modelMappingService, ICommandContext commandContext)
        {
            _logger = logger;
            _modelMappingService = modelMappingService;
            _commandContext = commandContext;
        }

        [HttpGet("")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var wordCounts = await _commandContext.WordCounts.AsNoTracking().OrderByDescending(d => d.Count).Take(100).ToListAsync(cancellationToken);
                var results = _modelMappingService.MapWordCountEntitiessToWordCountModels(wordCounts);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
