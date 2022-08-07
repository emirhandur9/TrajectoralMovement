using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Trajectory/Animation Curve")]
public class TrajectoralMovementAnimationCurve : TrajectoralMovementBase
{
    [SerializeField] AnimationCurve animationCurve;

    public override Vector3 Movement(Vector3 startPos, Vector3 endPos, float time, float maxHeight)
    {
        //zamani curve'a yolluyoruz, animationCurve'un en guzel yani, tam olarak nasil bir hareket istedigimizi elle cizip simule edebilmemiz
        float heightTime = animationCurve.Evaluate(time);
        float height = Mathf.Lerp(0, maxHeight, heightTime);

        Vector3 movementVector = Vector3.Lerp(startPos, endPos, time) + new Vector3(0, height, 0);
        return movementVector;
    }
}
