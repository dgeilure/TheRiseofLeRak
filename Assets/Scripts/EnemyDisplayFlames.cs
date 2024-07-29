using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisplayFlames : MonoBehaviour
{
    [SerializeField]
    private GameObject flameAttack;
    [SerializeField]
    private GameObject flameProtection;
    [SerializeField]
    private GameObject flameMalice;

    private Coroutine invisCor;

    public void FlameAttackVisible()
    {
        flameAttack.SetActive(true);
        flameProtection.SetActive(false);
        flameMalice.SetActive(false);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(5));
    }
    public void FlameProtectionVisible()
    {
        flameAttack.SetActive(false);
        flameProtection.SetActive(true);
        flameMalice.SetActive(false);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(5));
    }
    public void FlameMaliceVisible()
    {
        flameAttack.SetActive(false);
        flameProtection.SetActive(false);
        flameMalice.SetActive(true);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(10));
    }

    private void StopInvisCor()
    {
        if (invisCor != null)
        {
            StopCoroutine(invisCor);
            invisCor = null;
        }
    }

    private IEnumerator GlyphsInvisible(int sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        flameAttack.SetActive(false);
        flameProtection.SetActive(false);
        flameMalice.SetActive(false);
    }
}
