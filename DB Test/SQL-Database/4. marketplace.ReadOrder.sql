CREATE PROCEDURE marketplace.ReadOrder
    @FilterXml XML,
    @DataTablesXml XML,
    @Language VARCHAR(2) = 'EN'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CustomerId INT, @OrderId INT, @Status NVARCHAR(20);

    -- Get XML Filter Values
    SELECT
        @CustomerId = FilterXml.value('(//Filter/CustomerId)[1]', 'INT'),
        @OrderId = FilterXml.value('(//Filter/OrderId)[1]', 'INT'),
        @Status = FilterXml.value('(//Filter/Status)[1]', 'NVARCHAR(20)')
    FROM
        @FilterXml.nodes('/Filter') AS F (FilterXml);

    -- 'Order-Header' case
    IF EXISTS (SELECT 1 FROM @DataTablesXml.nodes('/Tables/Table[TableName="Order-Header"]') AS DT (XmlData))
    BEGIN
       -- SELECT TableName = 'Order-Header';

        SELECT
            o.OrderId,
            o.CustomerId,
            c.CustomerName,
            o.OrderDate,
            o.Status
        FROM
            marketplace.[Order] AS o
        JOIN
            marketplace.Customer AS c ON o.CustomerId = c.CustomerId
        WHERE
            (@CustomerId IS NULL OR o.CustomerId = @CustomerId)
            AND (@OrderId IS NULL OR o.OrderId = @OrderId)
            AND (@Status IS NULL OR o.Status = @Status)
    END;

    -- 'Order-Detail' case
    IF EXISTS (SELECT 1 FROM @DataTablesXml.nodes('/Tables/Table[TableName="Order-Detail"]') AS DT (XmlData))
    BEGIN
        --SELECT TableName = 'Order-Detail';

        SELECT
            op.OrderId,
            op.ProductId,
            op.Qty,
            op.UnitPrice,
            op.TotalPrice
        FROM
            marketplace.OrderProduct AS op
        JOIN
            marketplace.[Order] AS o ON op.OrderId = o.OrderId
        JOIN
            marketplace.Customer AS c ON o.CustomerId = c.CustomerId
        WHERE
            (@CustomerId IS NULL OR o.CustomerId = @CustomerId)
            AND (@OrderId IS NULL OR o.OrderId = @OrderId)
            AND (@Status IS NULL OR o.Status = @Status)
    END;

    -- 'Order-History-Json' case
    IF EXISTS (SELECT 1 FROM @DataTablesXml.nodes('/Tables/Table[TableName="Order-History-Json"]') AS DT (XmlData))
    BEGIN
        --SELECT TableName = 'Order-History-Json';

        SELECT
            o.OrderId AS '_id',
            c.CustomerId,
            c.CustomerName,
            ld.Description AS CustomerLocationHierarchy,
            COUNT(op.ProductId) AS ProductsCount,
            SUM(op.TotalPrice) AS TotalOrder
        FROM
            marketplace.[Order] AS o
        JOIN
            marketplace.Customer AS c ON o.CustomerId = c.CustomerId
        JOIN
            marketplace.Location AS l ON c.LocationId = l.LocationId
        JOIN
            marketplace.LocationDescription AS ld ON l.LocationId = ld.LocationId AND ld.Language = @Language
        LEFT JOIN
            marketplace.OrderProduct AS op ON o.OrderId = op.OrderId
        WHERE
            (@CustomerId IS NULL OR o.CustomerId = @CustomerId)
            AND (@OrderId IS NULL OR o.OrderId = @OrderId)
            AND (@Status IS NULL OR o.Status = @Status)
        GROUP BY
            o.OrderId,
            c.CustomerId,
            c.CustomerName,
            ld.Description
        FOR JSON PATH, ROOT('Order-History-Json')
    END;
END;
