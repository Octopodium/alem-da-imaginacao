using UnityEngine;

public class ImaginadorTrigger : MonoBehaviour {
    public IdeiaInfo ideia;

    void OnTriggerEnter(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if (!go.CompareTag("Player")) return;

        GameManager.instance.idealizador.AdquirirIdeia(ideia);
    }

}
