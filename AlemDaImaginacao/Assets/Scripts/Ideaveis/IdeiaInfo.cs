using UnityEngine;

[CreateAssetMenu(fileName = "Ideia_", menuName = "Ideia")]
public class IdeiaInfo : ScriptableObject {
    public string nome;
    public Sprite sprite;
    public Color cor;

    public GameObject efeitoPrefab;
}
