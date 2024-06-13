using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DAL;

namespace BookApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _databaseConnection;
    }
}
