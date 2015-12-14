using System;
using Newtonsoft.Json;
using System.IO;

namespace FeelTheBern
{
	public class JSONService
	{
		private string storagePath;

		/// <summary>
		/// Creates a new JSONService and creates HighScores.json if it doesn't exist
		/// </summary>
		public JSONService ()
		{
			//expected storage path for the .json file
			storagePath = Path.Combine (System.Environment.GetFolderPath(
				System.Environment.SpecialFolder.MyDocuments), "FeelTheBern");

			//checks to see if the storage path exists
			if (!Directory.Exists(storagePath))
			{
				//creates the directory if it does not exist
				Directory.CreateDirectory (storagePath);
			}
			//checks to see if the .json file exists within the storage path
			if (!File.Exists (Path.Combine (storagePath, "HighScores.json")))
			{
				//creates the file if it does not exist
				HighScores dummyScores = new HighScores();
				string dummyString=JsonConvert.SerializeObject(dummyScores);
				File.WriteAllText (Path.Combine (storagePath, "HighScores.json"), dummyString);
			}
		}

		/// <summary>
		/// Save the specified High Scores.
		/// </summary>
		/// <param name="highScores">List of all High scores.</param>
		public void Save(HighScores highScores)
		{
			//expected storage path for the .json file
			storagePath = Path.Combine (System.Environment.GetFolderPath(
				System.Environment.SpecialFolder.MyDocuments), "FeelTheBern");

			//store the data as a .json file
			string serializedScores = JsonConvert.SerializeObject (highScores);
			File.WriteAllText (Path.Combine (storagePath, "HighScores.json"), serializedScores);
		}

		/// <summary>
		/// Load in the stored HighScores.json.
		/// </summary>
		/// <returns>
		/// Returns the stored HighScores.json </returns>
		public HighScores Load()
		{
			//expected storage path for the .json file
			storagePath = Path.Combine (System.Environment.GetFolderPath(
				System.Environment.SpecialFolder.MyDocuments), "FeelTheBern");

			//retrieve the data within the .json file
			string[] filenames = Directory.GetFiles(storagePath, "*.json");
			string readText = File.ReadAllText (filenames [0]);
			return JsonConvert.DeserializeObject<HighScores> (readText);
		}
	}
}

