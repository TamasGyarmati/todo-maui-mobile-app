using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDo.Models;

namespace ToDo.ViewModels;

[QueryProperty(nameof(EditingTodo), "SentFromMainPage")]
public partial class EditPageViewModel : ObservableObject
{
    [ObservableProperty] Todo editingTodo;

    [ObservableProperty] Todo draft;
    
    [ObservableProperty] DateTime date = DateTime.Now;

    public void InitDraft() => Draft = EditingTodo.GetCopy();

    [RelayCommand]
    public async Task SaveEditAsync()
    {
        var param = new ShellNavigationQueryParameters
        {
            { "SentFromEditPage", Draft }
        };

        await Shell.Current.GoToAsync("..", param);
    }

    [RelayCommand]
    public async Task CancelEditAsync() => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    public async Task PickPhotoAsync()
    {
        var image = await MediaPicker.Default.PickPhotoAsync();
        await SaveImageAsync(image);
    }

    async Task SaveImageAsync(FileResult? image)
    {
        try
        {
            if (image != null)
            {
                var url = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
                
                if (!File.Exists(url))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fileStream = File.OpenWrite(url);
                    await stream.CopyToAsync(fileStream);
                }

                Draft.ImageName = image.FileName;
            }
        }
        catch (Exception e)
        {
            WeakReferenceMessenger.Default.Send(e.Message);
        }
    }
}