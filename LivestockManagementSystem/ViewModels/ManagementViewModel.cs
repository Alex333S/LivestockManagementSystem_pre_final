using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LivestockManagementSystem.Models;
using LivestockManagementSystem.Services;
using System.Security.Cryptography.X509Certificates;

namespace LivestockManagementSystem.ViewModels
{
    // ViewModel
    public class ManagementViewModel : BindableObject
    {
        private string _inputText;
        private string _updatedDataText;
        private ObservableCollection<Animal> _animals;
        private readonly Database _database;

        public float AnimalWoolOrMilk { get; set; }
        
        public string AnimalColour { get; set; }
        public float AnimalCost { get; set; }

        public float AnimalWeight { get; set; }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public string UpdatedDataText
        {
            get => _updatedDataText;
            set
            {
                _updatedDataText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Animal> Animals
        {
            get => _animals;
            set
            {
                _animals = value;
                OnPropertyChanged();
            }
        }

        public ICommand InsertCommand {get;}
        public ICommand DeleteCommand {get;}
        public ICommand UpdateCommand {get;}

        public ManagementViewModel()
        {
            _database = new Database();
            Animals = new ObservableCollection<Animal>(_database.ReadItems());
            InsertCommand = new Command(Insert);
            DeleteCommand = new Command<int>(Delete);
            UpdateCommand = new Command<int>(Update);
        }

        private void Insert()
        {
            // Check if InputText is null
            if (string.IsNullOrEmpty(InputText))
            {
                
                return;
            }
            // Insert logic
            string animalType = InputText.ToLower();
            Animal newAnimal;
            switch (animalType)
            {
                case "cow":
                    newAnimal = new Cow();
                    newAnimal.Id = _database.GetNextCowId();
                    ((Cow)newAnimal).Milk = AnimalWoolOrMilk;
                    newAnimal.Colour = AnimalColour;
                    newAnimal.Cost = AnimalCost;
                    newAnimal.Weight = AnimalWeight;
                    break;
                case "sheep":
                    newAnimal = new Sheep();
                    newAnimal.Id = _database.GetNextSheepId();
                    ((Sheep)newAnimal).Wool = AnimalWoolOrMilk;
                    newAnimal.Colour = AnimalColour;
                    newAnimal.Cost = AnimalCost;
                    newAnimal.Weight = AnimalWeight;
                    break;
                case "goat":
                    newAnimal = new Goat();
                    newAnimal.Id = _database.GetNextGoatId();
                    ((Goat)newAnimal).Milk = AnimalWoolOrMilk;
                    newAnimal.Colour = AnimalColour;
                    newAnimal.Cost = AnimalCost;
                    newAnimal.Weight = AnimalWeight;
                    break;
                default:
                    // Invalid animal type
                    return;
            }

            _database.InsertItem(newAnimal);
            Animals.Add(newAnimal);
        }

        private void Delete(int id)
        {
            Animal animalToDelete = Animals.FirstOrDefault(a => a.Id == id);
            if (animalToDelete != null)
            {
                _database.DeleteItem(animalToDelete);
                Animals.Remove(animalToDelete);
            }
        }

        private void Update(int id)
        {
            if (string.IsNullOrEmpty(InputText))
            {
                
                return;
            }
           // id= int.Parse(InputText);

            Animal animalToUpdate = Animals.First(a => a.Id == id);
            if (animalToUpdate != null)
            {
                animalToUpdate.Cost = AnimalCost;
                animalToUpdate.Weight = AnimalWeight;
                if (animalToUpdate is Cow cow)
                {
                    cow.Milk = AnimalWoolOrMilk;
                }
                else if (animalToUpdate is Sheep sheep)
                {
                    sheep.Wool = AnimalWoolOrMilk;
                }
                else if (animalToUpdate is Goat goat)
                {
                    goat.Milk = AnimalWoolOrMilk;
                }
                animalToUpdate.Colour = AnimalColour;
                _database.UpdateItem(animalToUpdate);
            }
        }

        
    }
}
