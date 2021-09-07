using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;
    Vector3 startDistance, moveVec;

    AudioSource audioSource;

    public AudioClip[]
    music_ambient;


    void Start()
    {
        startDistance = transform.position - _target.position; //разница позиции игрока и камеры

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveVec = _target.position + startDistance;

        moveVec.y = startDistance.y;

        transform.position = moveVec;

        RandomMusic();
    }

    void RandomMusic()
    {
        if (!audioSource.isPlaying)
        {
            GetComponent<AudioSource>().clip = music_ambient[Random.Range(0, music_ambient.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
