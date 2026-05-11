using UnityEngine;
using Unity.Cinemachine;

public class HaritaYonetici : MonoBehaviour
{
    public void Start()
    {
        KamerayiKediyeBagla();
    }

    public void KamerayiKediyeBagla()
    {
        GameObject kedi = GameObject.FindWithTag("Cat");
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();

        if (kedi != null && vcam != null)
        {
            vcam.Follow = kedi.transform;
            Debug.Log("Kamera '" + kedi.name + "' objesine odaklandı.");
        }
    }
}