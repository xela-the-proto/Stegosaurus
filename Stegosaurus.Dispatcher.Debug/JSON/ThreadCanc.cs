namespace Stegosaurus.Dispatcher.Debug.JSON;

public class ThreadCanc
{
    public Thread thread { get; set; }
    public CancellationToken CancellationToken { get; set; }
}