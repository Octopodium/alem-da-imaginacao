using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public string menuScenePath;

    public Actions actions;

    public Idealizador idealizador;


    public System.Action OnPauseChange;


    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        actions = new Actions();
        actions.Enable();

        actions.UI.Pause.performed += ctx => {
            TogglePause();
        };

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
        Despausar();

        UIController.instance.FadeOut();
        yield return new WaitForSeconds(3f);

        Fase faseAtual = GetFaseAtual();
        FaseInfo proximaFaseInfo = faseAtual.proximaFase;

        Despausar();

        if (proximaFaseInfo == null || proximaFaseInfo.cenaPath == "") {
            Debug.LogWarning("Nao ha proxima fase definida na fase " + faseAtual.name);
            VoltarAoMenu();
            yield break;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(proximaFaseInfo.cenaPath);
        while (!asyncOperation.isDone) {
            yield return null;
        }

        Fase proximaFase = GetFaseAtual();

        UIController.instance.FadeIn();
    }


    public void Pausar() {
        Time.timeScale = 0f;
        OnPauseChange?.Invoke();
    }

    public void Despausar() {
        Time.timeScale = 1f;
        OnPauseChange?.Invoke();
    }

    public void TogglePause() {
        if (Time.timeScale == 0f) {
            Despausar();
        } else {
            Pausar();
        }
    }

    public void VoltarAoMenu() {
        SceneManager.LoadScene(menuScenePath);
        Destroy(gameObject);
    }

    public void RecarregarFase() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
