using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSemi3D : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] float speed;
    [SerializeField] GameObject ObjectToTrack;

    Vector3 _direction;
    private IUnityService _unityService;
    private Movement _movement;
    private Rigidbody _rb;

    private Vector3 lastFacing;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _direction = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_movement == null)
            _movement = new Movement(speed);

        if (_unityService == null)
            _unityService = new UnityService();
    }

    // Update is called once per frame
    void Update()
    {
        _direction = _movement.Calculate2D(_unityService.GetAxisRaw("Horizontal"), _unityService.GetAxisRaw("Vertical"), _unityService.GetDeltaTime()).normalized;
        

        animator.SetFloat("Horizontal", _direction.x);
        animator.SetFloat("Vertical", _direction.y);
        animator.SetFloat("Magnitude", _direction.magnitude);


        Attack();
        HandleAnimation(animator, _rb);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement.Calculate(_direction, Time.fixedDeltaTime) * speed;
        if (_direction.magnitude != 0)
            lastFacing = new Vector3(_direction.x, 0, _direction.y).normalized;

        var currentPosition = transform.position;
        var directionFacing = lastFacing;
        var directionToCam = ObjectToTrack.transform.position - currentPosition;
        directionFacing.y = 0;
        directionToCam.y = 0;
        var angleSigned = Vector3.SignedAngle(directionToCam, directionFacing, currentPosition);

        animator.SetFloat("angleToCam", angleSigned);
    }


    private void OnDrawGizmos()
    {

        var origin = Vector3.zero;
        var currentPosition = transform.position;
        var directionFacing = lastFacing;
        var directionToCam = ObjectToTrack.transform.position - transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(currentPosition, directionFacing + transform.position);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(currentPosition, ObjectToTrack.transform.position);
        directionFacing.y = 0;
        directionToCam.y = 0;
        var angeSigned = Vector3.SignedAngle(directionToCam, directionFacing, currentPosition);

        //Debug.Log("==============");
        //Debug.Log($"facing {directionFacing}");
        //Debug.Log($" to objected {directionToCam}");
        Debug.Log(angeSigned);
        //Debug.Log(ange);
    }

    void Attack()
    {
        if (Input.GetButtonDown("attack"))
            animator.SetTrigger("Attack");
    }

    void HandleAnimation(Animator animator, Rigidbody rigidbody2D)
    {
        if (animator == null)
            return;

        //animator.SetFloat("Horizontal", rigidbody2D.velocity.x);
        //animator.SetFloat("Vertical", rigidbody2D.velocity.y);
    }
}
