using System;

namespace FeelTheBern
{
	public class HighScores
	{
		/// <summary>
		/// The names of those who made High Scores.
		/// </summary>
		private string[] scoreNames;

		/// <summary>
		/// The top ten scores achieved in the game.
		/// </summary>
		private int[] scoreAmounts;

		/// <summary>
		/// HighScores Class Constructor. Initializes scoreNames and scoreAmounts
		/// </summary>
		public HighScores ()
		{
			scoreNames=new string[10];
			scoreAmounts=new int[10];
			for (int i=0; i<10; i++)
			{
				scoreNames [i] = "High Score";
				scoreAmounts [i] = 0;
			}
		}

		/// <summary>
		/// Adds the person's name and score to the list if it is high enough.
		/// </summary>
		/// <param name="name">Name of the person who made the score.</param>
		/// <param name="score">Score achieved in the game.</param>
		/// <returns>
		/// Returns true if the score was added to the list, false if it was not </returns>
		public bool addScore(string name, int score)
		{
			int scoreSlot=0;
			// find the highest score that the inputted score surpasses
			for (int i = 9; i >= 0; i--)
			{
				if (score > scoreAmounts [i])
				{
					scoreSlot = i;
				}
			}

			// if the score surpasses none of the others, do not add it to the list
			if (scoreSlot == 0)
			{
				return false;
			}
			//otherwise, move all scores below that slot down, and enter the current score in that slot
			for (int i = 9; i >= scoreSlot; i--)
			{
				if (i == scoreSlot)
				{
					scoreNames [i] = name;
					scoreAmounts [i] = score;
				}
				else
				{
					scoreNames [i] = scoreNames [i - 1];
					scoreAmounts [i] = scoreAmounts [i - 1];
				}
			}
			return true;
		}
	}
}
