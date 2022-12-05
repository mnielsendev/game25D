using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
  public enum Turns
	{
    PlayerTurn,
    OpponentTurn
	}
  public enum Choices
	{
    A,
    B,
    C
	}

  public TextManager textManager;
  public float textChangeDelay = 1.5f;
  private bool isBattleConcluded;
  private Turns currentTurn;
  private Choices currentChoice;

// Placeholder choices
  private string textA = "Choose: \n" +
        ">> Choice A \n" +
        "- Choice B \n" +
        "- Choice C";
  private string textB = "Choose: \n" +
        "- Choice A \n" +
        ">> Choice B \n" +
        "- Choice C";
  private string textC = "Choose: \n" +
        "- Choice A \n" +
        "- Choice B \n" +
        ">> Choice C";

  // Start is called before the first frame update
  void Start()
  {
    isBattleConcluded = false;
    currentTurn = Turns.PlayerTurn;
    currentChoice = Choices.A;
    StartCoroutine(LoopTurns());
  }

  IEnumerator LoopTurns()
	{
    while (!isBattleConcluded)
		{
      if (currentTurn == Turns.PlayerTurn)
      {
        textManager.ChangeDisplayText("\n" + "Player's turn!");
        yield return new WaitForSeconds(textChangeDelay);
        currentChoice = Choices.A;
        textManager.ChangeDisplayText(textA);
        // Prompt the player to select a choice
        yield return WaitForInput();
        // Process the choice the player made
        textManager.ChangeDisplayText("");
        yield return new WaitForSeconds(textChangeDelay);
        ProcessChoice();
        yield return new WaitForSeconds(textChangeDelay);
        currentTurn = Turns.OpponentTurn;
      }
      // Opponent's Turn
      else if (currentTurn == Turns.OpponentTurn)
      {
        textManager.ChangeDisplayText("");
        yield return new WaitForSeconds(textChangeDelay);
        currentChoice = (Choices)Random.Range(0, 3);
        ProcessChoice();
        yield return new WaitForSeconds(textChangeDelay);
        textManager.ChangeDisplayText("");
        yield return new WaitForSeconds(textChangeDelay);
        currentTurn = Turns.PlayerTurn;
      }
      yield return null;
    }
    // Battle has concluded at this point
	}

  IEnumerator WaitForInput()
	{
    bool choiceSelected = false;
    while (!choiceSelected)
		{
      switch (currentChoice)
      {
        case Choices.A:
        {
          if (Input.GetKeyDown(KeyCode.DownArrow))
          {
            currentChoice = Choices.B;
            textManager.ChangeDisplayText(textB);
          }
          else if (Input.GetKeyDown(KeyCode.Return))
          {
            //Player has made selection A
            choiceSelected = true;
          }
          break;
        }
        case Choices.B:
        {
          if (Input.GetKeyDown(KeyCode.UpArrow))
          {
            currentChoice = Choices.A;
            textManager.ChangeDisplayText(textA);
          }
          else if (Input.GetKeyDown(KeyCode.DownArrow))
          {
            currentChoice = Choices.C;
            textManager.ChangeDisplayText(textC);
          }
          else if (Input.GetKeyDown(KeyCode.Return))
          {
            //Player has made selection B
            choiceSelected = true;
          }
          break;
        }
        case Choices.C:
        {
          if (Input.GetKeyDown(KeyCode.UpArrow))
          {
            currentChoice = Choices.B;
            textManager.ChangeDisplayText(textB);
          }
          else if (Input.GetKeyDown(KeyCode.Return))
          {
            //Player has made selection C
            choiceSelected = true;
          }
          break;
        }
      }
      // If we got here, a selection still has not been made yet
      yield return null;
    }
    // At this point, a choice has been made
  }

  void ProcessChoice()
	{
    string choiceSelected = "";
    switch (currentChoice)
    {
      case Choices.A:
        choiceSelected = "Choice A";
        break;
      case Choices.B:
        choiceSelected = "Choice B";
        break;
      case Choices.C:
        choiceSelected = "Choice C";
        break;
    }
    // Show text expressing the choice being acted out
    if (currentTurn == Turns.PlayerTurn)
      textManager.ChangeDisplayText("Player selected " + choiceSelected + "!");
    else if (currentTurn == Turns.OpponentTurn)
      textManager.ChangeDisplayText("Opponent selected " + choiceSelected + "!");
  }

}
