using UnityEngine;
using System.Collections.Generic;

public class Ideavel : MonoBehaviour {
    public Rigidbody rb {get; protected set;}

    List<Ideia> ideias = new List<Ideia>();

    public bool temGravidade = false;
    public float direcaoGravidade = 1f;



    void Awake() {
        rb = GetComponent<Rigidbody>();
        temGravidade = rb.useGravity;
        rb.useGravity = false;
        direcaoGravidade = 1f;
    }


    void FixedUpdate() {
        if (!temGravidade) return;
        Vector3 gravidade = Physics.gravity * direcaoGravidade;
        rb.AddForce(gravidade, ForceMode.Acceleration);
    }



    public void AtivarPrevisaoDeIdeia() {
        Debug.Log("Ta em cima de mim! " + gameObject.name);
    }

    public void DesativarPrevisaoDeIdeia() {
        Debug.Log("Saiu de cima de mim! " + gameObject.name);
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
