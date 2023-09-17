using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.Label;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    public class LabelController : BaseController
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        /// <summary>
        /// Retrieves all labels.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetLabels()
        {
            var response = await _labelService.GetLabels();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a label by its ID.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabelById(Guid id)
        {
            var response = await _labelService.GetLabelById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new label.
        /// </summary>
        /// <param name="labelDto">The DTO containing the label data.</param>
        [HttpPost]
        public async Task<IActionResult> CreateLabel(LabelCreateDto labelDto)
        {
            var response = await _labelService.CreateLabel(labelDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing label by its ID.
        /// </summary>
        /// <param name="id">The ID of the label to update.</param>
        /// <param name="labelDto">The DTO containing the updated label data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLabel(Guid id, LabelUpdateDto labelDto)
        {
            var response = await _labelService.UpdateLabel(id, labelDto);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}