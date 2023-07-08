using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.DAL.DTO_s.Label;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.BLL.RepositoryPattern.Services
{
    public class LabelService : BaseService, ILabelService
    {
        public LabelService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base( mapper, dbContext )
        { }

        public async Task<Response<LabelDto>> 
        CreateLabel
        (
            LabelCreateDto labelDto
        )
        {
            try
            {
                var label = _mapper.Map<Label>(labelDto);

                _dbContext.Labels.Add(label);
                await _dbContext.SaveChangesAsync();

                return Response<LabelDto>.Ok(_mapper.Map<LabelDto>(label), "Label created successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<List<LabelDto>>> 
        GetLabels()
        {
            try
            {
                var labels = await _dbContext.Labels.ToListAsync();

                return Response<List<LabelDto>>.Ok(_mapper.Map<List<LabelDto>>(labels), "Labels retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<LabelDto>>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<LabelDto>> 
        GetLabelById
        (
            Guid labelId
        )
        {
            try
            {
                var label = await _dbContext.Labels.FindAsync(labelId);
                if (label == null)
                    return Response<LabelDto>.NotFound($"Label with id: {labelId} doesn't exist.");

                return Response<LabelDto>.Ok(_mapper.Map<LabelDto>(label), $"Label {label.Name} retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<LabelDto>> 
        UpdateLabel
        (
            Guid labelId, 
            LabelUpdateDto labelDto
        )
        {
            try
            {
                var label = await _dbContext.Labels.FindAsync(labelId);
                if (label == null)
                    return Response<LabelDto>.NotFound($"Label with id: {labelId} doesn't exist.");

                _mapper.Map(labelDto, label);
                await _dbContext.SaveChangesAsync();

                return Response<LabelDto>.Ok(_mapper.Map<LabelDto>(label), $"Label with id: {labelId} updated successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>> 
        DeleteLabel
        (
            Guid labelId
        )
        {
            try
            {
                var label = await _dbContext.Labels.FindAsync(labelId);
                if (label == null)
                    return Response<bool>.NotFound($"Label with id: {labelId} doesn't exist.");

                _dbContext.Labels.Remove(label);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Label with id: {labelId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }
    }
}