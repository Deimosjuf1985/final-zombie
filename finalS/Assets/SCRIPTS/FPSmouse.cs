using UnityEngine;

public class FPSmouse : MonoBehaviour
{
    float mouseY;               //  variable posicion Y
    float mouseX;               //  variable posicion X
    public bool mouseInver;     // variable vision inversa
    public int lookCase;        

    void Update()
    {
        switch (lookCase)
        { 
            case 1:
                mouseX += Input.GetAxis("Mouse X"); // Direccion de mouse eje X
                if (mouseInver)
                { 
                    mouseY += Input.GetAxis("Mouse Y"); // Direccion de mouse eje Y
                    mouseY = Mathf.Clamp(mouseY, -45.0f, 45.0f); // Rango de vison
                }
                else
                { 
                    mouseY -= Input.GetAxis("Mouse Y"); // Direccion de mouse eje Y
                    mouseY = Mathf.Clamp(mouseY, -45.0f, 45.0f); // 
                }
                transform.eulerAngles = new Vector3(mouseY, mouseX); 
                break;
            case 2:
                mouseX += Input.GetAxis("Mouse X"); // 
                transform.eulerAngles = new Vector3(0, mouseX);
                break;
        }
    }
}