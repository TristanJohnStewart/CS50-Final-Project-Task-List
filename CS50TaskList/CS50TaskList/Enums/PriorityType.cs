namespace CS50TaskList.Enums
{
    /// <summary>
    ///     An enum class for the different kinds of priorities a <see cref="Data.Entities.Task"/> can have.
    /// </summary>
    public enum PriorityType
    {
        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that has no priority selected.
        /// </summary>
        None,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> with a low level priority.
        /// </summary>
        Low,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> with a medium level priority.
        /// </summary>
        Medium,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> with a high level priority.
        /// </summary>
        High,
    }
}
