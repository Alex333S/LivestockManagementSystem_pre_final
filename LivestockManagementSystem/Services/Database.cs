using LivestockManagementSystem.Models;
using SQLite;
using static Microsoft.Maui.Storage.FileSystem;

namespace LivestockManagementSystem.Services;

public class Database
{
    private readonly SQLiteConnection _connection;
    
    public Database()

    {   

        string dbName = "FarmDataOriginal.db";
        string dbPath = Path.Combine(Current.AppDataDirectory, dbName);
        if (!File.Exists(dbPath))
        {
            

            //open the db file from the asset folder
            using Stream stream = Current.OpenAppPackageFileAsync(dbName).Result;

            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            //write db data to app directory
            File.WriteAllBytes(dbPath, memoryStream.ToArray());
        }
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTables<Cow, Sheep, Goat>();//create if not exist
       // _connection.DeleteAll<Goat>();
        
    }


    public List<Animal> ReadItems()
    {
        var animals = new List<Animal>();
        var lst1 = _connection.Table<Cow>().ToList();
        animals.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList();
        animals.AddRange(lst2);
        var lst3 = _connection.Table<Goat>().ToList();
        animals.AddRange(lst3);
        return animals;

    }
    public int InsertItem(Animal item)
    {
        return _connection.Insert(item);
    }
    public int DeleteItem(Animal item)
    {
        return _connection.Delete(item);
    }
    public int UpdateItem(Animal item)
    {
        return _connection.Update(item);
    }
    public int GetNextCowId()
    {
        var lastCow = _connection.Table<Cow>().OrderByDescending(c => c.Id).FirstOrDefault();
        if (lastCow != null)
        {
            return lastCow.Id + 1;
        }
        return 1;
    }
    public int GetNextSheepId()
    {
        var lastSheep = _connection.Table<Sheep>().OrderByDescending(s => s.Id).FirstOrDefault();
        if (lastSheep != null)
        {
            return lastSheep.Id + 1;
        }
        return 50001;
    }
    public int GetNextGoatId()
    {
        var lastGoat = _connection.Table<Goat>().OrderByDescending(s => s.Id).FirstOrDefault();
        if (lastGoat != null)
        {
            return lastGoat.Id + 1;
        }
        return 100001;
    }


}

