namespace AnilWebAPI.Models
{
    /// <summary>
    /// Base class that can be used to create a CrudController
    /// </summary>
    public abstract class OperationModel
    {
        /// <summary>
        /// Id is the default property of all OperationModel objects
        /// </summary>
        public int Id { get; set; }
    }
}