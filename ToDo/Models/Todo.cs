using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ToDo.Models;

public partial class Todo : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ObservableProperty]
    string title;
    
    [ObservableProperty]
    string description;
    
    [ObservableProperty]
    DateTime created = DateTime.Now;
    
    [ObservableProperty] // kell converter majd
    DateTime deadline;
    
    [ObservableProperty]
    string imageName;

    public string? ImageUrl
    {
        get
        {
            if (ImageName == null)
                return null;

            return Path.Combine(FileSystem.Current.AppDataDirectory, ImageName);
        }
    }

    partial void OnImageNameChanged(string value) => OnPropertyChanged(nameof(ImageUrl));

    public override string ToString() => $"Todo name: {Title}, Description: {Description}, Created: {Created}, Deadline: {Deadline}";

    public Todo GetCopy() => (Todo)MemberwiseClone();
}