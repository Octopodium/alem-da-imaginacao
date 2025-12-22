using UnityEngine;

public class JaneAnimator : MonoBehaviour{

    [SerializeField] private Animator animator;
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int fallHash = Animator.StringToHash("Fall");


    public void SetWalking(bool isWalking) => animator.SetBool(walkHash, isWalking);

    public void SetFacingSide (bool faceRight) {
        Vector3 scale = transform.localScale;
        if(faceRight) scale.x = 1;
        else scale.x = -1;
        transform.localScale = scale;
    }

    public void SetFalling(bool falling) => animator.SetBool(fallHash, falling);
    
}
