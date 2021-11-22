using Firebase.Database;
using Firebase.Database.Query;
using Notes_Clone.Localdb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_Clone.FireBase
{
    public class FireBaseHelper
    {
        public static FirebaseClient fb = new FirebaseClient("https://notes-48c99-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public async static Task<List<Notes>> Getall()
        {
            try
            {
                var notelist = (await fb.Child("Notes").OnceAsync<Notes>()).Select(item => new Notes()
                {
                    TITLE = item.Object.TITLE,
                    INFO = item.Object.INFO,
                    DATE = item.Object.DATE,
                    COLOR = item.Object.COLOR,
                    ID = item.Object.ID
                }).ToList();
                return notelist;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Exception : {e}");
                return null;
            }
        }
        public async static Task<Notes> Getnote(Notes newnote)
        {
            try
            {
                var note = await Getall();               
                return note.Where(a => a.ID == newnote.ID).FirstOrDefault(); ;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception : {e}");
                return null;
            }
        }
        public async static Task<bool> Addnote(Notes newnote)
        {
            try
            {
                await fb.Child("Notes").PostAsync(newnote);                
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception : {e}");
                return false;
            }
        }
        public async static Task<bool> UpdateNote(Notes newnote)
        {
            try
            {
                var update = (await fb.Child("Notes").OnceAsync<Notes>()).Where(a => a.Object.ID == newnote.ID).FirstOrDefault();
                await fb.Child("Notes").Child(update.Key).PutAsync(newnote);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception : {e}");
                return false;
            }
        }
        public async static Task<bool> DeleteNote(Notes newnote)
        {
            try
            {
                var delete = (await fb.Child("Notes").OnceAsync<Notes>()).Where(a => a.Object.ID == newnote.ID).FirstOrDefault();
                await fb.Child("Notes").Child(delete.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception : {e}");
                return false;
            }
        }
    }
}
