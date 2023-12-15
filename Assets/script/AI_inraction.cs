using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_inraction : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _player_distance;
    [SerializeField] float time_to_next_intract_audio;
    [SerializeField] float _Audio_maxDistance;
    [SerializeField] float _Audio_minDistance;
    [SerializeField] AudioRolloffMode audiotype;
    [SerializeField] AudioClip[] _audio;
    [SerializeField] GameObject[] charcters_prefab;
    [SerializeField] GameObject[] waypoint;
    [SerializeField] float charcters_speed;
    [SerializeField] float charcters_hight;
    [SerializeField] float charcters_radius;
    [SerializeField] float charcters_stop_time;
    [SerializeField] float _action_distance;
    [SerializeField] int Respaown_delay;
    [SerializeField] RuntimeAnimatorController animatorController;
    NavMeshAgent[] navMeshAgents;
    Animator[] charcter_Animator;
    AudioSource[] audioSources;
    [SerializeField] float distance;
    [SerializeField] GameObject start_location;
    float[] timer;
    bool[] _is_location_reached;
    int[] index;
    float _audio_timer;
    bool[] _is_spaown_done;
    readonly int Walk = Animator.StringToHash("Walk");
    readonly int Pulling = Animator.StringToHash("pulling");
    readonly int Dwarf = Animator.StringToHash("dwarf");
    void Start()
    {
        _is_spaown_done = new bool[charcters_prefab.Length];
        audioSources = new AudioSource[charcters_prefab.Length];
        _is_location_reached = new bool[charcters_prefab.Length];
        charcter_Animator = new Animator[charcters_prefab.Length];
        navMeshAgents = new NavMeshAgent[charcters_prefab.Length];
        index = new int[charcters_prefab.Length];
        timer = new float[charcters_prefab.Length];
        StartCoroutine(Create_Charcter());
    }
    public IEnumerator Create_Charcter()
    {
        _audio_timer = time_to_next_intract_audio;
        for (int i = 0; i < charcters_prefab.Length; i++)
        {
            _is_location_reached[i] = true;
            charcters_prefab[i] = Instantiate(charcters_prefab[i], start_location.transform.position, Quaternion.identity);
            charcters_prefab[i].AddComponent<NavMeshAgent>();
            audioSources[i] = charcters_prefab[i].GetComponent<AudioSource>();
            audioSources[i].spatialBlend = 1;
            audioSources[i].maxDistance = _Audio_maxDistance;
            audioSources[i].minDistance = _Audio_minDistance;
            audioSources[i].playOnAwake = false;
            audioSources[i].rolloffMode = audiotype;
            navMeshAgents[i] = charcters_prefab[i].GetComponent<NavMeshAgent>();
            navMeshAgents[i].radius = charcters_radius;
            navMeshAgents[i].speed = charcters_speed;
            navMeshAgents[i].height = charcters_hight;
            navMeshAgents[i].obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
            charcter_Animator[i] = charcters_prefab[i].GetComponent<Animator>();
            apply_animator_controller(charcter_Animator[i]);
            timer[i] = charcters_stop_time;
            _is_spaown_done[i] = true;
            yield return new WaitForSeconds(Respaown_delay);
        }
    }
    
    void ai_all_location_reached(bool _is_reset)
    {
        for(int i = 0; i < charcters_prefab.Length;i++)
        {
            _is_location_reached[i] = _is_reset;
        }
    }
    void apply_animator_controller(Animator animator)
    {
        if (animator.runtimeAnimatorController != animatorController)
        {
            animator.runtimeAnimatorController = animatorController;
        }
    }
    AudioClip get_audio_for_intract_for_charcter(GameObject charcter)
    {
        int num = Random.Range(0,_audio.Length);
        if(num > _audio.Length - 1)
        {
            return null;
        }
        return _audio[num];
    }
    void walk_random()
    {
        for (int i = 0; i < charcters_prefab.Length; i++)
        {
            if (charcters_prefab[i] != null && _is_spaown_done[i])
            {
                var dis = Vector3.Distance(charcters_prefab[i].transform.position, waypoint[index[i]].transform.position);
                apply_animator_controller(charcter_Animator[i]);
                if (_is_location_reached[i])
                {
                    index[i] = Random.Range(0,waypoint.Length);
                    _is_location_reached[i] = false;
                }
                if (Vector3.Distance(charcters_prefab[i].transform.position, _player.transform.position) < _player_distance && _audio_timer <= 0)
                {
                    _audio_timer = time_to_next_intract_audio;
                    if (get_audio_for_intract_for_charcter(charcters_prefab[i]) != null)
                    {
                        audioSources[i].PlayOneShot(get_audio_for_intract_for_charcter(charcters_prefab[i]), 1f);
                    }
                }
                charcter_Animator[i].SetBool(Walk, dis > _action_distance);
                charcter_Animator[i].SetBool(Pulling, dis < _action_distance);
                navMeshAgents[i].transform.rotation = (dis < _action_distance) ? Quaternion.Lerp(navMeshAgents[i].transform.rotation, waypoint[index[i]].transform.rotation, 0.2f) : navMeshAgents[i].transform.rotation;
                if (dis < distance)
                {
                    timer[i] -= Time.deltaTime;
                    if (timer[i] <= 0)
                    {
                        timer[i] = charcters_stop_time;
                        _is_location_reached[i] = true;
                    }
                }
                else
                {
                    navMeshAgents[i].SetDestination(waypoint[index[i]].transform.position);
                }
                var lockd = Quaternion.LookRotation(new Vector3(navMeshAgents[i].destination.x, 0, navMeshAgents[i].destination.z));
                if (lockd.eulerAngles.x != 0 || lockd.eulerAngles.z != 0)
                {
                    navMeshAgents[i].transform.Rotate(0, lockd.eulerAngles.normalized.magnitude, 0);
                }
            }
        }
    }
    void Audio_timer()
    {
        _audio_timer -= Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        Audio_timer();
        walk_random();
    }
}
