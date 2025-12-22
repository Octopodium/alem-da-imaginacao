using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [Tooltip("Checkpoints com maior prioridade sobrescrevem checkpoints com menor prioridade.")]
    public int prioridadeCheckpoint = 0;
    public Transform checkPoint;

    void OnTriggerEnter(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if (!go.CompareTag("Player")) return;

        Fase fase = GameManager.instance.GetFaseAtual();
        if (fase.CadastrarCheckpoint(checkPoint, prioridadeCheckpoint)) {
            // Trazer todas as ideias de volta (se gastadas)
            GameManager.instance.idealizador.OnResetar();
        }
    }

}
