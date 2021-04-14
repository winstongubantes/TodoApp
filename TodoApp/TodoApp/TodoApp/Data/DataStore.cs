using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TodoApp.Constants;

namespace TodoApp.Data
{
    public class DataStore<TEntity> where TEntity : new()
    {
        protected readonly SQLiteAsyncConnection Connection;

        private const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public DataStore()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppConstants.OfflineDbPath);
            var conn = new SQLiteConnection(databasePath, Flags);
            conn.CreateTable<TEntity>();
            Connection = new SQLiteAsyncConnection(databasePath, Flags);
        }

        public AsyncTableQuery<TEntity> Query => Connection.Table<TEntity>();

        public virtual async Task InsertAsync(TEntity record)
        {
            //Api submit successful. Insert to Db.
            await Connection.InsertAsync(record);
        }

        public virtual async Task UpdateAsync(TEntity record)
        {
            //Api submit successful. Insert to Db.
            await Connection.UpdateAsync(record);
        }

        public virtual async Task DeleteAsync(TEntity record)
        {
            await Connection.DeleteAsync(record);
        }

        public virtual async Task DeleteAllAsync()
        {
            await Connection.DropTableAsync<TEntity>();
            await Connection.CreateTableAsync<TEntity>();
        }
    }
}