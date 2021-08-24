using System.Threading.Tasks;

namespace RSoft.Lib.Data.MongoDb.Creators
{

    /// <summary>
    /// Document collection creator interface contract
    /// </summary>
    public interface IDocumentCollectionCreator
    {

        /// <summary>
        /// Perform a create collection
        /// </summary>

        Task CreateCollection();

    }

}
