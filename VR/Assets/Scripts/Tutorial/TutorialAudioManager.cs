using UnityEngine.Audio;
using UnityEngine;

public class TutorialAudioManager : MonoBehaviour
{
    public AudioClip[] audios;
	public AudioSource[] srcs;
    void Awake()
    {
        for (int i=0;i<11;i++){
			srcs[i] = gameObject.AddComponent<AudioSource>();
			srcs[i].clip = audios[i];
		}
    }

    // Update is called once per frame
    public void play(int x){
		srcs[x].Play();
	}
}
