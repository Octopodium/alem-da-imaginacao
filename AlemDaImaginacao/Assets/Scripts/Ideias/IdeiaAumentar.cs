using UnityEngine;
using System.Collections;

public class IdeiaAumentar : Ideia {
    public float aumentarEm = 2f;
    public float tempoDeAumento = 2f;
    Vector3 escalaOriginal;

    public override void AplicarEfeito() {
        escalaOriginal = ideavel.transform.localScale;
        StopAllCoroutines();
        StartCoroutine(Escalando());
    }


    IEnumerator Escalando() {
        float i = 0;
        while (i < tempoDeAumento) {
            yield return new WaitForFixedUpdate();
            i += Time.fixedDeltaTime;

            float mult = Mathf.Lerp(1, aumentarEm, i/tempoDeAumento);
            ideavel.transform.localScale = escalaOriginal * mult;
        }
        ideavel.transform.localScale = escalaOriginal * aumentarEm;
    }

    public override void DesaplicarEfeito() {
        StopAllCoroutines();
        ideavel.transform.localScale = escalaOriginal;
    }

}
