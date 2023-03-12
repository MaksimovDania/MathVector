using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LinearAlgebra
{
    public class MathVector : IMathVector
    {
        /// <summary>
        /// Создает пустой вектор с заданным количеством координат
        /// </summary>
        /// <param name="dimensions">Количество координат</param>
        /// <exception cref="ArgumentOutOfRangeException">Кол-во должно быть целым и положительным</exception>
        public MathVector(int dimensions)
        {
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("Dimensions must be more than 0");
            Dimensions = dimensions;
            _vector = new double[Dimensions];
        }
        /// <summary>
        /// Создает вектор из массива вещественных чисел
        /// </summary>
        /// <param name="vector">Массив вещественных чисел</param>
        public MathVector(double[] vector)
        {
            Dimensions = vector.Length;
            _vector = vector;
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Dimensions"/>
        public int Dimensions { get; }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.this"/>
        public double this[int i]
        {
            get
            {
                return _vector[i];
            }
            set
            {
                _vector[i] = value;
            } 
        }

        /// <summary>
        /// Имлементация метода интерфейса IEnumerable
        /// </summary>
        public IEnumerator GetEnumerator() => _vector.GetEnumerator();
        
        private double[] _vector;
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Length"/>
        public double Length
        { 
            get
            {
                double len = 0;
                foreach (var elem in _vector)
                {
                    len += elem * elem;
                }
                return Math.Sqrt(len);
            }
        }
        /// <inheritdoc cref="LinearAlgebra.IMathVector.SumNumber"/>
        public IMathVector SumNumber(double number)
        {
            var newVector = new double[Dimensions];
            for (var i = 0; i < Dimensions; i++)
            {
                newVector[i] = _vector[i] + number;
            }
            return new MathVector(newVector);
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.MultiplyNumber"/>
        public IMathVector MultiplyNumber(double number)
        {
            var newVector = new MathVector(Dimensions);
            for (var i = 0; i < Dimensions; i++)
            {
                newVector[i] = _vector[i] * number;
            }
            return newVector;
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.Sum"/>
        public IMathVector Sum(IMathVector vector)
        {
            DimensionCheck(vector);
            var newVector = new MathVector(Dimensions);
            for (var i = 0; i < Dimensions; i++)
            {
                newVector[i] = this[i] + vector[i];
            }
            return newVector;
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.Multiply"/>
        public IMathVector Multiply(IMathVector vector)
        {
            DimensionCheck(vector);
            var newVector = new MathVector(Dimensions);
            for (var i = 0; i < Dimensions; i++)
            {
                newVector[i] = this[i] + vector[i];
            }
            return newVector;
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.ScalarMultiply"/>
        public double ScalarMultiply(IMathVector vector)
        {
            DimensionCheck(vector);
            double scalar = 0;
            for (var i = 0; i < Dimensions; i++)
            {
                scalar += this[i] + vector[i];
            }
            return scalar;
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.CalculateDistance"/>
        public double CalculateDistance(IMathVector vector)
        {
            DimensionCheck(vector);
            double distance = 0;
            for (var i = 0; i < Dimensions; i++)
            {
                distance += (this[i] - vector[i]) * (this[i] - vector[i]);
            }
            return Math.Sqrt(distance);
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.SumNumber"/>
        public static IMathVector operator +(MathVector vector, double number)
        {
            return vector.SumNumber(number);
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Sum"/>
        public static IMathVector operator +(MathVector vector1, IMathVector vector2)
        {
            if (vector1.Dimensions != vector2.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
            return vector1.Sum(vector2);
        }
        /// <inheritdoc cref="LinearAlgebra.IMathVector.SumNumber"/>
        public static IMathVector operator -(MathVector vector, double number)
        {
            return vector.SumNumber(-number);
        }
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Sum"/>
        public static IMathVector operator -(MathVector vector1, IMathVector vector2)
        {
            if (vector1.Dimensions != vector2.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
            return vector1.Sum(vector2.MultiplyNumber(-1));
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.MultiplyNumber"/>
        public static IMathVector operator *(MathVector vector, double number)
        {
            return vector.MultiplyNumber(number);
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Multiply"/>
        public static IMathVector operator *(MathVector vector1, IMathVector vector2)
        {
            if (vector1.Dimensions != vector2.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
            return vector1.Multiply(vector2);
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.MultiplyNumber"/>
        public static IMathVector operator /(MathVector vector, double number)
        {
            if (number == 0) throw new DivideByZeroException(); 
            return vector.SumNumber(1/number);
        }
        
        /// <inheritdoc cref="LinearAlgebra.IMathVector.Multiply"/>
        public static IMathVector operator /(MathVector vector1, IMathVector vector2)
        {
            if (vector1.Dimensions != vector2.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
            var vector3 = new MathVector(vector1.Dimensions);
            for (var i = 0; i < vector1.Dimensions; i++)
            {
                if (vector2[i] == 0) throw new DivideByZeroException("There was a zero in second vector");
                vector3[i] = vector1[i] / vector2[i];
            }
            return vector3;
        }

        /// <inheritdoc cref="LinearAlgebra.IMathVector.ScalarMultiply"/>
        public static double operator %(MathVector vector1, IMathVector vector2)
        {
            if (vector1.Dimensions != vector2.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
            return vector1.ScalarMultiply(vector2);
        }

        private void DimensionCheck(IMathVector vector)
        {
            if (Dimensions != vector.Dimensions) throw new ArgumentException("Vectors must have same dimensions");
        }
    }
}
