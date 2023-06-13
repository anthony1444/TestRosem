CREATE PROCEDURE marketplace.WriteOrderProduct
    @OrderId int,
    @ProductId int = NULL,
    @Delete bit = 0,
    @Qty int = NULL,
    @UnitPrice money = NULL,
    @TotalPrice money = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ParentStatus NVARCHAR(20);

    -- Get the status of the parent order
    SELECT @ParentStatus = [Status]
    FROM marketplace.[Order]
    WHERE OrderId = @OrderId;

    -- Check if the parent order status is 'OPEN'
    IF @ParentStatus = 'OPEN'
    BEGIN
        IF @Delete = 1
        BEGIN
            -- Perform deletion of the product
            DELETE FROM marketplace.OrderProduct
            WHERE OrderId = @OrderId AND ProductId = @ProductId;
        END
        ELSE
        BEGIN
            -- Validation for creation or update
            IF @Qty IS NOT NULL AND @UnitPrice IS NOT NULL AND @TotalPrice IS NOT NULL
            BEGIN
                -- Perform create or update using MERGE statement
                MERGE marketplace.OrderProduct AS target
                USING (SELECT @OrderId AS OrderId, @ProductId AS ProductId) AS source
                ON (target.OrderId = source.OrderId AND target.ProductId = source.ProductId)
                WHEN MATCHED THEN
                    UPDATE SET
                        target.Qty = @Qty,
                        target.UnitPrice = @UnitPrice,
                        target.TotalPrice = @TotalPrice
                WHEN NOT MATCHED THEN
                    INSERT (OrderId, ProductId, Qty, UnitPrice, TotalPrice)
                    VALUES (@OrderId, @ProductId, @Qty, @UnitPrice, @TotalPrice)
                WHEN NOT MATCHED BY SOURCE THEN
                    DELETE;
            END
            ELSE
            BEGIN
                -- Validation failed, raise an error
                RAISERROR('Invalid values for ProductId, Qty, UnitPrice, or TotalPrice.', 16, 1);
            END
        END
    END
    ELSE
    BEGIN
        -- Parent order status is not 'OPEN', raise an error
        RAISERROR('Parent order status is not open. Cannot perform operations.', 16, 1);
    END
END;
