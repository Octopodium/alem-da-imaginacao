using UnityEngine;

public class Fase : MonoBehaviour {
    [Tooltip("Colisor utilizado na bounds para a camera de Gameplay")]
    public Collider2D faseBounds;
    public FaseInfo proximaFase;
    public Transform spawnPoint;

    IResetavel[] resetaveis;

    void Start() {
        resetaveis = FindObjectsByType<IResetavel>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        ResetarFase();
    }


    public void ResetarFase() {
        PosicionarJogador();

        foreach (IResetavel r in resetaveis) {
            r.OnResetar();
        }
    }


    public void PosicionarJogador(){
        Player p = Player.instance;
        p.Teletransportar(spawnPoint.position);
        p.gameObject.SetActive(true);
    }
}
