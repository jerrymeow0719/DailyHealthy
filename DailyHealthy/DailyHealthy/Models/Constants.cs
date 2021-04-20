using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DailyHealthy.Models
{
    class Constants
    {
        public const string DatabaseFilename = "DailyHealthy.db";
        SQLiteConnection conn;

        public Constants()
        {
            string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

            using (conn = new SQLiteConnection(dbPath))
            {
                conn.CreateTable<EventModel>();
            }   
        }
        public List<EventModel> GetItemsAsync()
        {
            return conn.Table<EventModel>().ToList();
        }
        public List<EventModel> GetItemsAsync(DateTime dateTime)
        {
            return conn.Table<EventModel>().Where(i => (i.Datetime == dateTime)).ToList();
        }
        public List<EventModel> GetItemsAsync(DateTime dateTime, string name)
        {
            return conn.Table<EventModel>().Where(i => (i.Datetime == dateTime) && (i.Name == name)).ToList();
        }
        public void InsertItemAsync(EventModel eventModel)
        {
            conn.Insert(eventModel);
        }
        public void UpdateItemAsync(EventModel eventModel)
        {
            conn.Update(eventModel);
        }
        public void DeleteItemAsync(EventModel eventModel)
        {
            conn.Delete(eventModel);
        }
    }
}
