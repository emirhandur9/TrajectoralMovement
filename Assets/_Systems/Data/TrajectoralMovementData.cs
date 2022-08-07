using System;
using UnityEngine;

[Serializable]
public class TrajectoralMovementData 
{
    [Header("Core")]

    [Tooltip("Topun egik atis icin cikabilecegi maximum yukselik miktari")]
    public ScriptableFloat maxHeight;
    [Tooltip("Topun hedefe kac saniyede ulastigi")]
    public ScriptableFloat duration;

    [Header("Bounce")]

    [Tooltip("Bounce hareketini yapmak isteyip istemedigimiz")]
    public ScriptableBool isBounce;
    [Tooltip("Top her yere carptiginde yukseliginin ne kadar azaldigi.")]
    public ScriptableFloat bounceHeightMultiplier; //muhtemelen bunu topun mass degerine ayarlamam?z falan laz?m ama fizik formullerine girmeyeyim cok :) gerekirse gireriz.
    [Tooltip("Max height bu degerin altina dustugunde topun bounce'u bitmis oluyor")]
    public ScriptableFloat minBounceHeight;
    [Tooltip("Top yere carptiktan sonra bounce alirken ne kadar ileri gitmesi gerektiginin carpani")]
    public ScriptableFloat bounceForwardMultiplier;
}
