using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Clone.Localdb
{
    public class Database
    {
        public static SQLiteAsyncConnection db;   
        
        public Database (string Dbpath)
        {
            db = new SQLiteAsyncConnection(Dbpath);
            db.CreateTableAsync<Notes>().Wait();
        }              
        public static Task<Notes> GetNote(int id)
        {
            _ = App.Localdatabase;
            return db.Table<Notes>().Where(z => z.ID == id).FirstOrDefaultAsync();
        }
        public static  Task<List<Notes>> Onget()
        {
            _ = App.Localdatabase;
            return  db.Table<Notes>().ToListAsync();                          
        }
        public static async Task<int> OnSave(Notes x)
        {
            _ = App.Localdatabase;
            if (x.ID!=0)
            {
               return await db.UpdateAsync(x);
            }
            else
            {
               return await db.InsertAsync(x);
            }
        }
        public static async Task<int> Ondelete(Notes x)
        {
            _ = App.Localdatabase;
            return await db.DeleteAsync(x);
           
        }
    }
}
