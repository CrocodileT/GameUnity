using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveMonster : Monster
{
    [SerializeField]
    private float speed = 0;

    private Vector3 direction;

    private Bullet bullet;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("FaerBall");
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7f) ReceiveDamage();
            else unit.ReceiveDamage();
        }

        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)
        {
            ReceiveDamage();
        }
    }

    protected override void Start()
    {
        direction = transform.right;
    }

    protected override void Update()
    {
        Run();
    }

    private void Run()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.5f, 1f);

        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<Character>())) direction *= -1.0f;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, 2.0f * Time.deltaTime);
    }
}
