using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectMovement : MonoBehaviour
{
    private GameObject target;
    private GameObject bullet;
    private GameObject insect;
    private bool isShooting = false;
    private Animator anim;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float bulletSpeed = 70f;
    [SerializeField] private float minDistanceToMouse = 2f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Food");
        // get all children
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Bullet"))
            {
                bullet = child.gameObject;
            }
            if (child.CompareTag("Insect"))
            {
                insect = child.gameObject;
            }
        }
        if (target == null)
        {
            Debug.LogError("No food found");
        }
        if (bullet == null)
        {
            Debug.LogError("No bullet found");
        }
        if (insect == null)
        {
            Debug.LogError("No insect found");
        } else {
            anim = insect.GetComponent<Animator>();
        }
        bullet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = target.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distanceToMouse = Vector2.Distance(insect.transform.position, mousePosition);
        // move towards the target if distanceToMouse is greater than minDistanceToMouse
        // get animator parameter
        
        if (distanceToMouse > minDistanceToMouse || !anim.GetBool("isDead"))
        {
            bullet.SetActive(false);
            // face the target
            Vector2 direction = targetPosition - (Vector2)insect.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            insect.transform.rotation = Quaternion.Euler(0f,0f,angle);
            // move towards the target
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            StartShooting(mousePosition);
        }
    }

    void StartShooting(Vector2 mousePosition)
    {
        if (!isShooting)
        {
            Vector2 direction = mousePosition - (Vector2)insect.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            insect.transform.rotation = Quaternion.Euler(0f,0f,angle);
            isShooting = true;
            bullet.SetActive(true);
            // set bullet position to the front of the insect BoxCollider2D based on rotation
            Vector2 bulletPosition = insect.transform.position;
            bullet.transform.position = bulletPosition;
            StartCoroutine(Shoot(mousePosition));
        }
    }

    IEnumerator Shoot(Vector2 mousePosition)
    {
        // move bullet to pass the mouse position
        
        for (int i = 0; i < 10; i++)
        {
            Vector2 newPosition = bullet.transform.position;
            newPosition.x += Mathf.Cos(insect.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * bulletSpeed * Time.deltaTime;
            newPosition.y += Mathf.Sin(insect.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * bulletSpeed * Time.deltaTime;
            bullet.transform.position = newPosition;
            yield return new WaitForSeconds(0.05f);
        }
        isShooting = false;
    }
    
}
