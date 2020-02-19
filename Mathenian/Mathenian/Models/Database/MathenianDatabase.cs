using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathenian.Models
{
    public class MathenianDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public MathenianDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Account).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Account)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Account>> GetItemsAsync()
        {
            return Database.Table<Account>().ToListAsync();
        }

        public Task<Account> GetAccountByIdAsync(int id)
        {
            return Database.Table<Account>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Account> GetAccountByCredentialAsync(string user, string pass)
        {
            return Database.Table<Account>().Where(i => i.Username == user && i.Password == pass).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Account account)
        {
            if (account.ID != 0)
            {
                return Database.UpdateAsync(account);
            }
            else
            {
                return Database.InsertAsync(account);
            }
        }

        public Task<int> DeleteItemAsync(Account account)
        {
            return Database.DeleteAsync(account);
        }
    }
}
