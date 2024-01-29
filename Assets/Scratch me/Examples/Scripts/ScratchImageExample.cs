using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace ScratchMe.Examples
{
    public class ScratchImageExample : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> itemsToEnable;

        [SerializeField]
        private List<Transform> images;

        [SerializeField]
        private float appearSpeed = 1.0f;

        [SerializeField]
        private float appearDelay = 0.5f;

        [SerializeField]
        private float downShift = 800.0f;

        [SerializeField]
        private Transform completeCardPosition;

        [SerializeField]
        private AnimationCurve appearCurve;

        [SerializeField]
        private AnimationCurve disappearCurve;

        [SerializeField]
        private AnimationCurve moveToCompleteCurve;

        [SerializeField]
        private Slider minSizeSlider;

        [SerializeField]
        private Slider maxSizeSlider;

        [SerializeField]
        private Slider minSpacingSlider;

        [SerializeField]
        private Slider maxSpacingSlider;

        [SerializeField]
        private Slider minRotationSlider;

        [SerializeField]
        private Slider maxRotationSlider;

        [SerializeField]
        private Toggle alignWithDirection;

        [SerializeField]
        private List<Button> brushButtons;

        private IEnumerator Start()
        {
            SubscribeMinMax(minSizeSlider, maxSizeSlider);
            SubscribeMinMax(minSpacingSlider, maxSpacingSlider);
            SubscribeMinMax(minRotationSlider, maxRotationSlider);

            var initialPositions = new List<Vector3>();
            foreach (var card in images)
            {
                initialPositions.Add(card.transform.position);

                var image = card.GetComponentInChildren<ScratchImage>();
                var brush = image.Brush;

                minSizeSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Size.Set(minSizeSlider.value, maxSizeSlider.value);
                    });

                maxSizeSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Size.Set(minSizeSlider.value, maxSizeSlider.value);
                    });

                brush.Size.Set(minSizeSlider.value, maxSizeSlider.value);

                minSpacingSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Spacing.Set(minSpacingSlider.value, maxSpacingSlider.value);
                    });

                maxSpacingSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Spacing.Set(minSpacingSlider.value, maxSpacingSlider.value);
                    });

                brush.Spacing.Set(minSpacingSlider.value, maxSpacingSlider.value);

                minRotationSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Rotation.Set(minRotationSlider.value, maxRotationSlider.value);
                    });

                maxRotationSlider.onValueChanged.AddListener(value =>
                    {
                        brush.Rotation.Set(minRotationSlider.value, maxRotationSlider.value);
                    });

                brush.Rotation.Set(minRotationSlider.value, maxRotationSlider.value);

                alignWithDirection.onValueChanged.AddListener(value =>
                    {
                        brush.AlignWithDirection = value;
                    });

                brush.AlignWithDirection = alignWithDirection.isOn;

                foreach (var brushButton in brushButtons)
                {
                    var capturedButton = brushButton;
                    brushButton.onClick.AddListener(() =>
                        {
                             brush.BrushTexture = capturedButton.targetGraphic.mainTexture;
                        });
                }
            }

            while (true)
            {
                foreach (var itemToEnable in itemsToEnable)
                    itemToEnable.SetActive(false);

                var complete = false;
                for (var index = 0; index < images.Count; index++)
                {
                    var cardIndex = index;
                    var cardTransform = images[index];
                    StartCoroutine(Move(cardTransform, initialPositions[index] + Vector3.down * downShift, initialPositions[index], index, appearCurve));

                    var cardScratchImage = cardTransform.GetComponentInChildren<ScratchImage>();
                    cardScratchImage.raycastTarget = true;
                    cardScratchImage.Clear();

                    cardScratchImage.OnStartScratching.RemoveAllListeners();
                    cardScratchImage.OnStartScratching.AddListener(() =>
                        {
                            foreach (var other in images)
                            {
                                var otherScratchImage = other.GetComponentInChildren<ScratchImage>();
                                if (otherScratchImage == cardScratchImage)
                                    continue;

                                otherScratchImage.raycastTarget = false;
                            }
                        });

                    cardScratchImage.OnScratchComplete.RemoveAllListeners();
                    cardScratchImage.OnScratchComplete.AddListener(() =>
                        {
                            complete = true;

                            StartCoroutine(Move(cardTransform, cardTransform.transform.position, completeCardPosition.position, cardIndex, moveToCompleteCurve));

                            for (var innerIndex = 0; innerIndex < images.Count; innerIndex++)
                            {
                                var other = images[innerIndex];
                                var otherScratchImage = other.GetComponentInChildren<ScratchImage>();
                                if (otherScratchImage == cardScratchImage)
                                    continue;
                            
                                StartCoroutine(Move(other, other.transform.position, other.transform.position + Vector3.down * downShift, innerIndex, disappearCurve, () =>
                                    {
                                        foreach (var itemToEnable in itemsToEnable)
                                            itemToEnable.SetActive(true);
                                    }));
                            }
                        });
                }

                while (!complete)
                    yield return null;

                yield return new WaitForSeconds(4.0f);
            }
        }

        private void SubscribeMinMax(Slider min, Slider max)
        {
            min.onValueChanged.AddListener(value =>
                {
                    if (max.value < min.value)
                        max.value = min.value;
                });

            max.onValueChanged.AddListener(value =>
                {
                    if (min.value > max.value)
                        min.value = max.value;
                });
        }

        private IEnumerator Move(Transform item, Vector3 initialPosition, Vector3 targetPosition, int itemIndex, AnimationCurve curve, Action onComplete = null)
        {
            item.position = initialPosition;

            yield return new WaitForSeconds(appearDelay * itemIndex);

            var current = 0.0f;
            while (current < 1.0f)
            {
                item.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, curve.Evaluate(Mathf.Clamp01(current)));

                current += Time.deltaTime * appearSpeed;

                yield return null;
            }

            item.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, curve.Evaluate(1.0f));

            if (onComplete != null)
                onComplete();
        }
    }
}