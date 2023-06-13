CREATE PROCEDURE marketplace.WriteOrder
    @OrderId INT = NULL OUTPUT,
    @Delete BIT = 0,
    @CustomerId INT = NULL,
    @OrderDate DATETIME = NULL,
    @Status VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the Delete flag is set
    IF @Delete = 1
    BEGIN
        -- Delete the order and its associated products
        DELETE FROM marketplace.OrderProduct WHERE OrderId = @OrderId;
        DELETE FROM marketplace.[Order] WHERE OrderId = @OrderId;

        -- Set the output parameter to NULL as the order is deleted
        SET @OrderId = NULL;
    END
    ELSE
    BEGIN
        -- Validate the input parameters
        IF @CustomerId IS NOT NULL AND @OrderDate IS NOT NULL AND @Status IS NOT NULL
        BEGIN
            -- Check if the OrderId is provided for an update operation
            IF @OrderId IS NOT NULL
            BEGIN
                -- Check if the OrderId exists in the table
                IF EXISTS(SELECT 1 FROM marketplace.[Order] WHERE OrderId = @OrderId)
                BEGIN
                    -- Update the existing order
                    UPDATE marketplace.[Order]
                    SET CustomerId = @CustomerId,
                        OrderDate = @OrderDate,
                        [Status] = @Status
                    WHERE OrderId = @OrderId;
                END
                ELSE
                BEGIN
                    -- OrderId does not exist, insert a new order
               		INSERT INTO marketplace.[Order] (OrderId, CustomerId, OrderDate, [Status])
                	VALUES (@OrderId, @CustomerId, @OrderDate, @Status);


                    -- Set the output parameter to the newly created OrderId
                    SET @OrderId = SCOPE_IDENTITY();
                END
            END
           

            -- Call marketplace.WriteOrderProduct if necessary

        END
        ELSE
        BEGIN
            -- Validation failed, raise an error
            RAISERROR('Invalid values for CustomerId, OrderDate, or Status.', 16, 1);
        END
    END
END;
