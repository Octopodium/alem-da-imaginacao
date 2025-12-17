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
}
