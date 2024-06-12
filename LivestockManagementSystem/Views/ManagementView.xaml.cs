using LivestockManagementSystem.Services;
using LivestockManagementSystem.ViewModels;
//using Xamarin.Forms;

namespace LivestockManagementSystem.Views;

public partial class ManagementView : ContentPage
{
    public ManagementView(ManagementViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        var InsertButton = new Button();
        InsertButton.SetBinding(Button.CommandProperty, new Binding(nameof(vm.InsertCommand)));
        InsertButton.Text = "Insert";
        InsertButton.Clicked += OnInsertClicked;
        //   InsertButton.BackgroundColor = Color.LightCoral; // Set the background color of the button
        InsertButton.CornerRadius = 5; 

        var DeleteButton = new Button();
        DeleteButton.Text = "Delete";
        DeleteButton.Clicked += OnDeleteClicked;
        //    DeleteButton.BackgroundColor = Color.LightCoral; // Set the background color of the button
        DeleteButton.CornerRadius = 5; 

        var UpdateButton = new Button();
        UpdateButton.Text = "Update";
        UpdateButton.Clicked += OnUpdateClicked;
        //    UpdateButton.BackgroundColor = Color.LightGreen; // Set the background color of the button
        UpdateButton.CornerRadius = 5; 

        // Add the DeleteButton
        bl.Children.Add(DeleteButton);
        bl.HorizontalOptions = LayoutOptions.Center;

        // Add the InsertButton
        bl2.Children.Add(InsertButton);
        bl2.HorizontalOptions = LayoutOptions.Center;

        // Add the UpdateButton
        bl3.Children.Add(UpdateButton);
        bl3.HorizontalOptions = LayoutOptions.Center;
    }

    //On delete clicked. enter ID
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        string result = await DisplayPromptAsync("Delete Animal", "Enter the ID of the animal to delete:");

        if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int id))
        {
            // Check if the ID is present in the database
            bool idExists = CheckIfIdExistsInDatabase(id);
            if (idExists)
            {
                ((ManagementViewModel)BindingContext).DeleteCommand.Execute(id);
            }
            else
            {
                await DisplayAlert("Invalid Input", "The provided ID does not exist in the database.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please enter a valid integer ID.", "OK");
        }
    }

    private async void OnInsertClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        string result = await DisplayPromptAsync("Insert Animal", "Enter the type of animal to Insert:");

        if (!string.IsNullOrEmpty(result) && (result == "Cow" || result == "Sheep" || result == "Goat"))
        {
            // Prompt for animal properties
            string cost = await DisplayPromptAsync("Animal Properties", "Enter the cost of the animal:");
            float costFloat;
            if (float.TryParse(cost, out costFloat) && costFloat > 0)
            {
                string weight = await DisplayPromptAsync("Animal Properties", "Enter the weight of the animal:");
                float weightFloat;
                if (float.TryParse(weight, out weightFloat) && weightFloat > 0)
                {
                    string colour = await DisplayPromptAsync("Animal Properties", "Enter the colour of the animal:");
                    if (colour == "Red" || colour == "Black" || colour == "White")
                    {
                        string woolOrMilk = await DisplayPromptAsync("Animal Properties", "Enter the wool/milk production of the animal:");
                        float woolOrMilkFloat = float.Parse(woolOrMilk);

                        // Pass the animal properties to the view model
                        ((ManagementViewModel)BindingContext).AnimalCost = costFloat;
                        ((ManagementViewModel)BindingContext).AnimalWeight = weightFloat;
                        ((ManagementViewModel)BindingContext).AnimalColour = colour;
                        ((ManagementViewModel)BindingContext).AnimalWoolOrMilk = woolOrMilkFloat;

                        ((ManagementViewModel)BindingContext).InputText = result;
                        ((ManagementViewModel)BindingContext).InsertCommand.Execute(null);
                    }
                    else
                    {
                        await DisplayAlert("Invalid Input", "Please enter a valid colour (Red, Black, White).", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter a valid positive weight value.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please enter a valid positive cost value.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please enter a valid animal type (Cow, Sheep, Goat).", "OK");
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        string result = await DisplayPromptAsync("Update Animal", "Enter the ID of the animal to update:");

        if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int id))
        {
            // Check if the ID is present in the database
            bool idExists = CheckIfIdExistsInDatabase(id);
            if (idExists)
            {
                // Prompt for animal properties
                string cost = await DisplayPromptAsync("Animal Properties", "Enter the cost of the animal:");
                float costFloat;
                if (float.TryParse(cost, out costFloat) && costFloat > 0)
                {
                    string weight = await DisplayPromptAsync("Animal Properties", "Enter the weight of the animal:");
                    float weightFloat;
                    if (float.TryParse(weight, out weightFloat) && weightFloat > 0)
                    {
                        string colour = await DisplayPromptAsync("Animal Properties", "Enter the colour of the animal:");
                        if (colour == "Red" || colour == "Black" || colour == "White")
                        {
                            string woolOrMilk = await DisplayPromptAsync("Animal Properties", "Enter the wool/milk production of the animal:");
                            float woolOrMilkFloat;
                            if (float.TryParse(woolOrMilk, out woolOrMilkFloat))
                            {
                                // Pass the animal properties to the view model
                                ((ManagementViewModel)BindingContext).AnimalCost = costFloat;
                                ((ManagementViewModel)BindingContext).AnimalWeight = weightFloat;
                                ((ManagementViewModel)BindingContext).AnimalColour = colour;
                                ((ManagementViewModel)BindingContext).AnimalWoolOrMilk = woolOrMilkFloat;

                                ((ManagementViewModel)BindingContext).InputText = result;
                                ((ManagementViewModel)BindingContext).UpdateCommand.Execute(id);
                            }
                            else
                            {
                                await DisplayAlert("Invalid Input", "Please enter a valid wool/milk production value.", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Invalid Input", "Please enter a valid colour (Red, Black, White).", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Invalid Input", "Please enter a valid positive weight value.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter a valid positive cost value.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid Input", "The provided ID does not exist in the database.", "OK");
            }

        }
        else
        {
            await DisplayAlert("Invalid Input", "Please enter a valid integer ID.", "OK");
        }
    }

    private bool CheckIfIdExistsInDatabase(int id)
    {
        var animals = ((ManagementViewModel)BindingContext).Animals;
        return animals.Any(a => a.Id == id);
    }
}
