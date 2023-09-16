using UnityEngine;

public class PointsManager : Singleton<PointsManager>
{
    private int m_Points = 0;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        // subscribe to enemy death or something idk
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
    }
}