using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource AudioMenu;
    public GameDatas Datas;

    private AudioSource currAudio;
    private float delayOfPlay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currAudio = AudioMenu;
    }

    void Update()
    {
        if (Datas.IsInMenu && Datas.IsSettedMusic)
        {
            Datas.IsSettedMusic = false;
            currAudio.PlayScheduled(delayOfPlay);
        }
        else if (!Datas.IsSettedMusic && !Datas.IsInMenu)
        {
            delayOfPlay = currAudio.time;
            currAudio.Pause();
        }

    }
}
