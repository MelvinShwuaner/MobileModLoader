using System.Reflection;

namespace Il2CppInterop.Runtime.Attributes
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property |
                    AttributeTargets.Event)]
    public class HideFromIl2CppAttribute : Attribute
    {
    }
}

namespace Il2CppInterop.Runtime.Injection
{
    public class ClassInjector
    {
        public static nint DerivedConstructorPointer<T>()
        {
            return 67;
        }

        public static void DerivedConstructorBody(object obj)
        {
            
        }
    }
}
[AttributeUsage(AttributeTargets.Class)]
public class RegisterTypeInIl2Cpp : Attribute
{
    public static void RegisterAssembly(Assembly assembly)
    {
    }
}