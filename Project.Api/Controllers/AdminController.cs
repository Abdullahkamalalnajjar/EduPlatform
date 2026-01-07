using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Bases;
using Project.Core.Features.Admin.Queries.Models;
using Project.Core.Features.Users.Queries.Models;
using Project.Data.Dtos;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all users (Students, Teachers, Parents) - Admin only
        /// </summary>
        /// <returns>All users grouped by role</returns>
        [HttpGet("users")]
        [ProducesResponseType(typeof(Response<GetAllUsersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllUsersResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query, cancellationToken);
                
                if (!result.Succeeded)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                return BadRequest(new Response<GetAllUsersResponse> 
                { 
                    Succeeded = false, 
                    Message = $"Error retrieving users: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Get all students only
        /// </summary>
        [HttpGet("users/students")]
        [ProducesResponseType(typeof(Response<IEnumerable<UserStudentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query, cancellationToken);
                
                if (!result.Succeeded)
                    return BadRequest(result);

                return Ok(new Response<IEnumerable<UserStudentDto>>
                {
                    Succeeded = true,
                    Data = result.Data!.Students,
                    Message = "Students retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving students");
                return BadRequest(new Response<IEnumerable<UserStudentDto>>
                {
                    Succeeded = false,
                    Message = $"Error retrieving students: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get all teachers only
        /// </summary>
        [HttpGet("users/teachers")]
        [ProducesResponseType(typeof(Response<IEnumerable<UserTeacherDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeachers(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query, cancellationToken);
                
                if (!result.Succeeded)
                    return BadRequest(result);

                return Ok(new Response<IEnumerable<UserTeacherDto>>
                {
                    Succeeded = true,
                    Data = result.Data!.Teachers,
                    Message = "Teachers retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teachers");
                return BadRequest(new Response<IEnumerable<UserTeacherDto>>
                {
                    Succeeded = false,
                    Message = $"Error retrieving teachers: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get all parents only
        /// </summary>
        [HttpGet("users/parents")]
        [ProducesResponseType(typeof(Response<IEnumerable<UserParentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllParents(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query, cancellationToken);
                
                if (!result.Succeeded)
                    return BadRequest(result);

                return Ok(new Response<IEnumerable<UserParentDto>>
                {
                    Succeeded = true,
                    Data = result.Data!.Parents,
                    Message = "Parents retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving parents");
                return BadRequest(new Response<IEnumerable<UserParentDto>>
                {
                    Succeeded = false,
                    Message = $"Error retrieving parents: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get dashboard statistics - Admin only
        /// </summary>
        /// <returns>Statistics for teachers, students, parents, exams, courses, and users</returns>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(Response<AdminStatisticsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AdminStatisticsResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetStatistics(CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAdminStatisticsQuery();
                var result = await _mediator.Send(query, cancellationToken);
                
                if (!result.Succeeded)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard statistics");
                return BadRequest(new Response<AdminStatisticsResponse>
                {
                    Succeeded = false,
                    Message = $"Error retrieving statistics: {ex.Message}"
                });
            }
        }
    }
}
