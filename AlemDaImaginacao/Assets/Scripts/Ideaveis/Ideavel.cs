using UnityEngine;

public class Ideavel : MonoBehaviour {
    public Rigidbody rb {get; protected set;}


    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void AtivarPrevisaoDeIdeia() {
        Debug.Log("aaaa");
    }

    public void DesativarPrevisaoDeIdeia() {
        Debug.Log("bbbb");
    }

    public void AplicarIdeia() {
        Debug.Log("aaaa");
    }
}
