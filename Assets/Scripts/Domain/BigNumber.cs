using System;

namespace MyCat.Domain
{
    public struct BigNumber
    {
        public double Value;
        public long Exponent;

        public BigNumber(double value, long exponent)
        {
            Value = value;
            Exponent = exponent;
            Normalize();
        }

        public BigNumber Add(BigNumber other)
        {
            BigNumber result = this;
            BigNumber small = other;

            // 더 큰 숫자 위주로 계산을 해야 정밀도 손실을 최소화 할 수 있다.
            if (result.Exponent < small.Exponent)
            {
                result = other;
                small = this;
            }

            long expDiff = result.Exponent - small.Exponent;
            if (expDiff > 15) // double 의 정밀도를 고려한 값
            {
                // 15 이상의 지수 차이가 나면 무의미하므로 큰숫자를 그대로 반영함
                return result;
            }

            // 단위가 큰 숫자를 기준으로 계산하기 위해 단위를 맞춤
            double scaledSmallValue = small.Value / Math.Pow(10, expDiff);

            result.Value += scaledSmallValue;
            result.Normalize(); // 지수 보정

            return result;
        }

        public BigNumber Multiply(BigNumber other)
        {
            long newExp = this.Exponent + other.Exponent;
            double newValue = this.Value * other.Value;
            return new BigNumber(newValue, newExp);
        }

        // 숫자를 항상 1.0 <= Value < 10.0 으로 만드는 정규화 함수
        private void Normalize()
        {
            if (Value == 0)
            {
                Exponent = 0;
                return;
            }

            while (Value >= 10.0)
            {
                Value /= 10.0;
                Exponent++;
            }

            while (Value < 1.0 && Exponent > 0)
            {
                Value *= 10.0;
                Exponent--;
            }
        }
    }
}