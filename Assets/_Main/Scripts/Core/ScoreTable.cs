using System.Linq;
using TMPro;

public class ScoreTable : MonoBehaviourSingleton<ScoreTable>
{
    public TMP_Text[] scoreTexts;
    public int stt;

    public void Init()
    {
        stt = 0;
        scoreTexts.ToList().ForEach(i => i.text = "");
    }

    public void AddScore(int score)
    {
        scoreTexts[stt].text = score.ToString();
        stt++;
    }
}
