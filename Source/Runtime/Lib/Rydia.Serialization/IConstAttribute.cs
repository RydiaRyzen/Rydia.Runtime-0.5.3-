namespace Rydia.Serialization
{
    internal interface IConstAttribute : IBindableFieldAttribute
    {
        object GetConstValue();
    }
}