using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Idealizador : MonoBehaviour {
    public bool idealizando { get{ return ideiaAtual != null;}}
    public System.Action<IdeiaInfo> OnStartIdealizacao;
    public System.Action<IdeiaInfo> OnEndIdealizacao;


    public LayerMask camadaDeIdealizacao;
    public float raycastDist = 100f;
    Camera mainCam;
    Vector3 mousePos;
    IdeiaUI ideiaAtual;


    Ideavel ultimoIdeavelImaginado = null;
    Ideavel ideavelCached = null;

    public List<IdeiaInfo> ideiasPossuidas = new List<IdeiaInfo>();

    [Header("Idealizador UI")]
    public GameObject ideiaUIPrefab;
    public Transform ideiasHolder;

    void Awake() {
        Setup();
    }

    public void Setup() {
        foreach (Transform fi in ideiasHolder) {
            Destroy(fi.gameObject);
        }

        foreach (IdeiaInfo ideia in ideiasPossuidas) {
            AdquirirIdeia(ideia, false);
        }
    }


    public void ComecarAImaginar(IdeiaUI ideia) {
        ideiaAtual = ideia;
        OnStartIdealizacao?.Invoke(ideia.ideiaInfo);
    }

    public bool Imaginando(IdeiaUI ideia, Vector3 mousePos) {
        Ideavel ideavel = PegarIdeavelEm(mousePos);

        if (ultimoIdeavelImaginado != null && ultimoIdeavelImaginado != ideavel) {
            ultimoIdeavelImaginado.DesativarPrevisaoDeIdeia();
            ultimoIdeavelImaginado = null;
        }


        if (ideavel != null) {
            if (ideavel != ultimoIdeavelImaginado) ideavel.AtivarPrevisaoDeIdeia();
            ultimoIdeavelImaginado = ideavel;

            return true;
        }

        ultimoIdeavelImaginado = null;
        return false;
    }

    public void EncerrarImaginacao(IdeiaUI ideia, Vector3 mousePos) {
        if (ultimoIdeavelImaginado != null) {
            ultimoIdeavelImaginado.DesativarPrevisaoDeIdeia();
            ultimoIdeavelImaginado = null;
        }

        Ideavel ideavel = PegarIdeavelEm(mousePos);
        if (ideavel != null) {
            ideavel.AplicarIdeia(ideia.ideiaInfo);
            ideia.Gastar();
        } else {
            ideia.Resetar();
        }

        OnEndIdealizacao?.Invoke(ideia.ideiaInfo);
        
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


    public void AdquirirIdeia(IdeiaInfo ideia, bool ignorarOsQueJaTem = true) {
        if (ideiasPossuidas.Contains(ideia)) {
            if (ignorarOsQueJaTem) return;
        } else {
            ideiasPossuidas.Add(ideia);
        }

        GameObject ideiaInstance = Instantiate(ideiaUIPrefab);
        ideiaInstance.transform.SetParent(ideiasHolder);

        IdeiaUI ideiaUI = ideiaInstance.GetComponent<IdeiaUI>();
        ideiaUI.Setup(ideia);
    }
}