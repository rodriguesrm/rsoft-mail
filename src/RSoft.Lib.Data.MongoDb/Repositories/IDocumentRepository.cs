using RSoft.Lib.Data.MongoDb.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSoft.Lib.Data.MongoDb.Repositories
{

    /// <summary>
    /// Document repository interface contract
    /// </summary>
    public interface IDocumentRepository<TDocument>
        where TDocument : IDocument
    {

        /// <summary>
        /// Get document by id key
        /// </summary>
        /// <param name="id">Document id value</param>
        Task<TDocument> GetById(string id);

        /// <summary>
        /// Get all documents
        /// </summary>
        Task<IEnumerable<TDocument>> GetAll();

        /// <summary>
        /// Add a document
        /// </summary>
        /// <param name="document">Document data object</param>
        Task Add(TDocument document);

        /// <summary>
        /// Update an existing document
        /// </summary>
        /// <param name="document">Document data object</param>
        Task Update(TDocument document);

        /// <summary>
        /// Remove an existing document
        /// </summary>
        /// <param name="document">Document data object</param>
        Task Remove(TDocument document);

    }

}
