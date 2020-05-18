using System.Collections.Generic;

namespace OrdersAndExecution
{

    public class OrdersSequence
    {
        private OrderSO[] _orders;
        private Queue<OrderSO> _currentOrdersSequence;
        private int _currentOrdersInSequenceCount;

        public OrdersSequence(OrderSO[] arrayOfAllPossibleOrders, int initialOrdersInSequenceCount)
        {
            _orders = arrayOfAllPossibleOrders;
            _currentOrdersSequence = new Queue<OrderSO>();
            Reset(initialOrdersInSequenceCount);            
        }


        public void Reset(int initialOrdersInSequenceCount)
        {
            _currentOrdersSequence.Clear();
            _currentOrdersInSequenceCount = initialOrdersInSequenceCount;
        }

        public OrderSO GetNextOrder()
        {
            return _currentOrdersSequence.Dequeue();
        }

        public bool IsSequenceEmpty()
        {
            return _currentOrdersSequence.Count == 0;
        }

        public void ChangeOrdersInSequenceCountByAmount(int amount)
        {
            _currentOrdersInSequenceCount += amount;
        }

        public void CreateNewSequence()
        {
            _currentOrdersSequence.Clear();

            int sameOrderConsecutiveCount = 1;
            OrderSO previousOrder = default;

            for (int i = 0; i < _currentOrdersInSequenceCount; i++)
            {
                int randomOrderIndex = UnityEngine.Random.Range(0, _orders.Length);
                OrderSO randomOrder = _orders[randomOrderIndex];

                if (_currentOrdersSequence.Count > 0)
                {
                    CheckOrderRepeatsAndChangeOrderIfNeeded(previousOrder, ref sameOrderConsecutiveCount, ref randomOrderIndex, ref randomOrder);
                }

                _currentOrdersSequence.Enqueue(randomOrder);
                previousOrder = randomOrder;
            }
        }

        private void CheckOrderRepeatsAndChangeOrderIfNeeded(OrderSO previousOrder, ref int sameOrderConsecutiveCount, ref int randomOrderIndex, ref OrderSO randomOrder)
        {
            if (previousOrder == randomOrder)
            {
                sameOrderConsecutiveCount++;

                if (sameOrderConsecutiveCount > randomOrder.MaxIssuedConsecutively)
                {
                    randomOrderIndex = randomOrderIndex.ChangeArrayIndexRandomlyNoRepeat(_orders.Length);
                    randomOrder = _orders[randomOrderIndex];

                    sameOrderConsecutiveCount = 1;
                }
            }
            else
            {
                sameOrderConsecutiveCount = 1;
            }
        }
    }
}