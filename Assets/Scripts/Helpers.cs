using UnityEngine;

public static class HelperExtensionMethods
{
    /// <summary>
    /// Возвращает рандомный индекс массива, отличный от поданного на вход.
    /// </summary>
    /// <param name="index">Исходный индекс</param>
    /// <param name="arrayLength">Длина массива</param>
    /// <returns></returns>
    public static int ChangeArrayIndexRandomlyNoRepeat(this int index, int arrayLength)
    {
        if (arrayLength < 1 || index >= arrayLength || index < 0)
        {
            throw new System.IndexOutOfRangeException();
        }

        if (arrayLength == 1)
        {
            return index;
        }

        if (arrayLength == 2)
        {
            return arrayLength - index - 1;
        }

        if (index == 0)
        {
            return Random.Range(index + 1, arrayLength);
        }

        if (index == arrayLength - 1)
        {
            return Random.Range(0, arrayLength - 1);
        }

        //просто шанс 50 на 50
        if (Random.Range(0, 2) == 0)
        {
            return Random.Range(0, index);
        }
        else
        {
            return Random.Range(index, arrayLength);
        }
    }

    /// <summary>
    /// Возвращает поданный на вход индекс, сдвигая его на заданную величину.
    /// Если итоговый индекс переваливает через крайнее возможное значение (ноль или длина массива),
    /// он возвращается в массив "с другой стороны", т.е. эмулируется как бы замкнутый массив.
    /// Грубо говоря, при индексе 3, длине массива 5, и величине сдвига -4 мы получим на выходе 4.
    /// </summary>
    /// <param name="index">Исходный индекс</param>
    /// <param name="arrayLength">Длина массива</param>
    /// <param name="indexMoveValue">Величина сдвига индекса</param>
    /// <returns></returns>
    public static int ChangeClosedLoopArrayIndex(this int index, int arrayLength, int indexMoveValue)
    {
        index += indexMoveValue;

        while (index < 0 || index >= arrayLength)
        {
            if (index < 0)
            {
                index += arrayLength;
            }
            else
            {
                index -= arrayLength;
            }
        }

        return index;
    }
}


