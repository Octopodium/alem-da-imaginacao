using System.Collections;
using UnityEngine;

public interface IRecebeTemplate {
    void RecebeTemplate(GameObject template);
}


public class ControladorDeObjeto : IResetavel  {
    [Header("</color=green>Componentes : </color>")]
    [Space(10)]
    [SerializeField] private GameObject prefab;
    public Vector3 respawnPos = new Vector3(0, 6, 0);

    [Space(15)]
    [Header("Objeto controlado :")]
    [Space(10)]
    public GameObject objeto;

    [Space(15)]
    [Header("Configurações")]
    [Space(10)]
    [SerializeField] private float dellay = 0.0f;
    [SerializeField] private bool habilitado = true;
    [SerializeField] private bool spawnNoInicio = false;
    [Tooltip("Caso o objeto prefab seja 'IRecebeTemplate', ele recebera o objeto 'template' como parâmetro de 'RecebeTemplate'. Util para replicar valores base nos objetos recém instanciados.")]
    public GameObject template;

    bool spawnando = false;


    private void Start() {
        if (template != null) template.SetActive(false);

        if (spawnNoInicio)
            Spawn();
    }


    void OnDestroy() {
        if (objeto != null) {
            Destrutivel destrutivel = objeto.GetComponent<Destrutivel>();
            if (destrutivel != null) {
                destrutivel.OnDestruido.RemoveListener(Respawn);
            }

            Destroy(objeto);
            objeto = null;
        }
    }

    public override void OnResetar() {
        Reiniciar();
    }

    /// <summary>
    /// Caso não exista nenhum objeto atribuido ao campo do objeto controlado, instancia um novo objeto com base no prefab.
    /// </summary>
    public void Spawn() {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine(){
        yield return new WaitForSecondsRealtime(dellay);
        if(!habilitado) yield break;
        if (objeto != null) yield break;
        if (spawnando) yield break;
        
        spawnando = true;
        AposSpawn(Instantiate(prefab, transform.TransformPoint(respawnPos), transform.rotation));
    }

    void AposSpawn(GameObject objeto) {
        if (objeto != null)
            spawnando = false;

        if (objeto != null && objeto != this.objeto) {
            if (this.objeto != null) {
                Destroy(this.objeto);
            }


            Destrutivel destrutivel = objeto.GetComponent<Destrutivel>();
            destrutivel?.OnDestruido.AddListener(Respawn);
            this.objeto = objeto;

            if (template != null) {
                IRecebeTemplate recebeTemplate = objeto.GetComponent<IRecebeTemplate>();
                if (recebeTemplate != null) {
                    recebeTemplate.RecebeTemplate(template);
                }
            }
        }
    }


    /// <summary>
    /// Transporta o objeto controlado para o ponto de respawn atribuido no componente e ativa ele.
    /// </summary>
    public void Respawn(){
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine() {
        yield return new WaitForSecondsRealtime(dellay);
        
        if (objeto != null) {
            objeto.transform.position = transform.TransformPoint(respawnPos);

            if(!objeto.activeInHierarchy)
                objeto.SetActive(true);
        } else {
            Spawn();
        }
        
    }

    /// <summary>
    /// Destroi o objeto controlado e reinicia o sistema
    /// </summary>
    public void Reiniciar() {
        if (objeto != null) {
            // Essa parte é exclusiva pra esse código, instanciar e desinstanciar 
            objeto.SetActive(false);

            Destroy(objeto);
            objeto = null;
        }

        if (spawnNoInicio) {
            Spawn();
        }
    }

}
