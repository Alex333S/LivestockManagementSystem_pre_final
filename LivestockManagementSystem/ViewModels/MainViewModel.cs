using System.Collections.ObjectModel;
using System.Windows.Input;
using LivestockManagementSystem.Models;
using LivestockManagementSystem.Services;

namespace LivestockManagementSystem.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Animal> Animals { get; set; }
    public readonly Database _database;
    public MainViewModel()
    {
        _database = new();
        Animals = new();
        _database.ReadItems().ForEach(x => Animals.Add(x));

        RefreshCommand = new Command(RefreshAnimals);

    }

    private void RefreshAnimals()
    {
        Animals.Clear();
        _database.ReadItems().ForEach(x => Animals.Add(x));
    }


    public ICommand RefreshCommand { get; set; }


}
