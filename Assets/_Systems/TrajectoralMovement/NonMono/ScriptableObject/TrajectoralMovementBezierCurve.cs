using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trajectory/Bezier Curve")]
public class TrajectoralMovementBezierCurve : TrajectoralMovementBase
{
    public override Vector3 Movement(Vector3 startPos, Vector3 endPos, float time, float maxHeight)
    {
        /*Burada Cubic Bezier Curve denilen yontemi kullandim, 
         * max height'i elle girmemiz gerektiginden bazi noktalarin yukseklilerini kucuk bir matematik ile hallettim
         yukaridan asagi dogru lerp'ledigimiz zaman, matematik sag olsun ki boyle harika bir smooth trajectory elde edebiliyoruz
        point'leri nereye koyarsaniz koyun cok smooth bi goruntu elde edebiliyorsunuz, spline'lar bu sekilde yapiliyor.*/


        //Cubic bezier curve'un max yuksekligi animation curve ve trigonometri yontemindeki kadar dumduz olmuyor. Basic bir matematik ile hallediyoruz.
        float y = maxHeight / (4.0f / 3f);

        Vector3 p0 = startPos;
        Vector3 p1 = startPos;
        Vector3 p2 = endPos;
        Vector3 p3 = endPos;

        p1.y = y;
        p2.y = y;

        Vector3 aPoint = Vector3.Lerp(p0, p1, time);
        Vector3 bPoint = Vector3.Lerp(p1, p2, time);
        Vector3 cPoint = Vector3.Lerp(p2, p3, time);
        Vector3 dPoint = Vector3.Lerp(aPoint, bPoint, time);
        Vector3 ePoint = Vector3.Lerp(bPoint, cPoint, time);

        Vector3 movementVector = Vector3.Lerp(dPoint, ePoint, time);
        return movementVector;
    }
}
