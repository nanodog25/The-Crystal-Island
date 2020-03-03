using UnityEngine;

namespace Assets.Scripts
{
    public class Fade : MonoBehaviour
    {
        public enum FadeState
        {
            None,
            FadingIn,
            FadingOut,
            Blanked,
            Visible
        }

        public AnimationCurve FadeCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

        private float _alpha = 1;
        private float _targetAlpha = 0;
        private Texture2D _texture;
        private bool _done = true;
        private float _time;
        private bool _fadeOut;
        private float _currentVolume;
        private float _lowestVolume;

        private void Reset(bool fadeOut)
        {
            _fadeOut = fadeOut;
            _done = false;
            _alpha = fadeOut ? 0 : 1;
            _targetAlpha = 1 - _alpha;
            _time = 0;
        }

        [RuntimeInitializeOnLoadMethod]
        public void FadeOut()
        {
            _currentVolume = AudioListener.volume;
            _lowestVolume = _currentVolume < 0.3f ? 0 : 0.3f;
            Reset(true);
        }

        public void FadeIn()
        {
            Reset(false);
        }

        public void SetDone()
        {
            _done = true;
        }

        public FadeState GetFadeState()
        {
            if (_done)
            {
                return _fadeOut ? FadeState.Blanked : FadeState.Visible;
            }
            else
            {
                return _fadeOut ? FadeState.FadingOut : FadeState.FadingIn;
            }
        }

        public void OnGUI()
        {
            if (_done)
            {
                if (_fadeOut)
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

                return;
            }
            if (_texture == null)
                _texture = new Texture2D(1, 1);

            AudioListener.volume = Mathf.Lerp(_currentVolume, _lowestVolume, _alpha);
            _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
            _texture.Apply();

            _time += Time.deltaTime / 3f;
            _alpha = Mathf.Abs(_targetAlpha - FadeCurve.Evaluate(_time));
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

            if (_fadeOut && _alpha >= 1 || !_fadeOut && _alpha <= 0)
                _done = true;
        }
    }
}
