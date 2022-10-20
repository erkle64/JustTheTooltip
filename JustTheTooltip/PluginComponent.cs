using System;
using UnityEngine;

namespace JustTheTooltip
{
    public class PluginComponent : MonoBehaviour
    {
        public static BepInEx.Logging.ManualLogSource log;

        public PluginComponent(IntPtr ptr) : base(ptr)
        {
        }

        public static void TooltipFrame__generateItemContainer(TooltipFrame __instance, ItemTemplate itemTemplate, int itemCount)
        {
            if (itemTemplate != null)
            {
                if (!__instance.uiContainer_item_custom.activeSelf)
                {
                    __instance.uiContainer_item_custom.SetActive(true);
                    __instance.uiText_itemContent.setText("");
                }
                else
                {
                    __instance.uiText_itemContent.setText(__instance.uiText_itemContent.tmp.text);
                }

                var text = __instance.uiText_itemContent.tmp.text;

                text += "\n" + string.Format("Stack: {0}", itemTemplate.stackSize);

                if (itemTemplate.buildableObjectTemplate != null)
                {
                    text += "\n" + string.Format("Size: {0}x{1}x{2}", itemTemplate.buildableObjectTemplate.size.x, itemTemplate.buildableObjectTemplate.size.y, itemTemplate.buildableObjectTemplate.size.z);
                }

                __instance.uiText_itemContent.setText(text.TrimStart('\n'));
            }
        }
    }
}