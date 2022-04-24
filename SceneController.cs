using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridCols = 6;
    public const float offsetX = 4f;
    public const float offsetY = 4f;


    [SerializeField] public MainCard originalCard;
    [SerializeField] public Card_Back cardBack;
    [SerializeField] private Sprite[] images;

    private void Start()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }

        }
        Card_Back cBack;
        cBack = cardBack;
      
        //cback = cardBack;
        Card_Back.SetActive(false);
        yield return new WaitForSeconds(3);
        Card_Back.SetActive(true);



    }



    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }


    // -------------------------------------------------------------------------


    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _medic = 0;
    private int _wood = 0;
    private int _nothing = 0;
    private int _chance = 5;

    [SerializeField] private TextMesh medicLabel;
    [SerializeField] private TextMesh woodLabel;

    //private object gameOverText;
    public GameObject gameOverText;
    public TextMeshProUGUI timerText;
    //private object card_back;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    

    public void CardRevealed(MainCard card)
    {
        if (_chance > 0)
        {
            if (_firstRevealed == null)
            {
                _firstRevealed = card;
            }
            else
            {
                _secondRevealed = card;
                StartCoroutine(CheckMatch());
            }
        }
        else
        {
            StopCoroutine("StopWatch");
            gameOverText.SetActive(true);
            timerText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            if (_firstRevealed.id == 0)
            {
                _medic++;
                medicLabel.text = "약초 " + _medic + "개";

            }

            else if(_firstRevealed.id == 1)
            {
                _wood++;
                woodLabel.text = "목재 " + _wood + "개";
            }

            else if(_firstRevealed.id == 2)
            {
                _chance--;
            }

            else if(_firstRevealed.id == 3)
            {
                _chance--;
            }

            else
            {
                _nothing++;
            }
                          
        }

        else if(_medic == 5)
        {
            _medic++;
        }

        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();

            _chance--;
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    

    public void Restart()
    {

        SceneManager.LoadScene("MiniGame");
        Time.timeScale = 1;
    }
}

public class Card_Back
{
    internal static void SetActive(bool v)
    {
        throw new System.NotImplementedException();
    }
}