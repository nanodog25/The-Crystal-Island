using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Space_Shooter_Template_FREE.Scripts
{
    public class DialogController : MonoBehaviour
    {
        public GameObject DialogBoxObj;

        private float _time;
        private float _nextDialogTime;
        private List<DialogBox> _dialogs;

        private void Awake()
        {
            _dialogs = new List<DialogBox>
            {
                { new DialogBox("Crystals, on a gas giant? How?", 5, 5, Character.Ninja) },
                { new DialogBox("You see, they were formed in the centre of the planet and expelled outwards due to random collisions over millions of years - overall their mass is more than Jupiter itself!", 10, 10, Character.Scientist) },
                { new DialogBox("Calculating... that is correct, though only by 12.6 atomic units. A lucky guess.", 20, 10, Character.Computer) },
                { new DialogBox("Thanks Computer, who programmed you with a sardonic wit?", 30, 5, Character.Scientist) },
                { new DialogBox("Technically me. After your dismal efforts I had to do quite a lot of self-care.", 35, 10, Character.Computer) },
                { new DialogBox("Even so, you can't say he wasn't right. Nice work Scientist.", 45, 5, Character.Ninja) },
                { new DialogBox("Thanks Ninja.", 50, 5, Character.Scientist) },
                { new DialogBox("*Fist bump!*", 55, 5, Character.Ninja) },
                { new DialogBox("*Fist bump!*", 55, 5, Character.Scientist) },
            };

            _dialogs.Sort((d1, d2) => d1.Time.CompareTo(d2.Time));
            _nextDialogTime = _dialogs[0].Time;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_nextDialogTime != 0 && _nextDialogTime < _time)
            {
                DisplayDialog(_dialogs[0]);
                _dialogs.RemoveAt(0);
                if (_dialogs.Any())
                    _nextDialogTime = _dialogs[0].Time;
                else
                    _nextDialogTime = 0;
            }
        }

        private void DisplayDialog(DialogBox dialog)
        {
            dialog.CreateDialogBox(DialogBoxObj, transform);
        }
    }
}
