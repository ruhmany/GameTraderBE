using GameTrader.Core.DTOs.ItemDTOs;
using GameTrader.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        public ItemController(IItemService itemService, ICategoryService categoryService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddNewItem([FromBody] AddItemDTO itemDTO)
        {
            if (itemDTO == null)
            {
                return BadRequest("Item data is required.");
            }
            var result = await _itemService.AddNewItem(itemDTO);
            if (result)
            {
                return Ok("Item added successfully.");
            }
            return BadRequest("Failed to add item.");
        }

        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItem([FromBody] EditItemDTO itemDTO)
        {
            if (itemDTO == null)
            {
                return BadRequest("Item data is required.");
            }
            var result = await _itemService.UpdateItem(itemDTO);
            if (result)
            {
                return Ok("Item updated successfully.");
            }
            return BadRequest("Failed to update item.");
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] string categoryName)
        {
            if (categoryName == null)
            {
                return BadRequest("Category data is required.");
            }
            var result = await _categoryService.CreateNewCategory(categoryName);
            if (result)
            {
                return Ok("Category added successfully.");
            }
            return BadRequest("Failed to add category.");
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }


    }
}
