using UnityEngine;

public class Boton : MonoBehaviour
{
    public bool show_ListBuilds;

    public void Button_ShowBuilds()
    {
        if (!show_ListBuilds)
        {
            show_ListBuilds = true;
        }
        else
        {
            if (show_ListBuilds)
            {
                show_ListBuilds = false;
            }
        }
    }
}
