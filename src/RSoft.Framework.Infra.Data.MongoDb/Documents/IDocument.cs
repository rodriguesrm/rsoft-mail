using System;

namespace RSoft.Framework.Infra.Data.MongoDb.Documents
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
