using Android.Speech.Tts;
using Android.App;
using Android.OS;



namespace GreatQuotes.Droid
{
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        TextToSpeech speech;
        string lastText;

        public void Speak(string text)
        {
            if (speech == null)
            {
                lastText = text;
                speech = new TextToSpeech(Application.Context, this);
            }
            else
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    speech.Speak(text, QueueMode.Flush, null, null);
                else
                {

                    speech.Speak(text, QueueMode.Flush, null);

                }
            }
        }

        public void OnInit(OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    speech.Speak(lastText, QueueMode.Flush, null, null);
                else
                {

                    speech.Speak(lastText, QueueMode.Flush, null);

                }
                lastText = null;
            }
        }
    }
}

