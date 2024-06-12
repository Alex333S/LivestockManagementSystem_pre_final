using LivestockManagementSystem.ViewModels;

namespace LivestockManagementSystem.Views;

public partial class ManagementView : ContentPage
{
    private readonly ManagementViewModel _viewModel;

    public ManagementView(ManagementViewModel vmm)
    {
        InitializeComponent();
        BindingContext = vmm;
        _viewModel = vmm;
        UpdateEntryPlaceholder();
    }

    private void UpdateEntryPlaceholder()
    {
        if (_viewModel.InputText?.ToLower() == "goat" || _viewModel.InputText?.ToLower() == "cow")
        {
            MilkEntry.Placeholder = "Milk";
        }
        else if (_viewModel.InputText?.ToLower() == "sheep")
        {
            WoolEntry.Placeholder = "Wool";
        }
    }
}
