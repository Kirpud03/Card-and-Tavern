using UnityEngine;

public class NextTurn : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (StaticHolder.Move%2==0)
        {
            StaticHolder.Move++;
        }
    }
}
