using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.DAL.DTO_s.Priority;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.ServicesInterfaces;

namespace TaskManager.BLL.Services
{
    public class PriorityService : BaseService, IPriorityService
    {
        public PriorityService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {

        }

        public async Task<Response<PriorityDto>>
        CreatePriority
        (
            PriorityCreateDto priorityDto
        )
        {
            try
            {
                var priority = _mapper.Map<Priority>(priorityDto);

                _dbContext.Priorities.Add(priority);
                await _dbContext.SaveChangesAsync();

                return Response<PriorityDto>.Ok(_mapper.Map<PriorityDto>(priority), "Priority created successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<PriorityDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<List<PriorityDto>>>
        GetPriorities()
        {
            try
            {
                var priorities = await _dbContext.Priorities.ToListAsync();

                return Response<List<PriorityDto>>.Ok(_mapper.Map<List<PriorityDto>>(priorities), "Priorities retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<PriorityDto>>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<PriorityDto>>
        GetPriorityById
        (
            Guid priorityId
        )
        {
            try
            {
                var priority = await _dbContext.Priorities.FindAsync(priorityId);
                if (priority == null)
                    return Response<PriorityDto>.NotFound($"Priority with id: {priorityId} doesn't exist.");

                return Response<PriorityDto>.Ok(_mapper.Map<PriorityDto>(priority), $"Priority {priority.Name} retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<PriorityDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<PriorityDto>>
        UpdatePriority
        (
            Guid priorityId,
            PriorityUpdateDto priorityDto
        )
        {
            try
            {
                var priority = await _dbContext.Priorities.FindAsync(priorityId);
                if (priority == null)
                    return Response<PriorityDto>.NotFound($"Priority with id: {priorityId} doesn't exist.");

                _mapper.Map(priorityDto, priority);
                await _dbContext.SaveChangesAsync();

                return Response<PriorityDto>.Ok(_mapper.Map<PriorityDto>(priority), $"Priority with id: {priorityId} updated successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<PriorityDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>>
        DeletePriority
        (
            Guid priorityId
        )
        {
            try
            {
                var priority = await _dbContext.Priorities.FindAsync(priorityId);
                if (priority == null)
                    return Response<bool>.NotFound($"Priority with id: {priorityId} doesn't exist.");

                _dbContext.Priorities.Remove(priority);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Priority with id: {priorityId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

    }
}