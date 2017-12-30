using Freshmvvm.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshmvvm
{
    public class Repository
    {
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public Repository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Contact>().Wait();
        }

        public async Task CreateContact(Contact contact)
        {
            try
            {
                // Basic validation to ensure we have a contact name.
                if (string.IsNullOrWhiteSpace(contact.Name))
                    throw new Exception("Name is required");

                // Insert/update contact.
                var result = await conn.InsertOrReplaceAsync(contact).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Contact Name: {contact.Name}])";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to create contact: {contact.Name}. Error: {ex.Message}";
            }
        }

        public Task<List<Contact>> GetAllContacts()
        {
            return conn.Table<Contact>().ToListAsync();
        }
    }
}
