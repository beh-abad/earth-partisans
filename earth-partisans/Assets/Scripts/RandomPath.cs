using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    private bool coroutineAllowed;
    [SerializeField]
    private bool hasXYMovement;
    private float x, y;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fx;
    private GameObject spaceship;
    [SerializeField]
    private GameObject bossBullet;
    public static RandomPath randomPathInstance;
    private void Awake()
    {
        randomPathInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coroutineAllowed = true;
        spaceship = FindObjectOfType<CharacterController1>().gameObject;
        x = UnityEngine.Random.Range(-2.25f, 2.25f);
        y = UnityEngine.Random.Range(2f, 4f);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BossesPath());
    }
    private IEnumerator BossesPath()
    {
        if (hasXYMovement)
        {
            if (transform.position.x + 0.06f >= x && transform.position.x - 0.06f <= x || transform.position.y + 0.06f >= y && transform.position.y - 0.06f <= y)
            {
                x = UnityEngine.Random.Range(-2.25f, 2.25f);
                y = UnityEngine.Random.Range(2f, 4f);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), 4f * Time.deltaTime);
            yield return null;
        }
        else
        {
            if (transform.position.x + 0.06f >= x && transform.position.x - 0.06f <= x || transform.position.y + 0.06f >= y && transform.position.y - 0.06f <= y)
            {
                try
                {
                    x = spaceship.transform.position.x;
                }
                catch (MissingReferenceException)
                {
                    enabled = false;
                }
                y = UnityEngine.Random.Range(2f, 4f);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), 4f * Time.deltaTime);
            if (coroutineAllowed)
            {
                coroutineAllowed = false;
                yield return new WaitForSeconds(0.8f);
                Instantiate(bossBullet, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(fx);
                audioSource.volume = PauseMenuController.fxSoundVar;
                coroutineAllowed = true;
            }
        }
    }
}
