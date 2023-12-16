using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class place_detect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputActionProperty A;
    [SerializeField] GameObject[] bye;
    [SerializeField] GameObject point;
    [SerializeField] Animator animator;
    [SerializeField] float distance;
    public bool is_finish;
    
    bool is_press = true;
    void Start()
    {
        is_finish = true;
    }
    public void animation_finish()
    {
        is_finish = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(A.action.triggered)
        {
            bye[0].SetActive(is_press);
            bye[1].SetActive(is_press);
            is_press = !is_press;
        }
        if(Vector3.Distance(this.transform.position,point.transform.position) < distance && is_finish)
        {
            is_finish = false;
            animator.SetTrigger(Animator.StringToHash("start"));
        }
    }
}
