using UnityEngine;

public class PlayerMouse : Singleton<PlayerMouse>
{
    private Rigidbody2D m_Rb;
    private CircleCollider2D m_Collider;
    private int m_NumberOfPointsWithinRange = 0;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        m_Rb = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<CircleCollider2D>();

        if (GlobalSettings.IsReady)
            InitialiseCollider();
        else
            GlobalSettings.OnReady += InitialiseCollider;
    }

    private void InitialiseCollider()
    {
        GlobalSettings.OnReady -= InitialiseCollider;
        m_Collider.radius = GlobalSettings.g_PlayerCursorColliderRadius;
    }

    private void Update()
    {
        Vector3 movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Rb.MovePosition(new Vector2(movePos.x, movePos.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("!!!");
        if (other.gameObject.layer == LayerMask.NameToLayer("DrawnPoints"))
        {
            m_NumberOfPointsWithinRange += 1;
            GlobalEvents.PlayerControlEvents.WithinPointRangeEvent?.Invoke();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            GlobalEvents.PlayerEvents.PlayerHealthChangeEvent?.Invoke(-1);
            //HandleAttack();  //probably throw to another script for cleanliness
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DrawnPoints"))
        {
            m_NumberOfPointsWithinRange -= 1;
            if (m_NumberOfPointsWithinRange == 0)
                GlobalEvents.PlayerControlEvents.NotWithinPointRangeEvent?.Invoke();
        }      
    }
}