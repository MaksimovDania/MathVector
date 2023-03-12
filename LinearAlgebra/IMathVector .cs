using System.Collections;
using System.Data.Common;

namespace LinearAlgebra
{
    public interface IMathVector : IEnumerable
    {
        /// <summary>
        /// Получить размерность вектора (количество координат)
        /// </summary>
        /// <returns>Количество чисел содержащихся в векторе</returns>
        int Dimensions { get; }

        /// <summary>
        /// Индексатор для доступа к элементам вектора. Нумерация с нуля
        /// </summary>
        /// <returns> i-тая по порядку координата</returns>
        double this[int i] { get; set; }
        
        /// <summary>
        /// Рассчитать длину (модуль) вектора
        /// </summary>
        /// <returns>Длину вектора - корень из суммы квадратов всех величин</returns>
        double Length { get; }

        /// <summary>
        ///Покомпонентное сложение с числом.
        /// </summary>
        /// <param name="number">Число, которое прибавляем</param>
        /// <returns>Новый вектор который является результатом сложения с number</returns>
        IMathVector SumNumber(double number);

        /// <summary>
        /// Покомпонентное умножение на число.
        /// </summary>
        /// <param name="number">Число, на которое хотим умножим</param>
        /// <returns>Новый вектор который является результатом умножения на number</returns>
        IMathVector MultiplyNumber(double number);

        /// <summary>
        /// Сложение с другим вектором.
        /// </summary>
        /// <param name="vector">Вектор с которым ссумируем</param>
        /// <returns>Новый вектор, который явлеяется результатом действия</returns>
        IMathVector Sum(IMathVector vector);
        
        /// <summary>
        /// Покомпонентное умножение с другим вектором.
        /// </summary>
        /// <param name="vector">Вектор на который умножаем</param>
        /// <returns>Новый вектор, который явлеяется результатом действия</returns>
        IMathVector Multiply(IMathVector vector);

        /// <summary>
        /// Скалярное умножение на другой вектор.
        /// </summary>
        /// <param name="vector">Вектор на который умножаем скалярно</param>
        /// <returns>Новый вектор, который явлеяется результатом действия</returns>
        double ScalarMultiply(IMathVector vector);

        /// <summary>
        /// Вычислить Евклидово расстояние до другого вектора.
        /// </summary>
        /// <param name="vector">Вектор до которого будем вычислять расстояние</param>
        /// <returns>Расстояние между векторами</returns>
        double CalculateDistance(IMathVector vector);

    }
}