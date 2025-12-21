namespace CS50TaskList.Enums
{
    /// <summary>
    ///     An enum class for the different kinds of recurrances a <see cref="Data.Entities.Task"/> can have.
    /// </summary>
    public enum RecurranceType
    {
        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that does not reoccur.
        /// </summary>
        None,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that reoccurs daily.
        /// </summary>
        Daily,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that reoccurs every week.
        /// </summary>
        Weekly,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that reoccurs every month.
        /// </summary>
        Monthly,

        /// <summary>
        ///     An enum member for a <see cref="Data.Entities.Task"/> that reoccurs every year.
        /// </summary>
        Yearly,
    }
}
