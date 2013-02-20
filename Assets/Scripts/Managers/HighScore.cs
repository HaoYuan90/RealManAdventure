using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class HighScore : MonoBehaviour {
	
	public XmlNodeList m_highScoreInfo;
	
	private XmlDocument m_Doc;
	private const int m_MaxHighScores = 5;
	// Use this for initialization
	void Start () {
		GetHighScoreInfo();
	}
	
	public int ProcessScore(string name, float score){
		//return -1 if score is not among highscores, else return ranking of the score
		//if score is ranked, it will save the score to xml file
		float[] scores = getScores();
		int i;
		for(i=0;i<m_MaxHighScores;i++){
			if(score<scores[i])
				break;
		}
		int ranking = m_MaxHighScores+1-i;
		ranking = (ranking < m_MaxHighScores+1)?ranking:-1;
		//save score
		ModifyHighScoreXMLNodeList(name,score,scores[0]);
		
		
		return ranking;
	}
	
	private float[] getScores () {
		//return score sorted from low to high
		float[] scores = new float[m_MaxHighScores];
		int i=0;
		foreach(XmlNode player in m_highScoreInfo){
			float temp;
			float.TryParse(player["score"].InnerXml,out temp);
			scores[i] = temp;
			i++;
		}
		System.Array.Sort(scores);
		return scores;
	}
	
	private void GetHighScoreInfo()
	{
		// Read highscore from xml file
		TextAsset ta = (TextAsset)Resources.Load ("HighScores",typeof(TextAsset));
		m_Doc = new XmlDocument();
		m_Doc.LoadXml(GetTextWithoutBOM(ta));
		m_highScoreInfo = m_Doc.GetElementsByTagName("player");
		/*
		foreach(XmlNode player in m_highScoreInfo){
			Debug.Log(player["name"].InnerXml);
			Debug.Log (player["score"].InnerXml);
		}*/
	}
	
	private void ModifyHighScoreXMLNodeList(string name, float score, float lowestScore)
	{
		//create xml element and append to doc
		XmlNode playerName = m_Doc.CreateNode(XmlNodeType.Element,"name","");
		playerName.InnerXml = name;
		XmlNode playerScore = m_Doc.CreateNode(XmlNodeType.Element,"score","");
		playerScore.InnerXml = string.Format("{0:0.00}",score);
		XmlNode player = m_Doc.CreateNode(XmlNodeType.Element,"player","");
		player.AppendChild(playerName);
		player.AppendChild(playerScore);
		m_Doc.DocumentElement.AppendChild(player);
		
		if(Mathf.Abs(lowestScore) > 0.000001){
			//Highscore list if full, remove entry with lowest score
			//Mind floating point comparison :D
			foreach (XmlNode playerInfo in m_highScoreInfo){
				float temp;
				float.TryParse(playerInfo["score"].InnerXml,out temp);
				if(Mathf.Abs(temp -lowestScore) < 0.000001){
					m_Doc.DocumentElement.RemoveChild(playerInfo);
					break;
				}
			}
		}
		SaveToXML();
	}
	
	private void SaveToXML()
	{
		string filePath = Application.dataPath + "/Resources/HighScores.xml";
		m_Doc.Save (filePath);
	}
	
	private string GetTextWithoutBOM(TextAsset textAsset)
    {
       MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
       StreamReader streamReader = new StreamReader(memoryStream, true);

       string result = streamReader.ReadToEnd();

       streamReader.Close();
       memoryStream.Close();

       return result;
    }
}
