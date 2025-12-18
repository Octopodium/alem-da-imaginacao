using UnityEngine;
using Unity.Cinemachine;

public enum Eixo { Horizontal, Vertical }

public class AlteraCameraTrigger : MonoBehaviour {
    public Eixo direcaoEntrada = Eixo.Horizontal;

    [Tooltip("Camera da esquerda ou de cima")]
    public CinemachineCamera cam1;
    [Tooltip("Camera da direita ou de baixo")]
    public CinemachineCamera cam2;


    void OnTriggerExit(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if (!go.CompareTag("Player")) return;

        float dir = 0;
        if (direcaoEntrada == Eixo.Horizontal) dir = go.transform.position.x - transform.position.x;
        else dir = transform.position.y - go.transform.position.y;

        if (dir < 0) CameraManager.instance.MudarCamera(cam1);
        else CameraManager.instance.MudarCamera(cam2);
    }

}
