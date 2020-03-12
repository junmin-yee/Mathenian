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
                    await Database.CreateTableAsync(typeof(Account)).ConfigureAwait(false);
                    await Database.CreateTableAsync(typeof(Score)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Account>> GetAccountsAsync()
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

        public Task<int> SaveAccountAsync(Account account)
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

        public Task<int> DeleteAccountAsync(Account account)
        {
            return Database.DeleteAsync(account);
        }

        public Task<List<Score>> GetScoresAsync()
        {
            return Database.Table<Score>().ToListAsync();
        }

        public Task<Score> GetScoreByIdAsync(int id)
        {
            return Database.Table<Score>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Score> GetScoreByAttributes(int type, int topic, int mastery)
        {
            return Database.Table<Score>().Where(i => i.Type == type && i.Topic == topic && i.Mastery == mastery).FirstOrDefaultAsync();
        }

        public Task<int> SaveScoreAsync(Score score)
        {
            if (score.ID != 0)
            {
                return Database.UpdateAsync(score);
            }
            else
            {
                return Database.InsertAsync(score);
            }
        }

        public Task<int> DeleteScoreAsync(Score score)
        {
            return Database.DeleteAsync(score);
        }
    }
}
