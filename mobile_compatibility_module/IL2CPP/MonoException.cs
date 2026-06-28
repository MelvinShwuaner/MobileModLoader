extern alias Loader;
using Il2CppInterop.Runtime.Injection;

namespace NeoModLoader.MobileCompatibilityModule;
/// <summary>
/// represents a mono exception for IL2CPP
/// </summary>
[Loader::ModLoader.RegisterTypeInIl2Cpp]
public class MonoException(IntPtr ptr) : Il2CppSystem.Exception(ptr)
{
    private MonoException() : this(ClassInjector.DerivedConstructorPointer<MonoException>())
    {
        ClassInjector.DerivedConstructorBody(this);
    }
    public MonoException(Exception Exception): this()
    {
       exception = Exception;
    }
    Exception exception;
    public override string ToString()
    {
        return exception.ToString();
    }
    public override string Message => exception.Message;
    public override string StackTrace => exception.StackTrace;
    public override string Source => exception.Source;
}