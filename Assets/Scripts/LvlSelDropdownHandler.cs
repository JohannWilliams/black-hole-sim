using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlSelDropdownHandler : MonoBehaviour
{
    List<string> ddItems = new List<string>();
    public TMPro.TMP_Dropdown dropdown;

    public TextMeshProUGUI ddText;
    public TextMeshProUGUI lvlLabel;
    // Start is called before the first frame update
    void Start()
    {
        ddItems.Add("Preset 1");
        ddItems.Add("Preset 2");
        ddItems.Add("Preset 3");
        ddItems.Add("Preset 4");

        dropdown.ClearOptions();
        dropdown.AddOptions(ddItems);

        ddText.text = "Level Selected : " + dropdown.options[0].text;
        //lvlLabel.text = dropdown.options[0].text;
    }

    public void DropdownItemSelected()
    {
        int index = dropdown.value;

        ddText.text = "Level Selected : " + dropdown.options[index].text;
        //lvlLabel.text = dropdown.options[index].text;
        GameObject.FindObjectOfType<LvlManager>().SelectLevel(index + 1);
    }
}
