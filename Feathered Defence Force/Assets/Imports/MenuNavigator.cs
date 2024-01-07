using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] Color selectColor;
    [SerializeField] KeyCode[] keys = new KeyCode[3]; 
    [SerializeField] List<Button> buttons;
    private int currentButtonIndex = 0;

    void Start()
    {
        // Set the first button as selected
        SelectButton(currentButtonIndex);
    }

    void Update()
    {
        // go up in order
        if (Input.GetKeyDown(keys[0]))//Up arrow
        {
            ChangeSelectedButton(-1);
        }

        // Select the current button
        if (Input.GetKeyDown(keys[1]))//Z
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }

        // Navigate up
        if (Input.GetKeyDown(keys[2]))//previous
        {
            ChangeSelectedButton(1);
        }
    }

    void ChangeSelectedButton(int direction)
    {
        // Deselect the current button
        buttons[currentButtonIndex].image.color = Color.white;

        // Change the current button index while skipping over disabled buttons
        // do-while. do is run once without conditions, and after is run every while where the while condition is met
        // Check if there are any active buttons
        if (buttons.All(button => !button.gameObject.activeInHierarchy))
        {
            // No active buttons, exit the method
            return;
        }

        do
        {
            currentButtonIndex += direction;

            // Wrap around if reaching the end of the button list
            if (currentButtonIndex < 0)
            {
                currentButtonIndex = buttons.Count - 1;
            }
            else if (currentButtonIndex >= buttons.Count)
            {
                currentButtonIndex = 0;
            }
        } while (!buttons[currentButtonIndex].gameObject.activeInHierarchy); // Skip disabled buttons

        // Select the new current button
        SelectButton(currentButtonIndex);
    }

    void SelectButton(int index)
    {
        Debug.Log(index);
        buttons[index].Select();
        buttons[index].image.color = selectColor; // Highlight the selected button
    }
}