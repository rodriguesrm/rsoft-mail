using RSoft.Framework.Infra.Data.MongoDb.Creators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSoft.Framework.Infra.Data.MongoDb.IoC
{

    /// <summary>
    /// Criador de objetos de banco de dados MongoDb (Collections, Indexes, etc)
    /// </summary>
    public class MongoCollectionCreator : IDatabaseCreator
    {

        #region Local Objects/Variables

        private readonly IEnumerable<IDocumentCollectionCreator> _creators;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new MongoCollectionCreator instance
        /// </summary>
        /// <param name="criators">Creators object list</param>
        public MongoCollectionCreator(IEnumerable<IDocumentCollectionCreator> criators)
        {
            _creators = criators;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task CreateDatabase()
            => await Task.WhenAll(_creators.Select(creator => creator.CreateCollection()));

        #endregion

    }

}
