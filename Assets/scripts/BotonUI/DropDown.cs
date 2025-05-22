using UnityEngine;

public class DropDown : MonoBehaviour
{
    private Animator buttonAnim;
    private Boton boton;

    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        boton = GameObject.Find("Logica_BotonListBuilds").GetComponent<Boton>();
    }


    void Update()
    {
        if (boton.show_ListBuilds)
        {
            buttonAnim.SetBool("b_show_ListBuilds", true);
        }
        else
        {
            if (!boton.show_ListBuilds)
            {
                buttonAnim.SetBool("b_show_ListBuilds", false);
            }
        }
        
    }
}
