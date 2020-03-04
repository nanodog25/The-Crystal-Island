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
                { new DialogBox(5,  5,  Character.Ninja,        "Since when was there an asteroid feild in this sector outside of Jupiter.") },
                { new DialogBox(10, 10, Character.Scientist,    "I don't know, I'm receiving a lot of interfeirance from Jupiters outer Atmophere. The scanners have gone off line!") },
                { new DialogBox(20, 10, Character.Computer,     "See if you can temnporarly use those gas clouds to power them up again.") },
                { new DialogBox(30, 5,  Character.Scientist,    "Good idea, but for now I think I'm going to focus on keeping us all alive.") },
                { new DialogBox(35, 10, Character.Computer,     "Yeah that would be prefrable, I think the interfearance is coming from the island.") },
                { new DialogBox(45, 5,  Character.Ninja,        "How could an island interfear with our ship?") },
                { new DialogBox(50, 3,  Character.Scientist,    "I dont know, but i have a feeling we're going to find out soon enough!.") },
                { new DialogBox(55, 5,  Character.Computer,     "Watch Out!!!!!") },
                { new DialogBox(60, 5, Character.Ninja,         "Dont worry, if there is anyone who could get us through this its our pilot") },
                { new DialogBox(65, 5, Character.Scientist,      "Thanks!") },
                { new DialogBox(72, 3,  Character.Ninja,        "*Fist bump!*") },
                { new DialogBox(72, 3,  Character.Scientist,    "*Fist bump!*") },
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
