using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHealthProfile.Models;
using MyHealthProfile.Models.Dtos;
using MyHealthProfile.Repositories.Allergies;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserAllergiesController : ControllerBase
{
    private readonly IUserAllergyService _userAllergyService;

    public UserAllergiesController(IUserAllergyService userAllergyService)
    {
        _userAllergyService = userAllergyService;
    }

    /// <summary>
    /// Get a specific user allergy by ID.
    /// </summary>
    /// <param name="id">The ID of the user allergy.</param>
    /// <returns>The user allergy details.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var userAllergy = await _userAllergyService.GetAllergyAsync(id);
            if (userAllergy == null)
            {
                return NotFound("Allergy not found.");
            }

            return Ok(userAllergy);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not logged in.");
        }
    }

    /// <summary>
    /// Update an existing user allergy.
    /// </summary>
    /// <param name="id">The ID of the user allergy to update.</param>
    /// <param name="userAllergy">The updated user allergy details.</param>
    /// <returns>The updated user allergy.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromQuery]int id, [FromBody] UserAllergyDto userAllergy)
    {
        try
        {
            var updatedAllergy = await _userAllergyService.UpdateUserAllergyAsync(userAllergy);
            if (updatedAllergy == null)
            {
                return NotFound("Allergy not found.");
            }

            return Ok(updatedAllergy);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not logged in.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete a user allergy by ID.
    /// </summary>
    /// <param name="id">The ID of the user allergy to delete.</param>
    /// <returns>A success message.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _userAllergyService.DeleteUserAllergyAsync(id);
            if (success)
            {
                return Ok("Allergy deleted successfully.");
            }

            return BadRequest("Unable to delete allergy.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not logged in.");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserAllergyDto userAllergy)
    {
        try
        {
            var result = await _userAllergyService.AddAllergyAsync(userAllergy);
            if (result)
            {
                return Ok("Allergy created successfully.");
            }

            return BadRequest("Unable to create allergy.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not logged in.");
        }
    }

    [HttpGet("list/{userId}")]
    [AllowAnonymous]
    public async Task<IActionResult> List(string userId)
    {
        try
        {
            var result = await _userAllergyService.AllergiesListAsync(userId);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Unable to get allergies List.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not logged in.");
        }
    }
}
