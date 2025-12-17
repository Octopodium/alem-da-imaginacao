using UnityEngine;

public abstract class Ideia : MonoBehaviour {
    public IdeiaInfo info;
    public Ideavel ideavel;

    public abstract void AplicarEfeito();
    public abstract void DesaplicarEfeito();
}
