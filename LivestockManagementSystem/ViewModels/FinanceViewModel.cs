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
using System.ComponentModel;

namespace LivestockManagementSystem.ViewModels
{
    public class FinanceViewModel : BindableObject
    {
        private ObservableCollection<Animal> _livestock;
        private string _selectedLivestockType;
        private string _selectedLivestockColour;
        private int _totalLivestock;
        private float _percentageOfLivestock;
        private float _profitOrLossPerDay;
        private float _averageWeight;
        private float _governmentTaxPerDay;
        private float _produceAmountPerDay;
        private float _futureInvestment;

        public ObservableCollection<Animal> Livestock
        {
            get => _livestock;
            set
            {
                _livestock = value;
                OnPropertyChanged();
            }
        }

        public string SelectedLivestockType
        {
            get => _selectedLivestockType;
            set
            {
                _selectedLivestockType = value;
                OnPropertyChanged();
                QueryLivestock();
            }
        }

        public string SelectedLivestockColour
        {
            get => _selectedLivestockColour;
            set
            {
                _selectedLivestockColour = value;
                OnPropertyChanged();
                QueryLivestock();
            }
        }

        public int TotalLivestock
        {
            get => _totalLivestock;
            set
            {
                _totalLivestock = value;
                OnPropertyChanged();
            }
        }

        public float PercentageOfLivestock
        {
            get => _percentageOfLivestock;
            set
            {
                _percentageOfLivestock = value;
                OnPropertyChanged();
            }
        }

        public float ProfitOrLossPerDay
        {
            get => _profitOrLossPerDay;
            set
            {
                _profitOrLossPerDay = value;
                OnPropertyChanged();
            }
        }

        public float AverageWeight
        {
            get => _averageWeight;
            set
            {
                _averageWeight = value;
                OnPropertyChanged();
            }
        }

        public float GovernmentTaxPerDay
        {
            get => _governmentTaxPerDay;
            set
            {
                _governmentTaxPerDay = value;
                OnPropertyChanged();
            }
        }

        public float ProduceAmountPerDay
        {
            get => _produceAmountPerDay;
            set
            {
                _produceAmountPerDay = value;
                OnPropertyChanged();
            }
        }

        public float FutureInvestment
        {
            get => _futureInvestment;
            set
            {
                _futureInvestment = value;
                OnPropertyChanged();
            }
        }

        public ICommand QueryLivestockCommand { get; }

        public ICommand CalculateFutureInvestmentCommand { get; }



        private readonly Database _database;

        public FinanceViewModel()
        {
            _database = new Database();
            QueryLivestockCommand = new Command(QueryLivestock);
            CalculateFutureInvestmentCommand = new Command<int>(CalculateFutureInvestment);

        }

        private void QueryLivestock()
        {
            List<Animal> queriedLivestock = _database.ReadItems();

            if (!string.IsNullOrEmpty(SelectedLivestockType))
            {
                queriedLivestock = queriedLivestock.Where(animal => animal.GetType().Name == SelectedLivestockType).ToList();
            }

            if (!string.IsNullOrEmpty(SelectedLivestockColour) && SelectedLivestockColour != "All colours")
            {
                queriedLivestock = queriedLivestock.Where(animal => animal.Colour == SelectedLivestockColour).ToList();
            }

            TotalLivestock = queriedLivestock.Count;
            PercentageOfLivestock = (float)TotalLivestock / _database.ReadItems().Count * 100;

            float totalProfitOrLoss = 0;
            float totalWeight = 0;
            float totalGovernmentTax = 0;
            float totalProduceAmount = 0;

            foreach (Animal animal in queriedLivestock)
            {
                if (animal is Cow cow)
                {
                    float milkAmount =  cow.Milk;
                    float income = milkAmount * 9.4f;
                    float cost = cow.Cost;
                    float tax = cow.Weight * 0.2f;

                    totalProfitOrLoss += income - cost - tax;
                    totalWeight += cow.Weight;
                    totalGovernmentTax += tax;
                    totalProduceAmount += milkAmount;
                }
                else if (animal is Sheep sheep)
                {
                    float woolAmount =  sheep.Wool;
                    float income = woolAmount * 6.2f;
                    float cost = sheep.Cost;
                    float tax = sheep.Weight * 0.2f;

                    totalProfitOrLoss += income - cost - tax;
                    totalWeight += sheep.Weight;
                    totalGovernmentTax += tax;
                    totalProduceAmount += woolAmount;
                }
            }

            ProfitOrLossPerDay = totalProfitOrLoss;
            AverageWeight = totalWeight / TotalLivestock;
            GovernmentTaxPerDay = totalGovernmentTax;
            ProduceAmountPerDay = totalProduceAmount;
        }
        public void CalculateFutureInvestment(int numberOfAnimals)
        {
            string animalType = SelectedLivestockType;
            float futureInvestment = 0;

            if (!string.IsNullOrEmpty(animalType))
            {
                List<Animal> queriedLivestock = _database.ReadItems();

                if (!string.IsNullOrEmpty(animalType))
                {
                    queriedLivestock = queriedLivestock.Where(animal => animal.GetType().Name == SelectedLivestockType).ToList();
                }

                int totalLivestockOfType = queriedLivestock.Count;

                float averageProfitPerDay = 0;
              //  float averageGovernmentTax = 0;

                if (animalType == "Cow")
                {
                    averageProfitPerDay = queriedLivestock.OfType<Cow>().Sum(cow => cow.Milk * 9.4f - cow.Cost - cow.Weight * 0.2f) / totalLivestockOfType;
                 //   averageGovernmentTax = queriedLivestock.OfType<Cow>().Sum(cow => cow.Weight * 0.2f) / totalLivestockOfType;
                }
                else if (animalType == "Sheep")
                {
                    averageProfitPerDay = queriedLivestock.OfType<Sheep>().Sum(sheep => sheep.Wool * 6.2f - sheep.Cost - sheep.Weight * 0.2f) / totalLivestockOfType;
                  //  averageGovernmentTax = queriedLivestock.OfType<Sheep>().Sum(sheep => sheep.Weight * 0.2f) / totalLivestockOfType;
                }
                else if (animalType == "Goat")
                {
                    averageProfitPerDay = queriedLivestock.OfType<Goat>().Sum(goat => goat.Milk * 10f - goat.Cost - goat.Weight * 0.2f) / totalLivestockOfType;
                 //   averageGovernmentTax = queriedLivestock.OfType<Goat>().Sum(goat => goat.Weight * 0.2f) / totalLivestockOfType;
                }

                futureInvestment = averageProfitPerDay * numberOfAnimals;
            }
           FutureInvestment = futureInvestment;

        }

        public class CustomCommand<T1, T2> : ICommand
        {
            private readonly Action<T1, T2> _executeMethod;
            private readonly Func<T1, T2, bool> _canExecuteMethod;

            public CustomCommand(Action<T1, T2> executeMethod)
            {
                _executeMethod = executeMethod;
                _canExecuteMethod = (p1, p2) => true;
            }

            public CustomCommand(Action<T1, T2> executeMethod, Func<T1, T2, bool> canExecuteMethod)
            {
                _executeMethod = executeMethod;
                _canExecuteMethod = canExecuteMethod;
            }

            public bool CanExecute(object? parameter)
            {
                if (_canExecuteMethod != null)
                {
                    return _canExecuteMethod.Invoke((T1)parameter!, (T2)parameter!);
                }
                return true;
            }

            public void Execute(object? parameter)
            {
                _executeMethod?.Invoke((T1)parameter!, (T2)parameter!);
            }

            public event EventHandler? CanExecuteChanged;
        }

    }
}
