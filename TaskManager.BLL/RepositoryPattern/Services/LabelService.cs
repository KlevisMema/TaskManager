/*
    This service class provides CRUD operations and business logic for managing labels.
    It interacts with the database through the application's DbContext and uses AutoMapper for mapping between DTOs and entities.

    - CreateLabel: Creates a new label.
    - GetLabels: Retrieves a list of all labels.
    - GetLabelById: Retrieves a label by its ID.
    - UpdateLabel: Updates an existing label.
    - SoftDeleteLabel: Soft deletes a label by its ID.
    - HardDeleteLabel: Hard deletes a label by its ID.
    - RestoreLabel: Restores a soft-deleted label by its ID.
*/

#region Usings
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL.Context;
using TaskManager.DAL.DTO_s.Label;
using TaskManager.DAL.Models;
using TaskManager.HELPERS.LogsHelper;
using TaskManager.HELPERS.ServiceResponse;
using TaskManager.BLL.BaseServices;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
#endregion

namespace TaskManager.BLL.RepositoryPattern.Services
{
    /// <summary>
    /// This service class provides CRUD operations and business logic for managing labels.
    /// </summary>
    public class LabelService : BaseService, ILabelService
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="dbContext">The application's DbContext.</param>
        public LabelService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new label.
        /// </summary>
        /// <param name="labelDto">The DTO containing the label data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the created label DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the label.</exception>
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
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves a list of all labels.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a list of label DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving labels.</exception>
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
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<List<LabelDto>>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves a label by its ID.
        /// </summary>
        /// <param name="labelId">The ID of the label to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the label DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the label.</exception>
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
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Updates an existing label.
        /// </summary>
        /// <param name="labelId">The ID of the label to update.</param>
        /// <param name="labelDto">The DTO containing the updated label data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the updated label DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the label.</exception>
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
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Soft deletes a label by its ID.
        /// </summary>
        /// <param name="labelId">The ID of the label to soft delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the soft deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the label.</exception>
        public async Task<Response<bool>>
        SoftDeleteLabel
        (
            Guid labelId
        )
        {
            try
            {
                var label = await _dbContext.Labels.FindAsync(labelId);
                if (label == null)
                    return Response<bool>.NotFound($"Label with id: {labelId} doesn't exist.");

                label.IsDeleted = true;
                label.DeletedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Label with id: {labelId} soft deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Hard deletes a label by its ID.
        /// </summary>
        /// <param name="labelId">The ID of the label to hard delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the hard deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the label.</exception>
        public async Task<Response<bool>>
        HardDeleteLabel
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

                return Response<bool>.Ok(true, $"Label with id: {labelId} hard deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Restores a soft-deleted label by its ID.
        /// </summary>
        /// <param name="labelId">The ID of the label to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing a boolean indicating if the restoration was successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the label.</exception>
        public async Task<Response<LabelDto>>
        RestoreSoftDeletedLabel
        (
            Guid labelId
        )
        {
            try
            {
                var label = await _dbContext.Labels.FindAsync(labelId);
                if (label == null)
                    return Response<LabelDto>.NotFound($"Label with id: {labelId} doesn't exist.");

                label.IsDeleted = false;
                label.EditedAt = DateTime.Now;
                label.DeletedAt = null;
                await _dbContext.SaveChangesAsync();

                return Response<LabelDto>.Ok(_mapper.Map<LabelDto>(label), $"Label with id: {labelId} restored successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<LabelDto>.ErrorMsg(ex.ToString());
            }
        }
        #endregion
    }
}