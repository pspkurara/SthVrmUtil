﻿using System.Collections.Generic;
using SthVrmUtil.Runtime.ExpressionWrapper.Interface;
using SthVrmUtil.Runtime.ExpressionWrapper.Internal;
using UnityEngine;
using UniVRM10;

// NOTE: namespace は詐称する
namespace VRM
{
    public class SthVRMBlendShapeProxy : MonoBehaviour, IVirtualVrmBlendShapeProxy
    {
        [SerializeField] private Vrm10Instance _vrm10Instance;
        private Vrm10RuntimeExpressionWrapper _wrapper;

        private void Start()
        {
            if (_vrm10Instance == null)
            {
                _vrm10Instance = GetComponent<Vrm10Instance>();
            }
            _wrapper = new Vrm10RuntimeExpressionWrapper(() => _vrm10Instance.Runtime.Expression);
        }

        private void Reset()
        {
            _vrm10Instance = GetComponent<Vrm10Instance>();
        }

        public IVirtualBlendShapeAvatar BlendShapeAvatar { get; } = new SthBlendShapeAvatar();

        public float GetValue(BlendShapeKey key) => _wrapper.GetWeight(key);

        public float GetValue(BlendShapePreset key) => _wrapper.GetWeight(BlendShapeKey.CreateFromPreset(key));

        public float GetValue(string key) => _wrapper.GetWeight(BlendShapeKey.CreateUnknown(key));

        public IEnumerable<KeyValuePair<BlendShapeKey, float>> GetValues()
        {
            foreach (var x in _wrapper.GetWeights())
            {
                yield return new KeyValuePair<BlendShapeKey, float>(x.Key, x.Weight);
            }
        }

        public void AccumulateValue(BlendShapeKey key, float value) => _wrapper.AccumulateWeight(key, value);

        public void AccumulateValue(BlendShapePreset key, float value) => _wrapper.AccumulateWeight(BlendShapeKey.CreateFromPreset(key), value);

        public void AccumulateValue(string key, float value) => _wrapper.AccumulateWeight(BlendShapeKey.CreateUnknown(key), value);

        public void Apply() => _wrapper.Apply();

        public void SetValue(BlendShapeKey key, float value, bool apply)
        {
            _wrapper.AccumulateWeight(key, value);
            if (apply)
            {
                _wrapper.Apply();
            }
        }

        public void SetValue(BlendShapePreset key, float value, bool apply)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateFromPreset(key), value);
            if (apply)
            {
                _wrapper.Apply();
            }
        }

        public void SetValue(string key, float value, bool apply)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateUnknown(key), value);
            if (apply)
            {
                _wrapper.Apply();
            }
        }

        public void SetValue(BlendShapeKey key, float value)
        {
            _wrapper.AccumulateWeight(key, value);
            _wrapper.Apply();
        }

        public void SetValue(BlendShapePreset key, float value)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateFromPreset(key), value);
            _wrapper.Apply();
        }

        public void SetValue(string key, float value)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateUnknown(key), value);
            _wrapper.Apply();
        }

        public void SetValues(IEnumerable<KeyValuePair<BlendShapeKey, float>> values)
        {
            foreach (var x in values)
            {
                _wrapper.AccumulateWeight(x.Key, x.Value);
            }
            _wrapper.Apply();
        }

        public void ImmediatelySetValue(BlendShapeKey key, float value)
        {
            _wrapper.AccumulateWeight(key, value);
            _wrapper.Apply();
        }

        public void ImmediatelySetValue(BlendShapePreset key, float value)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateFromPreset(key), value);
            _wrapper.Apply();
        }

        public void ImmediatelySetValue(string key, float value)
        {
            _wrapper.AccumulateWeight(BlendShapeKey.CreateUnknown(key), value);
            _wrapper.Apply();
        }
    }
}