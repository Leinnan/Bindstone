namespace Bindstone.Binding
{
    public interface IMemberBinding
    {
        void Connect();
        void Disconnect();
        void Init();
    }
}