using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IdeiaUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public IdeiaInfo ideiaInfo;
    public Image imageFundo, imageIcone;

    public float opacidadeMovendo = 0.6f, opacidadeDesativado = 0.2f;
    float opacidadeNormal = 1.0f;

    Vector3 startDragPos;
    bool gastou = false;
    bool sendoArrastado = false;

    void Start() {
        Setup(ideiaInfo);
    }


    public void Setup(IdeiaInfo ideiaInfo) {
        this.ideiaInfo = ideiaInfo;

        if (ideiaInfo == null) return;

        imageFundo.color = ideiaInfo.cor;
        opacidadeNormal = ideiaInfo.cor.a;

        imageIcone.sprite = ideiaInfo.sprite;
        imageIcone.gameObject.SetActive(imageIcone.sprite != null); 
    }


    void MudarOpacidade(Image img, float v) {
        if (img.color.a == v) return;

        Color c = img.color;
        c.a = v;
        img.color = c;
    }

    public void OnBeginDrag(PointerEventData pointerEventData) {
        if (gastou) return;
        sendoArrastado = true;
        startDragPos = transform.position;
        imageFundo.raycastTarget = false;
        MudarOpacidade(imageFundo, opacidadeMovendo);
        MudarOpacidade(imageIcone, opacidadeMovendo);
        
        GameManager.instance.idealizador.ComecarAImaginar(this);
    }

    public void OnDrag(PointerEventData pointerEventData) {
        if (gastou) return;

        transform.position = pointerEventData.position;
        bool achouAlgo = GameManager.instance.idealizador.Imaginando(this, pointerEventData.position);

        if (achouAlgo) {
            MudarOpacidade(imageFundo, 0.0f);
            MudarOpacidade(imageIcone, 0.0f);
        } else {
            MudarOpacidade(imageFundo, opacidadeMovendo);
            MudarOpacidade(imageIcone, opacidadeMovendo);
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData) {
        if (gastou) return;

        GameManager.instance.idealizador.EncerrarImaginacao(this, pointerEventData.position);
        sendoArrastado = false;
    }


    public void Resetar() {
        if (sendoArrastado)
            transform.position = startDragPos;

        imageFundo.raycastTarget = true;
        MudarOpacidade(imageFundo, opacidadeNormal);
        MudarOpacidade(imageIcone, opacidadeNormal);
    }

    public void Gastar() {
        transform.position = startDragPos;
        imageFundo.raycastTarget = true;
        gastou = true;
        MudarOpacidade(imageFundo, opacidadeDesativado);
        MudarOpacidade(imageIcone, opacidadeDesativado);
    }

    public void Desgastar() {
        gastou = false;
        Resetar();
    }
}
