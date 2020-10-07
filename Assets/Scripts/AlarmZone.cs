using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class AlarmZone : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _alarmVolumeChangeTime;
    
    private bool _alarmOn = false;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private float _tempTime = 0f;

    private void Start()
    {
        _audioSource.volume = 0f;  
    }

    private void Update()
    {
        if (_alarmOn)
        {
            _tempTime += Time.deltaTime / _alarmVolumeChangeTime;
            if (_tempTime < _maxVolume)
                _audioSource.volume = Mathf.Lerp(_minVolume, _maxVolume, _tempTime);
        }

        if (!_alarmOn && _audioSource.volume > 0)
        {
            
            _tempTime += Time.deltaTime / _alarmVolumeChangeTime;
            if(_tempTime > _minVolume)
            {
                _audioSource.volume = Mathf.Lerp(_maxVolume, _minVolume, _tempTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Thief>(out Thief thief))
        {
            _box.SetActive(false);
            _alarmOn = true;
            _tempTime = 0;
            _audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _box.SetActive(true);
        _alarmOn = false;
        _tempTime = 0;
        if (_audioSource.volume <= 0)
            _audioSource.Stop();
    }
}
