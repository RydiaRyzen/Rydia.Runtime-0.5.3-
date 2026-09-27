using System;

namespace Rydia.Serialization.Graph
{
    internal class ConditionalBinding : Binding
    {
        private readonly object _conditionalValue;
        private readonly Type _conditionalValueType;
        private readonly ComparisonOperator _comparisonOperator;

        public ConditionalBinding(SerializeWhenAttribute attribute, int level)
            : base(attribute, level)
        {
            this._conditionalValue = attribute.Value;

            if (this._conditionalValue != null)
            {
                this._conditionalValueType = this._conditionalValue.GetType();
            }

            this._comparisonOperator = attribute.Operator;
        }

        public bool IsSatisfiedBy(object value)
        {
            switch (this._comparisonOperator)
            {
                case ComparisonOperator.Equal:
                    return AreEqual(value);
                case ComparisonOperator.NotEqual:
                    return !AreEqual(value);
                case ComparisonOperator.LessThan:
                    return Compare(value, (lhs, rhs) => lhs < rhs);
                case ComparisonOperator.GreaterThan:
                    return Compare(value, (lhs, rhs) => lhs > rhs);
                case ComparisonOperator.LessThanOrEqual:
                    return Compare(value, (lhs, rhs) => lhs <= rhs);
                case ComparisonOperator.GreaterThanOrEqual:
                    return Compare(value, (lhs, rhs) => lhs >= rhs);
                default: throw new NotSupportedException();
            }
        }

        private bool AreEqual(object value)
        {
            if (this._conditionalValue == null && value == null)
            {
                return true;
            }

            if (this._conditionalValue == null || value == null)
            {
                return false;
            }

            var convertedValue = value.ConvertTo(this._conditionalValueType);

            return convertedValue.Equals(this._conditionalValue);
        }

        private bool Compare(object value, Func<double, double, bool> comparator)
        {
            if (value == null || this._conditionalValue == null)
            {
                throw new InvalidOperationException("Unable to compare null values");
            }

            var convertedValue = value.ConvertTo(this._conditionalValueType);

            var lhs = Convert.ToDouble(convertedValue);
            var rhs = Convert.ToDouble(this._conditionalValue);

            return comparator(lhs, rhs);
        }
    }
}