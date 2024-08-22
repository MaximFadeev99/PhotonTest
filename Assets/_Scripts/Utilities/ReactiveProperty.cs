using System;

namespace PhotonTest.Utilities
{
    public class ReactiveProperty<TValue, TOwner>
    {
        private readonly TOwner _owner;

        private TValue _value;
        private Action<TValue, TOwner> _valueChanged;

        public TValue Value
        {
            //There is no easy way to set Value only from the script which creates a reactive property.
            //However, declaring a ReactiveProperty as property
            //(for example, public ReactiveProperty<int, Player> Health {get; private set} ) will allow easy tracking of any script 
            //which will try to reassign Value of the ReactiveProperty
            set 
            {
                _value = value;
                _valueChanged?.Invoke(_value, _owner);
            }
            get 
            {
                return _value; 
            }
        }     

        public ReactiveProperty(TValue value, TOwner owner) 
        {
            Value = value;
            _owner = owner;
        }

        public void Subscribe(Action<TValue, TOwner> callback) 
        {
            _valueChanged += callback;
        }

        public void Unsubscribe(Action<TValue, TOwner> callback)
        {
            _valueChanged -= callback;
        }
    }
}