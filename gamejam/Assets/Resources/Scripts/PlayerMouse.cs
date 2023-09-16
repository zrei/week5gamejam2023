using UnityEngine;

public class PlayerMouse : Singleton<PlayerMouse>
{
    private Rigidbody2D m_Rb;
    private CircleCollider2D m_Collider;
    private int m_NumberOfPointsWithinRange = 0;
    private float m_InvulCountdown = 0f;
    private bool m_CanBeDamaged = true;

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

        if (!m_CanBeDamaged)
        {
            m_InvulCountdown -= Time.deltaTime;
            if (m_InvulCountdown <= 0)
                m_CanBeDamaged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DrawnPoints"))
        {
            m_NumberOfPointsWithinRange += 1;
            GlobalEvents.PlayerControlEvents.WithinPointRangeEvent?.Invoke();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && m_CanBeDamaged)
        {
            m_CanBeDamaged = false;
            m_InvulCountdown = GlobalSettings.g_PlayerInvulTime;
            GlobalEvents.PlayerEvents.PlayerHealthChangeEvent?.Invoke(-1);
        }
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