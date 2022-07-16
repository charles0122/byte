using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;

public class MouseManger : PersistentMonoSingleton<MouseManger>
{

    // 无法操作时禁止
    public Texture2D normal, forbid, dialog;
    private void Update()
    {

    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "NPC":
                    Cursor.SetCursor(dialog, new Vector2(16, 16), CursorMode.Auto);
                    break;

                default:
                    Cursor.SetCursor(normal, new Vector2(16, 16), CursorMode.Auto);
                    break;

            }
        }
    }

    
}
