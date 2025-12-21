using UnityEngine;
using System.Collections;

public class LimitadorDeQueda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        /*
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            if (player != null) player.MudarVida(-99, fonteDeDano);
        }
        */

        Destrutivel destrutivel = other.GetComponent<Destrutivel>();
        if(destrutivel){
            destrutivel.Destruir();
        }
    }
}
