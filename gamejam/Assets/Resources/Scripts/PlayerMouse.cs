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
        if (other.gameObject.layer == LayerMask.NameToLayer("DrawnPoints"))
            Debug.Log("Within point radius");
    }
}