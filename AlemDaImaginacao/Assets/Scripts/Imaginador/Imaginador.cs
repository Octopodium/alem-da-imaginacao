using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class Imaginador : MonoBehaviour {
    public IdeiaInfo ideia;

    [Header("Antes da Imaginacao")]
    public Transform pontoDeIdleDoPlayer;
    public CinemachineCamera cameraPreImaginacao;
    public GameObject[] deletarAoEntrarNaImaginacao;
    public GameObject[] deletarAoSairDaImaginacao;
    public string[] textos;
    public string[] textosQueNaoVaoSerLidos;
    public float tempoEntreTextos = 0.5f;


    [Header("Na Imaginacao")]
    public Transform spawnImaginacao;
    public CinemachineCamera cameraImaginacao;
    public Transform pontoDeSaidaDoPlayer;
    public IdeiaUI ideiaParaColetar;




    void OnTriggerEnter(Collider other) {
        GameObject go = other.attachedRigidbody.gameObject;
        if (!go.CompareTag("Player")) return;


        StartCoroutine(AnimacaoDeImaginacao());
    }

    public void EntrarImaginacao() {
        StartCoroutine(AnimacaoDeImaginacao());
    }



    IEnumerator MoverAte(GameObject player, Vector3 posicaoFinal) {
        Vector3 posicaoInicial = player.transform.position;

        float duracao = 1f;
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao) {
            float y = player.transform.position.y;
            player.transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, tempoDecorrido / duracao);
            player.transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
            
            tempoDecorrido += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator AnimacaoDeImaginacao() {
        GameObject player = Player.instance.gameObject;
        Player.instance.Parar();
        Player.instance.enabled = false;

        Vector3 posicaoFinal = pontoDeIdleDoPlayer.position;


        yield return MoverAte(player, posicaoFinal);

        ideiaParaColetar.Setup(ideia);

        UIController.instance.EntrarImaginando();

        yield return new WaitForSeconds(3f);

        yield return UIController.instance.dialogoText.EscreverSequenciaDeTextos(textos, tempoEntreTextos);

        UIController.instance.FadeOut();

        StartCoroutine(UIController.instance.dialogoText.EscreverSequenciaDeTextos(textosQueNaoVaoSerLidos, tempoEntreTextos));
        yield return new WaitForSeconds(4.5f);

        UIController.instance.dialogoText.InterromperTudo();

        
        CameraManager.instance.MudarCamera(cameraImaginacao);
        Player.instance.Teletransportar(spawnImaginacao.position);
        CameraManager.instance.DeixarCameraCerta(cameraImaginacao);

        UIController.instance.FadeIn();
        yield return new WaitForSeconds(0.5f);
        Player.instance.enabled = true;

        foreach (GameObject goDel in deletarAoEntrarNaImaginacao) {
            Destroy(goDel);
        }
    }

    public void SairImaginacao() {
        StartCoroutine(AnimacaoSaindoImaginacao());
        
    }


    IEnumerator AnimacaoSaindoImaginacao() {
        GameObject player = Player.instance.gameObject;
        Player.instance.Parar();
        Player.instance.enabled = false;

        Vector3 posicaoFinal = pontoDeSaidaDoPlayer.position;

        yield return MoverAte(player, posicaoFinal);

        yield return new WaitForSeconds(1f);

        UIController.instance.SurgirIdeia(ideia);

        yield return new WaitForSeconds(1f);

        UIController.instance.FocarIdeia();


        yield return new WaitForSeconds(5f);

        Player.instance.Teletransportar(pontoDeIdleDoPlayer.position);
        CameraManager.instance.MudarCamera(CameraManager.instance.cameraDeGameplay);

        foreach (GameObject goDel in deletarAoSairDaImaginacao) {
            Destroy(goDel);
        }

        CameraManager.instance.DeixarCameraCerta(CameraManager.instance.cameraDeGameplay);

        UIController.instance.AbsorverIdeia();
        yield return new WaitForSeconds(3f);

        UIController.instance.SumirComIdeia();
        UIController.instance.SairImaginando();
        GameManager.instance.idealizador.AdquirirIdeia(ideia);

        yield return new WaitForSeconds(1.5f);
        Player.instance.enabled = true;

        
    }

}
