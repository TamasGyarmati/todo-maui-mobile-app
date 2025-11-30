namespace ToDo;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("editTodo", typeof(EditTodoPage));
        Routing.RegisterRoute("addTodo", typeof(AddTodoPage));
    }
}