using UnityEngine;
using UnityEngine.InputSystem;

public class Idealizador : MonoBehaviour {
    public LayerMask camadaDeIdealizacao;
    public float raycastDist = 100f;
    Camera mainCam;
    Vector3 mousePos;
    IdeiaUI ideiaAtual;


    Ideavel ultimoIdeavelImaginado = null;
    Ideavel ideavelCached = null;


    public void ComecarAImaginar(IdeiaUI ideia) {
        ideiaAtual = ideia;
    }

    public bool Imaginando(IdeiaUI ideia, Vector3 mousePos) {
        Ideavel ideavel = PegarIdeavelEm(mousePos);

        if (ultimoIdeavelImaginado != null && ultimoIdeavelImaginado != ideavel) {
            ultimoIdeavelImaginado.DesativarPrevisaoDeIdeia();
            ultimoIdeavelImaginado = null;
        }

        ultimoIdeavelImaginado = ideavel;

        if (ideavel != null) {
            ideavel.AtivarPrevisaoDeIdeia();
            return true;
        }

        return false;
    }

    public void EncerrarImaginacao(IdeiaUI ideia, Vector3 mousePos) {
        if (ultimoIdeavelImaginado != null) {
            ultimoIdeavelImaginado.DesativarPrevisaoDeIdeia();
            ultimoIdeavelImaginado = null;
        }

        Ideavel ideavel = PegarIdeavelEm(mousePos);
        if (ideavel != null) {
            ideavel.AplicarIdeia();
            ideia.Gastar();
        } else {
            ideia.Resetar();
        }

        ideiaAtual = null;
    }

    protected Ideavel PegarIdeavelEm(Vector3 mousePos) {
        mainCam = Camera.main;
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDist, camadaDeIdealizacao)) {
            GameObject objeto = hit.collider.gameObject;

            if (ideavelCached != null && ideavelCached.gameObject == objeto)
                return ideavelCached;

            Ideavel ideavel = objeto.GetComponent<Ideavel>();
            if (ideavel != null) {
                ideavelCached = ideavel;
                return ideavelCached;
            }
        }

        ideavelCached = null;

        return null;
    }


}