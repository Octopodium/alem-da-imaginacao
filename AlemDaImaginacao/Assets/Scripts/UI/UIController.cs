using UnityEngine;

public class UIController : MonoBehaviour {

    public static UIController instance { get; private set; }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }



    [Header("Resolucao")]
    public Animation animation;
    public AnimationClip animacaoNormal;
    public AnimationClip animacaoImaginando;
    public AnimationClip animacaoEntraImaginando;
    public AnimationClip animacaoSaiImaginando;

    [Header("Fase")]
    public Animation fadeAnimation;
    public AnimationClip fadeIn;
    public AnimationClip fadeOut;

    [Header("Ideia Surgindo")]
    public GameObject ideiaSurgindoObject;
    public IdeiaUI ideiaUISurgindo;
    public Animation ideiaSurgindoAnimation;
    public AnimationClip ideiaSurge;
    public AnimationClip ideiaFoca;
    public AnimationClip ideiaAbsorve;

    [Header("Dialogo")]
    public ImaginadorText dialogoText;




    public void EntrarImaginando() {
        animation.Stop();
        animation.clip = animacaoEntraImaginando;
        animation.Play();
    }

    public void SairImaginando() {
        animation.Stop();
        animation.clip = animacaoSaiImaginando;
        animation.Play();
    }



    public void FadeIn() {
        fadeAnimation.Stop();
        fadeAnimation.clip = fadeIn;
        fadeAnimation.Play();
    }

    public void FadeOut() {
        fadeAnimation.Stop();
        fadeAnimation.clip = fadeOut;
        fadeAnimation.Play();
    }




    public void SurgirIdeia(IdeiaInfo ideia) {
        ideiaUISurgindo.Setup(ideia);
        ideiaSurgindoAnimation.Stop();
        ideiaSurgindoAnimation.clip = ideiaSurge;
        ideiaSurgindoAnimation.Play();
        ideiaSurgindoObject.SetActive(true);
    }

    public void FocarIdeia() {
        ideiaSurgindoAnimation.Stop();
        ideiaSurgindoAnimation.clip = ideiaFoca;
        ideiaSurgindoAnimation.Play();
    }

    public void AbsorverIdeia() {
        ideiaSurgindoAnimation.Stop();
        ideiaSurgindoAnimation.clip = ideiaAbsorve;
        ideiaSurgindoAnimation.Play();
    }

    public void SumirComIdeia() {
        ideiaSurgindoObject.SetActive(false);
    }
}
