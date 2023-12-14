using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELEVATOR : MonoBehaviour
{
    [SerializeField] GameObject _elevator;
    [SerializeField] float _distance;
    [SerializeField] float _time;
    [SerializeField] Vector3 _upper_floor;
    [SerializeField] Vector3 _upper_door;
    [SerializeField] Animator _door_animator;
    [SerializeField] GameObject _cover;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip _elevator_sound;
    [SerializeField] float sound_delay;
    Vector3 _base_floor;
    Vector3 _base_door;
    public bool _is_door_open = false;
   public  bool _start { get; set; }
    public bool _is_down { get; set; }
    private void Start()
    {
        _base_door = _cover.transform.position;
        _cover.transform.position =  _upper_door;
        _base_floor = _elevator.transform.position;
        _door_animator.SetTrigger("close");
    }

    void go_to_target_point()
    {
        _elevator.transform.position = Vector3.Lerp(_elevator.transform.position, _is_down ? _base_floor : _upper_floor, _time * Time.deltaTime);
        if(Vector3.Distance(_elevator.transform.position, _is_down ? _base_floor : _upper_floor) < 0.1f)
        {
            _cover.transform.position = _is_down ? _upper_door : _base_door;
            _elevator.transform.position = _is_down ? _base_floor : _upper_floor;
            _start = false;
        }
    }
    IEnumerator audiodelay(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(_elevator_sound);
    }
    void door_detect()
    {
        if (Vector3.Distance(transform.position, _elevator.transform.position) < _distance)
        {
            if (!_is_door_open)
            {
                _is_door_open = true;
                _door_animator.SetTrigger("open");
                StartCoroutine(audiodelay(sound_delay));
            }
        }
        else
        {
            if(_is_door_open)
            {
                _is_door_open = false;
                _door_animator.SetTrigger("close");
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(_start)
        {
            if(_is_door_open)
            {
                _is_door_open = false;
                _door_animator.SetTrigger("close");
            }
            go_to_target_point();
        }
        else
        {
            door_detect();
        }
    }
}
