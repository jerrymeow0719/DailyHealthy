using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DailyHealthy.Models
{
    class Constants
    {
        public const string DatabaseFilename = "DailyHealthy.db";
        SQLiteAsyncConnection conn;

        public Constants()
        {
            string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<ConstantsModel>().Wait();
        }
        public async Task<List<ConstantsModel>> GetItemsAsync()
        {
            return await conn.Table<ConstantsModel>().ToListAsync();
        }
        public async Task<List<ConstantsModel>> GetItemsAsync(DateTime dateTime)
        {
            return await conn.Table<ConstantsModel>().Where(i => (i.Datetime == dateTime)).ToListAsync();
        }
        public async Task<List<ConstantsModel>> GetItemsAsync(int year,int month)
        {
            return await conn.Table<ConstantsModel>().Where(i => (i.Datetime.Year == year) && (i.Datetime.Month == month)).ToListAsync();
        }
        public async Task<ConstantsModel> GetItemsAsync(DateTime dateTime, string name)
        {
            return await conn.Table<ConstantsModel>().Where(i => (i.Datetime == dateTime) && (i.Name == name)).FirstOrDefaultAsync();
        }
        public async void InsertItemAsync(ConstantsModel constantsModel)
        {
           await conn.InsertAsync(constantsModel);
        }
        public async void UpdateItemAsync(ConstantsModel constantsModel)
        {
            await conn.UpdateAsync(constantsModel);
        }
        public async void DeleteItemAsync(ConstantsModel constantsModel)
        {
            await conn.DeleteAsync(constantsModel);
        }
    }
}
