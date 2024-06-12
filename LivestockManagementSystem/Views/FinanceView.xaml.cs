
using LivestockManagementSystem.ViewModels;
using LivestockManagementSystem.Services;

namespace LivestockManagementSystem.Views;

public partial class FinanceView : ContentPage
{
    public FinanceView(FinanceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        QueuryBtn.Clicked += CalcButton_Clicked;
        InvestmentBtn.Clicked += InvestmentBtn_Clicked;
        
       // AvgW.Text = vm.AverageWeight.ToString();
       

    }

    

    private async void CalcButton_Clicked(object sender, EventArgs e)
    {
        string selectedLivestockType = TypeInput.Text;
        string selectedLivestockColour = ColourInput.Text;

        if (selectedLivestockType != "Cow" && selectedLivestockType != "Sheep" && selectedLivestockType != "Goat")
        {
            await DisplayAlert("Error", "Invalid animal type. Please enter 'Cow' or 'Sheep' or 'Goat'.", "OK");
            return;
        }

        if (selectedLivestockColour != "Red" && selectedLivestockColour != "Black" && selectedLivestockColour != "White" && selectedLivestockColour != "All colours")
        {
            await DisplayAlert("Error", "Invalid animal colour. Please enter 'Red', 'Black', 'White', or 'All colours'.", "OK");
            return;
        }

        ((FinanceViewModel)BindingContext).SelectedLivestockType = selectedLivestockType;
        ((FinanceViewModel)BindingContext).SelectedLivestockColour = selectedLivestockColour;
        // Query livestock from ViewModel
        ((FinanceViewModel)BindingContext).QueryLivestockCommand.Execute(null);
        NumberOfLiveStocks.Text = ((FinanceViewModel)BindingContext).TotalLivestock.ToString();
        GovernmentTaxPerDay.Text= ((FinanceViewModel)BindingContext).GovernmentTaxPerDay.ToString() +"$";
        ProfitOrLossPerDay.Text = ((FinanceViewModel)BindingContext).ProfitOrLossPerDay.ToString() +"$";
        ProduceAmountPerDay.Text = ((FinanceViewModel)BindingContext).ProduceAmountPerDay.ToString() +"kg";
        AverageWeight.Text = ((FinanceViewModel)BindingContext).AverageWeight.ToString() +"kg";
        PercentageOfLivestock.Text = ((FinanceViewModel)BindingContext).PercentageOfLivestock.ToString() +"%";
    }

    private async void InvestmentBtn_Clicked(object? sender, EventArgs e)
    {
        if (TypeInput1.Text != "Cow" && TypeInput1.Text != "Sheep" && TypeInput1.Text != "Goat")
        {
            await DisplayAlert("Error", "Invalid animal type. Please enter 'Cow' or 'Sheep' or 'Goat'.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(AnimalAmout.Text))
        {
            await DisplayAlert("Error", "Animal amount cannot be empty.", "OK");
            return;
        }

        if (!int.TryParse(AnimalAmout.Text, out int numOfAnimals))
        {
            await DisplayAlert("Error", "Invalid animal amount. Please enter a valid integer.", "OK");
            return;
        }

        ((FinanceViewModel)BindingContext).SelectedLivestockType = TypeInput1.Text;
        ((FinanceViewModel)BindingContext).CalculateFutureInvestmentCommand.Execute(numOfAnimals);
        FutureInvestment.Text = ((FinanceViewModel)BindingContext).FutureInvestment.ToString() + "$";
    }


}
