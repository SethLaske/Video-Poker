using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class EffectManager : Branch
    {
        [SerializeField] private ParticleSystem particles;

        [SerializeField] private Text displayText;

        private Queue<TextEffect> textEffects = new Queue<TextEffect>();

        private TextEffect currentEffect;

        protected override void Initialize()
        {
            base.Initialize();

            displayText.gameObject.SetActive(false);
        }
        public override void Tick(float delta)
        {
            base.Tick(delta);

            if (currentEffect == null && textEffects.Count <= 0)
            {
                return;
            }

            if (currentEffect == null)
            {
                StartNewEffect();
            }

            if (currentEffect.displayTime > 0)
            {
                currentEffect.displayTime -= delta;
                return;
            }

            displayText.gameObject.SetActive(false);
            currentEffect = null;
        }

        public void EndGameEffects(Hand highestHand, float playerGain)
        {
            string displayMessage = "+ " + GameManager.Instance.currencyManager.GetCurrencyString(playerGain);
            AddTextEffectToQueue(new TextEffect(2, displayMessage));

            if (highestHand.payoutMultiplier > 3)
            {
                PlayParticles();
            }
            
        }


        public void PlayParticles()
        {
            particles.Play();
        }

        public void AddTextEffectToQueue(TextEffect newEffect)
        {
            textEffects.Enqueue(newEffect);
        }
        private void StartNewEffect()
        {
            if (textEffects.Count > 0)
            {
                currentEffect = textEffects.Dequeue();

                displayText.gameObject.SetActive(true);
                displayText.text = currentEffect.message;
            }
        }

    }

    public class TextEffect
    {
        public float displayTime;

        public string message;

        public TextEffect(float displayTime, string message)
        {
            this.displayTime = displayTime;
            this.message = message;
        }
    }
}
