using LivestockManagementSystem.ViewModels;

namespace LivestockManagementSystem.Views;

public partial class DashboardView : ContentPage
{
    public DashboardView(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        var RefreshButton = new Button();
        RefreshButton.Text = "Refresh";
        RefreshButton.Clicked += OnRefreshClicked;

        bb.Children.Add(RefreshButton);

    }
    // On refresh clicked
    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        ((MainViewModel)BindingContext).RefreshCommand.Execute(null);
        
        await Application.Current.MainPage.Navigation.PopAsync();
        await Application.Current.MainPage.Navigation.PushAsync(new DashboardView((MainViewModel)BindingContext));
        
    }
}



