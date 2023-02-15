using System.Collections;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private int randomClip;   
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] clip1;
    private Animator anim;  
    private void Start()
    {   
        randomClip = Random.Range(0, clip1.Length);
        anim = GetComponent<Animator>();
        audioSource.volume = PauseMenuController.fxSoundVar;
        audioSource.PlayOneShot(clip1[randomClip]);
    }
    private void Update()
    {
        StartCoroutine(AnimTime());
    }
    private IEnumerator AnimTime()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
