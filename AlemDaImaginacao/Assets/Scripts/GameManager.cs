using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Actions actions;

    public Idealizador idealizador;


    


    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        actions = new Actions();
        actions.Enable();
    }

    Fase faseAtual;
    public Fase GetFaseAtual() {
        if (faseAtual != null) return faseAtual;

        faseAtual = FindFirstObjectByType<Fase>();
        return faseAtual;
    }

    

    public PlayerMovement player {get; protected set;} = null;
    System.Action<PlayerMovement> _onPlayerSpawn;
    public void HandlePlayerSpawn(PlayerMovement player) {
        this.player = player;
        _onPlayerSpawn?.Invoke(player);
        _onPlayerSpawn = null;
    }

    public void OnPlayerSpawn(System.Action<PlayerMovement> onSpawn) {
        if (player != null) onSpawn?.Invoke(player);
        else _onPlayerSpawn += onSpawn;
    }
}
