EXEC marketplace.WriteOrder @OrderId = 8 , @CustomerId = 1, @OrderDate = '2023-06-12', @Status = 'Close';


EXEC marketplace.WriteOrder @OrderId = 8 , @CustomerId = 1, @OrderDate = '2023-06-12', @Status = 'CLOSE';


EXEC marketplace.WriteOrder @OrderId = 8 , @delete = 1, @CustomerId = 1, @OrderDate = '2023-06-12', @Status = 'CLOSE';
