namespace Zephyr.Define.Runtime
{
    public interface IDefine
    {
        string GetName();
        T GetComponent<T>() where T : IComponent;
        IComponent[] GetComponents();
    }
}