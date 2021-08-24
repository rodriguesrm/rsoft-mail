using System;

namespace RSoft.Lib.Data.MongoDb.Documents
{

    /// <summary>
    /// Entity interface
    /// </summary>
    public interface IDocument
    {

        /// <summary>
        /// Entity id value
        /// </summary>
        string Id { get; }

    }

}
