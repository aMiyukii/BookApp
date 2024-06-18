using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }
    }
}
