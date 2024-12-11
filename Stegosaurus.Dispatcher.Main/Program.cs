using Terminal.Gui;

Application.Init();

try
{
    Application.Run(new Connection.Connection());
}
finally
{
    Application.Shutdown();
}