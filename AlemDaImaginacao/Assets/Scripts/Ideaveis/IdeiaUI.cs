using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IdeiaUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Image image;

    public float opacidadeNormal, opacidadeMovendo, opacidadeDesativado;

    Vector3 startDragPos;
    bool gastou = false;


    void MudarOpacidade(float v) {
        if (image.color.a == v) return;

        Color c = image.color;
        c.a = v;
        image.color = c;
    }

    public void OnBeginDrag(PointerEventData pointerEventData) {
        if (gastou) return;
        startDragPos = transform.position;
        image.raycastTarget = false;
        MudarOpacidade(opacidadeMovendo);
        
        GameManager.instance.idealizador.ComecarAImaginar(this);
    }

    public void OnDrag(PointerEventData pointerEventData) {
        if (gastou) return;

        transform.position = pointerEventData.position;
        bool achouAlgo = GameManager.instance.idealizador.Imaginando(this, pointerEventData.position);

        if (achouAlgo) MudarOpacidade(0.0f);
        else MudarOpacidade(opacidadeMovendo);
    }

    public void OnEndDrag(PointerEventData pointerEventData) {
        if (gastou) return;

        GameManager.instance.idealizador.EncerrarImaginacao(this, pointerEventData.position);
    }


    public void Resetar() {
        transform.position = startDragPos;
        image.raycastTarget = true;
        MudarOpacidade(opacidadeNormal);
    }

    public void Gastar() {
        transform.position = startDragPos;
        image.raycastTarget = true;
        gastou = true;
        MudarOpacidade(opacidadeDesativado);
    }
}
