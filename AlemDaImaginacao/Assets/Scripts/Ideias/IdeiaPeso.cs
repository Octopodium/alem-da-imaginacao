using UnityEngine;

public class IdeiaPeso : Ideia {
    public float multPeso = 2.0f;
    float pesoOriginal;

    public override void AplicarEfeito() {
        Rigidbody rb = ideavel.rb;
        pesoOriginal = rb.mass;
        rb.mass *= multPeso;
    }

    public override void DesaplicarEfeito() {
        Rigidbody rb = ideavel.rb;
        rb.mass = pesoOriginal;
    }

}
