using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void MoveButton(int value)
    {
        PlayerHealth.instance.playerMove.MoveButton(value);
    }

    public void JumpButton(bool isJump)
    {
        PlayerHealth.instance.playerMove.JumpButton(isJump);
    }

    public void DownButton()
    {
        PlayerHealth.instance.playerMove.DownButton();
    }



    public void RestartButton()
    {
        Time.timeScale = 1;
        GameManager.instance.RestartButton();
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        GameManager.instance.MenuButton();
    }

}
