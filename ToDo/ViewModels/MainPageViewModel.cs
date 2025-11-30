using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDo.Database;
using ToDo.Models;

namespace ToDo.ViewModels;

[QueryProperty(nameof(EditedTodo),"SentFromEditPage")]
public partial class MainPageViewModel : ObservableObject
{
    IDatabase<Todo> database;
    public ObservableCollection<Todo> Todos { get; set; }
    
    [ObservableProperty]
    Todo selectedTodo;
    
    [ObservableProperty]
    Todo editedTodo;

    public MainPageViewModel()
    {
        Todos = new ObservableCollection<Todo>();
        database = new SQLiteDatabase();
    }

    public async Task InitAsync()
    {
        var todos = await database.GetAllTodoAsync();
        Todos.Clear();
        todos.ForEach(t => Todos.Add(t));
        Todos.Add(new Todo { Title = "Modular programming", Description = "Do it till monday", Deadline = new DateTime(2025, 11, 30), ImageName = "kopasz.JPG"});
        Todos.Add(new Todo { Title = "Concert", Description = "Hell yeah", Deadline = new DateTime(2025, 12, 13), ImageName = "haze.jpg"});
    }
    
    async partial void OnEditedTodoChanged(Todo value)
    {
        if (value != null)
        {
            if (SelectedTodo != null)
            {
                Todos.Remove(value);
                SelectedTodo = null;
                await database.UpdateTodoAsync(value);
            }
            else
            {
                await database.CreateTodoAsync(value);
            }
            Todos.Add(value);
        }
        else
        {
            WeakReferenceMessenger.Default.Send("Value doesn't exist");
        }
    }

    [RelayCommand]
    public async Task AddTodoAsync()
    {
        SelectedTodo = null;

        var param = new ShellNavigationQueryParameters
        {
            { "SentFromMainPage", new Todo() }
        };

        await Shell.Current.GoToAsync("editTodo", param);
    }
    
    [RelayCommand]
    public async Task EditTodoAsync()
    {
        if (SelectedTodo != null)
        {
            var param = new ShellNavigationQueryParameters
            {
                { "SentFromMainPage", SelectedTodo }
            };

            await Shell.Current.GoToAsync("editTodo", param);   
        }
        else
            WeakReferenceMessenger.Default.Send("You must select a Todo first!");
    }
    
    [RelayCommand]
    public async Task DeleteTodoAsync()
    {
        if (SelectedTodo != null)
        {
            Todos.Remove(SelectedTodo);
            await database.DeleteTodoAsync(SelectedTodo);
            SelectedTodo = null;
        }
        else
            WeakReferenceMessenger.Default.Send("You must select a Todo first!");
    }

    [RelayCommand]
    public async Task ShareTodoAsync()
    {
        if (SelectedTodo != null)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Title = "New Todo Share",
                    Text = SelectedTodo.ToString()
                });
            }
            else
                WeakReferenceMessenger.Default.Send("No connection to Internet");
        }
        else
            WeakReferenceMessenger.Default.Send("Select a Todo first!");
    }
}