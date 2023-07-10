namespace TaskManager.DTO.DTO_s.Label
{
    /// <summary>
    /// DTO for a label.
    /// </summary>
    public class LabelDto
    {
        /// <summary>
        /// Gets or sets the ID of the label.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}