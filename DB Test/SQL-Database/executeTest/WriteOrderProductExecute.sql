EXEC marketplace.WriteOrderProduct
    @OrderId = 5,
    @ProductId = 2001,
    @Qty = 5,
    @UnitPrice = 10.99,
    @TotalPrice = 54.95;

EXEC marketplace.WriteOrderProduct
    @OrderId = 5,
    @ProductId = 2001,
    @Qty = 10,
    @UnitPrice = 12.99,
    @TotalPrice = 129.90;

EXEC marketplace.WriteOrderProduct
    @OrderId = 5,
    @ProductId = 2001,
    @Delete = 1,
    @Qty = NULL,
    @UnitPrice = NULL,
    @TotalPrice = NULL;
