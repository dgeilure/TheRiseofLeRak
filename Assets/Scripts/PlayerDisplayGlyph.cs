using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplayGlyph : MonoBehaviour
{
    [SerializeField]
    private GameObject glyphenAttack;
    [SerializeField]
    private GameObject glyphenProtection;
    [SerializeField]
    private GameObject glyphenMalice;

    private Coroutine invisCor;

    public void GlyphAttackVisible ()
    {
        glyphenAttack.SetActive(true);
        glyphenProtection.SetActive(false);
        glyphenMalice.SetActive(false);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(5));
    }
    public void GlyphProtectionVisible ()
    {
        glyphenAttack.SetActive(false);
        glyphenProtection.SetActive(true);
        glyphenMalice.SetActive(false);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(5));
    }
    public void GlyphMaliceVisible ()
    {
        glyphenAttack.SetActive(false);
        glyphenProtection.SetActive(false);
        glyphenMalice.SetActive(true);
        StopInvisCor();
        invisCor = StartCoroutine(GlyphsInvisible(10));
    }

    private void StopInvisCor() {
        if (invisCor != null) {
            StopCoroutine(invisCor);
            invisCor = null;
        }
    }

    private IEnumerator GlyphsInvisible (int sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        glyphenAttack.SetActive(false);
        glyphenProtection.SetActive(false);
        glyphenMalice.SetActive(false);
    }
    
}
