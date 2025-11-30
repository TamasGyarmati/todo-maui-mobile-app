using CommunityToolkit.Mvvm.Messaging;
using ToDo.ViewModels;

namespace ToDo;

public partial class MainPage : ContentPage
{
    MainPageViewModel _vm;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
        
        WeakReferenceMessenger.Default.Register<string>(this, async (_, m) =>
        {
            await DisplayAlert("Message", m, "OK");
        });
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.InitAsync();
    }
}