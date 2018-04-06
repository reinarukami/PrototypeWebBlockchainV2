using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Npgsql;
using PrototypeWebBlockchain.Models;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Web;
using System.Configuration;

namespace PrototypeWebBlockchain.Repository
{
    public class MemberRepository : IRepository<Member>
    {

        private string connectionString;
        public MemberRepository()
        {
            connectionString = ConfigurationManager.AppSettings["PostgresConnection"];
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Member item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO member_t (email, password, fname,lname,age,contact,address) VALUES(@email, @password, @fname,@lname,@age,@contact,@address)", item);
            }
        }

        public IEnumerable<Member> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var list = dbConnection.Query<Member>("SELECT * FROM member_t where active = 1");
                return list;
            }
        }

        public Member FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Member>("SELECT * FROM member_t WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public Member Login(string email, string password)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Member>("Select * FROM member_t WHERE email = @Email AND password = @Password and active = 1", new { Email = email, Password = password }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE member_t SET active = 0 WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Member item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE member_t SET email = @email, passwod = @password,  fname = @fname, lname = @lname, age = @age, contact = @contact, address = @address WHERE id = @Id", item);
            }
        }


    }
}