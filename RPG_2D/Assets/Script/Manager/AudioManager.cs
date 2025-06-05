using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManagerInstance;

    [SerializeField] 
    private float _sfxMinimumDistance;
    [SerializeField] 
    private AudioSource[] _sfx;
    [SerializeField] 
    private AudioSource[] _bgm;

    public bool _playBgm;
    private int _selectedBgmIndex;

    private bool _canPlaySFX;


    private void Awake()
    {
        if (_audioManagerInstance != null)
            Destroy(_audioManagerInstance.gameObject);
        else
            _audioManagerInstance = this;

        Invoke("AllowSFX", 1f);
    }

    private void Update()
    {
        if (!_playBgm)
            StopAllBGM();
        else
        {
            if (!_bgm[_selectedBgmIndex].isPlaying)
                PlayBGM(_selectedBgmIndex);
        }
    }

    public void PlaySFX(int sfxIndex, Transform source)
    {
        if (_canPlaySFX == false)
            return;

        if (_sfx[sfxIndex].isPlaying)
            return;

        if (source != null && Vector2.Distance(PlayerManager._playerManagerInstance._playerController.transform.position, source.position) > _sfxMinimumDistance)
            return;

        if (sfxIndex < _sfx.Length)
        {
            _sfx[sfxIndex].pitch = Random.Range(.85f, 1.1f);
            _sfx[sfxIndex].Play();
        }
    }

    public void StopSFX(int sfxIndex) => _sfx[sfxIndex].Stop();

    public void PlayBGM(int bgmIndex)
    {
        _selectedBgmIndex = bgmIndex;

        StopAllBGM();

        _bgm[_selectedBgmIndex].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < _bgm.Length; i++)
        {
            _bgm[i].Stop();
        }
    }

    public void PlayRandomBGM()
    {
        _selectedBgmIndex = Random.Range(0, _bgm.Length);
        PlayBGM(_selectedBgmIndex);
    }

    private void AllowSFX() => _canPlaySFX = true;

    public void StopSFXWithDelay(int index) => StartCoroutine(DecreaseVolume(_sfx[index]));

    private IEnumerator DecreaseVolume(AudioSource audio)
    {
        float defaultVolume = audio.volume;

        while (audio.volume > .1f)
        {
            audio.volume -= audio.volume * .2f;
            yield return new WaitForSeconds(.6f);

            if (audio.volume <= .1f)
            {
                audio.Stop();
                audio.volume = defaultVolume;
                break;
            }
        }

    }
}
