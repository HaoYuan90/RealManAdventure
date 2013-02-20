using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;

public class DisplayHighScore : MonoBehaviour {
	
	private List<KeyValuePair<string,float>> m_highScoreInfo;
	private XmlDocument m_Doc;
	private const int m_MaxHighScores = 5;
	
	// Use this for initialization
	void Start () {
		GetHighScoreInfo();
		ShowHighScore();
	}
	
	private void GetHighScoreInfo()
	{
		// Read highscore from xml file
		TextAsset ta = (TextAsset)Resources.Load ("HighScores",typeof(TextAsset));
		m_Doc = new XmlDocument();
		m_Doc.LoadXml(GetTextWithoutBOM(ta));
		XmlNodeList highScoreInfo = m_Doc.GetElementsByTagName("player");
		
		m_highScoreInfo = new List<KeyValuePair<string, float>>();
		
		foreach (XmlNode playerInfo in highScoreInfo){
			float score;
			float.TryParse(playerInfo["score"].InnerXml,out score);
			string name = playerInfo["name"].InnerXml;
			m_highScoreInfo.Add(new KeyValuePair<string,float>(name,score));						
		}
		//sort the list
		m_highScoreInfo = m_highScoreInfo.OrderByDescending(x=>x.Value).ToList();
	}
	
	private void ShowHighScore()
	{
		transform.guiText.text = "";
		foreach (KeyValuePair<string,float> playerInfo in m_highScoreInfo){
			float score = playerInfo.Value;
			string name = playerInfo.Key;
			name = "< "+name+" >";
			transform.guiText.text += string.Format("{0,-25}{1,15:0.00}\n",name,score);								
		}
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