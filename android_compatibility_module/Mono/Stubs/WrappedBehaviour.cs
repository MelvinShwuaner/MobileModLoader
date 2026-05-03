
using UnityEngine;
namespace NeoModLoader.AndroidCompatibilityModule;
public class WrappedBehaviour : MonoBehaviour{
 public MonoBehaviour Wrapper => this;

 public void DontResolveSelf()
 {
  throw new PlatformNotSupportedException();
 }
}