using UnityEngine;
using System.Collections.Generic;

public class Ideavel : IResetavel, IRecebeTemplate {
    public Rigidbody rb {get; protected set;}

    List<Ideia> ideias = new List<Ideia>();

    [HideInInspector] public bool temGravidade = false;
    public float direcaoGravidade = 1f;



    public void RecebeTemplate(GameObject template) {
        transform.localScale = template.transform.localScale;
        transform.rotation = template.transform.rotation;

        Ideavel ideavelTemplate = template.GetComponent<Ideavel>();

        temGravidade = ideavelTemplate.temGravidade;

        rb.isKinematic = ideavelTemplate.rb.isKinematic;
        rb.mass = ideavelTemplate.rb.mass;

        direcaoGravidade = ideavelTemplate.direcaoGravidade;
    }



    void Awake() {
        rb = GetComponent<Rigidbody>();
        temGravidade = rb.useGravity;
        rb.useGravity = false;
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

    public override void OnResetar() {
        foreach (Ideia ideia in ideias) {
            ideia.DesaplicarEfeito();
        }

        ideias.Clear();
    }

}
