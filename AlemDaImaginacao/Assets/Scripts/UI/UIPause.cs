using UnityEngine;
using UnityEngine.UI;
public class UIPause : MonoBehaviour {
    public GameObject painelPause;

    void Start () {
        GameManager.instance.OnPauseChange += HandlePauseChange;
    }

    void HandlePauseChange() {
        if (Time.timeScale == 0) painelPause.SetActive(true);
        else painelPause.SetActive(false);
    }

    public void Continuar() {
        GameManager.instance.Despausar();
    }

    public void VoltarAoMenu() {
        GameManager.instance.VoltarAoMenu();
    }

    public void Resetar() {
        GameManager.instance.RecarregarFase();
    }
}
