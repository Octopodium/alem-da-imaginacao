using UnityEngine;

public class IdeiaCor : Ideia {
    Color corAntiga;

    public override void AplicarEfeito() {
        GameObject pai = ideavel.gameObject;
        Renderer rend = pai.GetComponentInChildren<Renderer>();

        corAntiga = rend.material.color;
        rend.material.color = info.cor;
    }

    public override void DesaplicarEfeito() {
        GameObject pai = ideavel.gameObject;
        Renderer rend = pai.GetComponentInChildren<Renderer>();
        
        rend.material.color = corAntiga;
    }

}
