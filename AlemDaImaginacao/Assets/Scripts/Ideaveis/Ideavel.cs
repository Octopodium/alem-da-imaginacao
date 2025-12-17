using UnityEngine;
using System.Collections.Generic;

public class Ideavel : MonoBehaviour {
    public Rigidbody rb {get; protected set;}
    List<Ideia> ideias = new List<Ideia>();



    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void AtivarPrevisaoDeIdeia() {
        Debug.Log("aaaa");
    }

    public void DesativarPrevisaoDeIdeia() {
        Debug.Log("bbbb");
    }

    public void AplicarIdeia(IdeiaInfo ideiaInfo) {
        GameObject efeito = Instantiate(ideiaInfo.efeitoPrefab);
        efeito.transform.SetParent(transform);
        
        Ideia ideia = efeito.GetComponent<Ideia>();
        ideia.info = ideiaInfo;
        ideia.ideavel = this;
        ideias.Add(ideia);

        ideia.AplicarEfeito();
    }

    public void Resetar() {
        foreach (Ideia ideia in ideias) {
            ideia.DesaplicarEfeito();
        }

        ideias.Clear();
    }


}
