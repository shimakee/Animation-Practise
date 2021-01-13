using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Vector2 directionFace;
    public float Speed;
    
    private Movement _movement;
    private IUnityService _unityService;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        directionFace = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_movement == null)
            _movement = new Movement(Speed);

        if (_unityService == null)
            _unityService = new UnityService();
    }

    // Update is called once per frame
    void Update()
    {
        directionFace = _movement.Calculate2D(_unityService.GetAxisRaw("Horizontal"), _unityService.GetAxisRaw("Vertical"), _unityService.GetDeltaTime());
        transform.Translate(directionFace);



        Vector2 input = new Vector2(_unityService.GetAxisRaw("Horizontal"), _unityService.GetAxisRaw("Vertical"));
        _animator.SetFloat("Horizontal", input.x);
        _animator.SetFloat("Vertical", input.y);
        _animator.SetFloat("Magnitude", input.magnitude);


        Attack();
        HandleAnimation(_animator, _rigidbody2D);
    }


    private void OnDrawGizmos()
    {
        var direction = (Vector2)transform.position + directionFace.normalized;
        Gizmos.DrawLine(transform.position, direction);
    }

    void Attack()
    {
        if (Input.GetButtonDown("attack"))
            _animator.SetTrigger("Attack");
    }

    void HandleAnimation(Animator animator, Rigidbody2D rigidbody2D)
    {
        if (animator == null)
            return;

        //animator.SetFloat("Horizontal", rigidbody2D.velocity.x);
        //animator.SetFloat("Vertical", rigidbody2D.velocity.y);
    }
}
