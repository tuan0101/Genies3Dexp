using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Player Grounded")]
    [SerializeField] GameObject mainBody;
    [SerializeField] Material originalMat;
    [SerializeField] Material glowMat;
    [SerializeField] AudioClip getShock;

    Animator anim;

    bool hasAnimator;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        hasAnimator = TryGetComponent(out anim);
    }

    public void GetHitBy(Hovl_Laser laser)
    {
        if (hasAnimator)
        {
            anim.SetTrigger("GetHit");
        }
        StartCoroutine( FreezePlayer());
        StartCoroutine(GetHitEffect(laser));
    }

    IEnumerator FreezePlayer()
    {
        float time = 0;
        float freezeDuration = 0.5f;
        Vector3 holdPosition = this.transform.position;

        while(time < freezeDuration)
        {
            time += Time.deltaTime;
            this.transform.position = holdPosition;
            yield return null;
            //yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator GetHitEffect(Hovl_Laser laser)
    {
        float flashDuration = 0.1f;
        Renderer renderer;
        renderer = mainBody.GetComponent<Renderer>();

        for (int i = 0; i < 4; i++)
        {
            renderer.material = glowMat;
            yield return new WaitForSeconds(flashDuration);
            renderer.material = originalMat;
            yield return new WaitForSeconds(flashDuration);
        }
        laser.EnablePlayerGetHit();
    }

    public void PlayVictoryAnim()
    {
        anim.SetBool("Victory", true);
    }

    public void PlayDefeatAnim()
    {
        anim.SetBool("Defeat", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Defeat")
        {
            GameManager.Instance.Defeat();
        }
    }
    // sound
    private void GetHit(AnimationEvent animationEvent)
    {
        AudioSource.PlayClipAtPoint(getShock, this.transform.position, 1f);
    }
}
