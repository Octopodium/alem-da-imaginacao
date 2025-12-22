using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public string menuScenePath;

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

        DontDestroyOnLoad(gameObject);
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



    public void PassarDeFase() {
        StartCoroutine(PassarDeFaseCoroutine());
    }

    IEnumerator PassarDeFaseCoroutine() {
        UIController.instance.FadeOut();
        yield return new WaitForSeconds(3f);

        Fase faseAtual = GetFaseAtual();
        FaseInfo proximaFaseInfo = faseAtual.proximaFase;

        if (proximaFaseInfo == null || proximaFaseInfo.cenaPath == "") {
            Debug.LogWarning("Nao ha proxima fase definida na fase " + faseAtual.name);
            Destroy(gameObject);
            SceneManager.LoadScene(menuScenePath);
            yield break;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(proximaFaseInfo.cenaPath);
        while (!asyncOperation.isDone) {
            yield return null;
        }

        Fase proximaFase = GetFaseAtual();

        UIController.instance.FadeIn();
    }
}
