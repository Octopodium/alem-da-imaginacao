using UnityEngine;
using System.Collections.Generic;

public class IdeiaDesaparecer : Ideia {
    public float alphaApagado;
    float alphaOriginal;
    bool usandoGravidade = false;

    public List<Collider> colisoresAfetados = new List<Collider>();


    Collider[] GetColliders() {
        return ideavel.gameObject.GetComponentsInChildren<Collider>();
    }

    public override void AplicarEfeito() {
        GameObject pai = ideavel.gameObject;
        Renderer rend = pai.GetComponentInChildren<Renderer>();

        Color cor = rend.material.color;
        alphaOriginal = cor.a;

        cor.a = alphaApagado;
        rend.material.color = cor;

        rend.gameObject.SetActive(false);

        colisoresAfetados.Clear();
        foreach (Collider col in GetColliders()) {
            col.enabled = false;
            colisoresAfetados.Add(col);
        }

        usandoGravidade = ideavel.temGravidade;
        ideavel.temGravidade = false;
    }

    public override void DesaplicarEfeito() {
        GameObject pai = ideavel.gameObject;
        Renderer rend = pai.GetComponentInChildren<Renderer>();
        
        Color cor = rend.material.color;
        cor.a = alphaOriginal;
        rend.material.color = cor;

        rend.gameObject.SetActive(true);


        foreach (Collider col in colisoresAfetados) {
            col.enabled = true;
        }

        colisoresAfetados.Clear();

        ideavel.temGravidade = usandoGravidade;
    }

}
