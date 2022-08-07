using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trajectory/Trigonometry")]
public class TrajectoralMovementTrigonometry : TrajectoralMovementBase
{
    public override Vector3 Movement(Vector3 startPos, Vector3 endPos, float time, float maxHeight)
    {
        //Lise matematiginden Pi'nin 180 derece oldugunu biliyoruz, bununla 0-1 arasinda akan zamani carparsak yarim cember hareketi elde etmis oluruz
        float hemicycle = Mathf.PI * time;
        float height = Mathf.Sin(hemicycle) * maxHeight;
        Vector3 movementVector = Vector3.Lerp(startPos, endPos, time) + new Vector3(0, height, 0);
        return movementVector;
    }
}
