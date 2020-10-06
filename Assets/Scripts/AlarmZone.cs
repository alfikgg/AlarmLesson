using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class AlarmZone : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _startAlarmVolume;
    
    private bool _alarmOn;

    private void Start()
    {
        _audioSource.volume = _startAlarmVolume;
        _alarmOn = false;
    }

    private void Update()
    {
        if(_alarmOn)
            StartCoroutine(AlarmVolumeUp());
        if (!_alarmOn && _audioSource.volume > 0)
            StartCoroutine(AlarmVolumeDown());
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Thief>(out Thief thief))
        {
            _box.SetActive(false);
            _alarmOn = true;
            _audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _box.SetActive(true);
        _alarmOn = false;
        if (_audioSource.volume <= 0)
            _audioSource.Stop();
    }

    private IEnumerator AlarmVolumeUp()
    {
        for (float i = 0; i <= 1; i++)
        {
            _audioSource.volume += i * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator AlarmVolumeDown()
    {
        for (float i = 0; i <= 1; i++)
        {
            _audioSource.volume -= i * Time.deltaTime;

            yield return null;
        }
    }


}
