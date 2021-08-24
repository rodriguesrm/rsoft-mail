using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;

namespace RSoft.Lib.Data.MongoDb.Abstractions
{

    /// <summary>
    /// Conversions from ObjectId<->String to Application-MongoDb
    /// </summary>
    public class StringObjectIdConvention : ConventionBase, IPostProcessingConvention
    {

        ///<inheritdoc/>
        public void PostProcess(BsonClassMap classMap)
        {
            BsonMemberMap mappedId = classMap.IdMemberMap;
            if (mappedId != null && mappedId.MemberName == "Id" && mappedId.MemberType == typeof(string))
            {
                mappedId.SetIdGenerator(new StringObjectIdGenerator());
            }
        }
    }

}
