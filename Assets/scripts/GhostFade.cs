using UnityEngine;

public class GhostFade : MonoBehaviour
{
    private SpriteRenderer sr;
    public float fadeSpeed = 2f; // Ne kadar hýzlý yok olsun?

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // Hayaleti yarý saydam baþlat (Hayalet gibi görünsün)
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0.5f; // %50 Saydamlýk
            sr.color = c;
        }
    }

    void Update()
    {
        if (sr != null)
        {
            // Rengin þeffaflýðýný (Alpha) yavaþça azalt
            Color color = sr.color;
            color.a -= fadeSpeed * Time.deltaTime;
            sr.color = color;

            // Tamamen görünmez olduysa yok et
            if (color.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}