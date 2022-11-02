using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();
        SingleTonUtils.add(SingletonType.AudioMananger, this);

    }

    public void play(string name)
    {
        player.PlayOneShot(Resources.Load<AudioClip>(name));
    }

    /**
     *  
     * <summary>播放资源</summary>
     * <param name="name">资源名称</param>
     */
    public static void Play(string name)
    {
        SingleTonUtils.get<AudioManager>(SingletonType.AudioMananger).play(name);
    }
}
