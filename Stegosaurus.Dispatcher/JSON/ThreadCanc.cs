namespace Stegosaurus.Dispatcher.JSON;

public class ThreadCanc
{
    public Thread thread { get; set; }
    public CancellationToken CancellationToken { get; set; }
}