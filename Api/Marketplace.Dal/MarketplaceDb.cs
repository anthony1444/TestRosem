// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Marketplace.Core.Model;
using Microsoft.Data.Sqlite;

namespace Marketplace.Dal
{
    internal class MarketplaceDb : IMarketplaceDb, IDisposable
    {
        private readonly SqliteConnection _connection;

        public MarketplaceDb()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            _connection = new SqliteConnection($@"Data Source={path}\Marketplace.Dal\marketplace.db");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<User[]> GetUsersAsync()
        {
            await using var command = new SqliteCommand(
                "SELECT U.Id, U.Username, COUNT(O.Id) AS Offers\r\n" +
                "FROM User U LEFT JOIN Offer O ON U.Id = O.UserId\r\n" +
                "GROUP BY U.Id, U.Username;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                var results = new List<User>();

                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    results.Add(user);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<Offer[]> GetOffersAsync(int pageNumber, int pageSize)
        {
            await using var command = new SqliteCommand(
            "SELECT *,c.Name as category_name FROM Offer o join Category c on o.CategoryId = c.Id  join User u on o.UserId  = u.Id LIMIT " + pageSize + " OFFSET (" + pageNumber + " - 1) * " + pageSize + ";",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                var results = new List<Offer>();

                while (await reader.ReadAsync())
                {
                    var user = new Offer
                    {
                        // Username = reader.GetString(reader.GetOrdinal("Username"))
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        User = new User
                        {
                            Username = reader.GetString(reader.GetOrdinal("Username"))
                        },
                        Category = new Category
                        {
                            Id = Convert.ToByte(reader.GetString(reader.GetOrdinal("CategoryId"))),
                            Name = reader.GetString(reader.GetOrdinal("category_name")),
                        }
                    };

                    results.Add(user);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public async Task<int> GetCountOffer()
        {
            await using var command = new SqliteCommand(
            "Select count(*) as Count FROM  Offer;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                if (await reader.ReadAsync())
                {
                    var count = reader.GetInt32(reader.GetOrdinal("Count"));
                    return count;
                }

                return 0; // Valo
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> AddNewOffer(Offer offer)
        {
            await using var command = new SqliteCommand(
            "INSERT INTO Offer (CategoryId, Description, Id, Location, PublishedOn, PictureUrl, Title, UserId) " +
            "VALUES (@CategoryId, @Description, @Id, @Location, @PublishedOn, @PictureUrl, @Title, @UserId);",
            _connection);

            // Asignar los parámetros del comando
            command.Parameters.AddWithValue("@CategoryId", offer.CategoryId);
            command.Parameters.AddWithValue("@Description", offer.Description);
            command.Parameters.AddWithValue("@Id", offer.Id);
            command.Parameters.AddWithValue("@Location", offer.Location);
            command.Parameters.AddWithValue("@PictureUrl", offer.PictureUrl);
            command.Parameters.AddWithValue("@PublishedOn", offer.PublishedOn);
            command.Parameters.AddWithValue("@Title", offer.Title);
            command.Parameters.AddWithValue("@UserId", 1);

            try
            {
                // Ejecutar el comando
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
