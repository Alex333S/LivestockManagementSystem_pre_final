public bool CheckIfIdExistsInDatabase(int id)
{
    var cow = _connection.Table<Cow>().FirstOrDefault(c => c.Id == id);
    if (cow != null)
    {
        return true;
    }

    var sheep = _connection.Table<Sheep>().FirstOrDefault(s => s.Id == id);
    if (sheep != null)
    {
        return true;
    }

    var goat = _connection.Table<Goat>().FirstOrDefault(g => g.Id == id);
    if (goat != null)
    {
        return true;
    }

    return false;
}
