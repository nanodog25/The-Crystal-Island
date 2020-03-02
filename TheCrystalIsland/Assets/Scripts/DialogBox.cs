using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DialogBox
    {
        private string _text;
        public float Time;
        private float _duration;
        private Character _character;

        private Dictionary<Character, Vector2> PositionCoords = new Dictionary<Character, Vector2>
        {
            { Character.Ninja, new Vector2(100, 100)},
            { Character.Scientist, new Vector2(Screen.width - 100, 100)},
            { Character.Computer, new Vector2(Screen.width / 2, 50)},
        };

        public DialogBox(float time, float duration, Character character, string text)
        {
            _text = text;
            Time = time;
            _duration = duration;
            _character = character;
        }

        public void CreateDialogBox(GameObject dialogBoxObj, Transform controller)
        {
            GameObject obj = GameObject.Instantiate(dialogBoxObj,  PositionCoords[_character], new Quaternion(), controller);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = _text;
            obj.GetComponent<Duration>().Set(_duration);

            var col = _character.Colour();
            var imageCol = new Color(col.r, col.g, col.b, 0.2f);
            var outlineCol = new Color(col.r, col.g, col.b, 0.5f);
            obj.GetComponent<Image>().color = imageCol;
            obj.GetComponent<Outline>().effectColor = outlineCol;
        }
    }
}
