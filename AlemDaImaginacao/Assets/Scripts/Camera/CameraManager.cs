using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour {
    public static CameraManager instance;

    public int prioridadeMaior = 99;
    public CinemachineCamera cameraAtual;
    public CinemachineCamera cameraDeGameplay;

    void Awake() {
        instance = this;
    }

    void Start() {
        if (cameraAtual != null) MudarCamera(cameraAtual, false);

        Fase fase = GameManager.instance.GetFaseAtual();
        if (fase != null) SetGameplayBounds(fase.faseBounds);

        GameManager.instance.OnPlayerSpawn(SetPlayerFollow);
    }

    public void MudarCamera(CinemachineCamera camera, bool dontOverwriteSelf = true) {
        if (dontOverwriteSelf && camera == cameraAtual) return;

        if (cameraAtual != null) cameraAtual.Priority = -1;
        cameraAtual = camera;
        cameraAtual.Priority = prioridadeMaior;
    }

    public void SetGameplayBounds(Collider2D bounds) {
        CinemachineConfiner2D confiner = cameraDeGameplay.GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = bounds;
        confiner.InvalidateBoundingShapeCache();
    }


    public void SetPlayerFollow(Player player) {
        cameraDeGameplay.Follow = player.frente;
    }
}
