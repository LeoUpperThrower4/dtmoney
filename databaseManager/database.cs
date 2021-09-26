using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Types;
using dtOperations;
using static FaunaDB.Query.Language;

namespace databaseManager
{
    public static class Database
    {
        // Levar para um arquivo de ambiente
        private static readonly string FAUNA_SECRET = "fnAET_D6GXAKRS2ZYT773qQF3sp44O752UenlQzI";
        //

        private static FaunaClient client;

        /// <summary>
        /// Initialize the FaunaClient object
        /// </summary>
        static Database()
        {
            client = new FaunaClient(FAUNA_SECRET);
        }

        /// <summary>
        /// Creates a user in the database given it's info
        /// </summary>
        /// <param name="user">User to be added to the database</param>
        /// <returns>The user added to the database</returns>
        public static async Task<FaunaUser> CreateUser(FaunaUser user)
        {
            await client.Query(Create(Collection("users"), Obj("data", Encoder.Encode(user))));
            return user;
        }

        /// <summary>
        /// Gets user in database given the ID
        /// </summary>
        /// <param name="id">ID of the user to be queried</param>
        /// <returns>A FaunaUser object containing the user's info</returns>
        public static async Task<FaunaUser> GetUser(string id)
        {
            Value result = await client.Query(Get(Match(Index("user_by_id"), id)), TimeSpan.FromSeconds(42));
            
            Value converted = result.At("data");

            string user = (string)converted.At("_name");
            float wallet = (float)(double)converted.At("_wallet");
            List<Transaction> transactionsList = converted.At("_transactionsList").To<List<Transaction>>().ToOption.Value;

            return new FaunaUser(user, id, wallet, transactionsList);
        }

        /// <summary>
        /// Updates a user in the database given the ID and the new info
        /// </summary>
        /// <param name="id">ID of the user to be changed</param>
        /// <param name="updatedUser">New user info</param>
        /// <returns>The updated user</returns>
        public static async Task<FaunaUser> UpdateUser(string id, FaunaUser updatedUser)
        {
            await client.Query(Replace(Select("ref", Get(Match(Index("user_by_id"), id))), Obj("data", Encoder.Encode(updatedUser))));

            return updatedUser;
        }
    }
}
