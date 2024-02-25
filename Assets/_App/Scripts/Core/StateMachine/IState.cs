using System.Threading.Tasks;

public interface IState
{
    public Task Enter();
    public Task Exit();
}
