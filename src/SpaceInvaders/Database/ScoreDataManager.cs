using LiteDB;
using SpaceInvaders.Logger;

namespace SpaceInvaders.Database;

internal class ScoreDataManager
{
    private static LiteDatabase? database;
    private static ILiteCollection<ScoreData>? collection;

    public static void Initializing(string fileName)
    {
        Log.WriteInfo("[DATABASE] Initialiing database.");

        try
        {
            database = new(fileName);
            collection = database.GetCollection<ScoreData>("scoreDatas");
        }
        catch
        {
            Log.WriteFatal("[DATABASE] Failed to open database.");
        }
    }

    public static void Finalizing()
    {
        Log.WriteInfo("[DATABASE] Dispose Detabase.");

        database?.Dispose();
    }

    public static void AddScore(ScoreData data)
    {
        if (collection == null)
            return;

        if (!string.IsNullOrWhiteSpace(data.Name)
            && ContainsData(data.Name))
        {
            var myData = GetData(data.Name);

            if (myData == null)
            {
                Log.WriteError("[DATABASE] Failed to get data.");
                return;
            }

            if (myData.Score < data.Score)
            {
                myData.Score = data.Score;
                collection.Update(myData);

                Log.WriteInfo("[DATABASE] Update data.");
            }

            return;
        }

        collection.Insert(data);

        Log.WriteInfo($"[DATABASE] Create data.");
    }

    public static IEnumerable<ScoreData>? GetAllData()
    {
        if (collection == null)
            return null;

        return collection.FindAll();
    }


    public static void AllDelete()
    {
        collection?.DeleteAll();

        Log.WriteInfo($"[DATABASE] Delete All data.");
    }

    public static bool ContainsData(string name)
    {
        if (collection == null)
            return false;

        var result = collection.Find(d => d.Name == name);

        return result.Any();
    }

    public static ScoreData? GetData(string name)
    {
        if(collection == null)
            return null;

        return collection.FindOne(d => d.Name == name);
    }
}