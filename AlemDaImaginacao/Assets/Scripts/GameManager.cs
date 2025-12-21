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

    void Start() {
        GetFaseAtual();
    }

    Fase faseAtual;
    public Fase GetFaseAtual() {
        if (faseAtual != null) return faseAtual;

        faseAtual = FindFirstObjectByType<Fase>();
        return faseAtual;
    }


    public void ResetarFase() {
        faseAtual.ResetarFase();
    }

    

    public Player player {get; protected set;} = null;
    System.Action<Player> _onPlayerSpawn;
    public void HandlePlayerSpawn(Player player) {
        this.player = player;
        _onPlayerSpawn?.Invoke(player);
        _onPlayerSpawn = null;
    }

    public void OnPlayerSpawn(System.Action<Player> onSpawn) {
        if (player != null) onSpawn?.Invoke(player);
        else _onPlayerSpawn += onSpawn;
    }
}
