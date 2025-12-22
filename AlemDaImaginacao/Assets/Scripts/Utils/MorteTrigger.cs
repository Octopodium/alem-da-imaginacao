using UnityEngine;

public class MorteTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if (!go.CompareTag("Player")) return;

        GameManager.instance.ResetarFase();
    }
}
