using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour {
    public UnityEvent<GameObject> onTriggerEnter, onTriggerExit, onTriggerStay;
    public System.Action<GameObject> onTriggerEnterAction, onTriggerExitAction, onTriggerStayAction;
    public string tagFilter;

    private void OnTriggerEnter(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if(string.IsNullOrEmpty(tagFilter) || go.CompareTag(tagFilter)) {
            onTriggerEnter?.Invoke(go);
            onTriggerEnterAction?.Invoke(go);
        }
    }

    private void OnTriggerExit(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if(string.IsNullOrEmpty(tagFilter) || go.CompareTag(tagFilter)) {
            onTriggerExit?.Invoke(go);
            onTriggerExitAction?.Invoke(go);
        }
    }

    private void OnTriggerStay(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if(string.IsNullOrEmpty(tagFilter) || go.CompareTag(tagFilter)) {
            onTriggerStay?.Invoke(go);
            onTriggerStayAction?.Invoke(go);
        }
    }
}
