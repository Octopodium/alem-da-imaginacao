using UnityEngine;

public class IdeiaInverter : Ideia {
    float direcaoOriginal;

    public override void AplicarEfeito() {
        direcaoOriginal = ideavel.direcaoGravidade;
        ideavel.direcaoGravidade *= -1;
    }

    public override void DesaplicarEfeito() {
        ideavel.direcaoGravidade = direcaoOriginal;
    }

}
