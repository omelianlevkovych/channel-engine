﻿using ChannelEngine.ChannalEngineApi.Orders;

namespace ChannelEngine.Exceptions
{
    public class OrderStatusIsNotSupportedException : ChannelEngineException
    {
        public OrderStatus OrderStatus { get; }

        public OrderStatusIsNotSupportedException(OrderStatus orderStatus)
            : base($"Order status {orderStatus} is not supported.") => OrderStatus = orderStatus;
    }
}