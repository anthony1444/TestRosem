A)

DECLARE @FilterXml XML, @DataTablesXml XML;

-- Datos de filtro para obtener la información del 'Order-Header'
SET @FilterXml = '
<Filter>
    <CustomerId>1</CustomerId>
    <OrderId>1</OrderId>
    <Status>CLOSE</Status>
</Filter>';

-- Datos de tablas para obtener la información del 'Order-Header'
SET @DataTablesXml = '
<Tables>
    <Table>
        <TableName>Order-Header</TableName>
    </Table>
</Tables>';

-- Ejecutar el procedimiento almacenado para obtener la información del 'Order-Header'
EXEC marketplace.ReadOrder @FilterXml, @DataTablesXml;

B)
DECLARE @FilterXml XML, @DataTablesXml XML;

SET @FilterXml = '
<Filter>
    <CustomerId>2</CustomerId>
    <OrderId>5</OrderId>
    <Status>OPEN</Status>
</Filter>';

-- Datos de tablas para obtener la información del 'Order-Header'
SET @DataTablesXml = '
<Tables>
    <Table>
        <TableName>Order-Detail</TableName>
    </Table>
</Tables>';

-- Ejecutar el procedimiento almacenado para obtener la información del 'Order-Header'
EXEC marketplace.ReadOrder @FilterXml, @DataTablesXml;

C) DECLARE @FilterXml XML, @DataTablesXml XML;

-- Datos de filtro para obtener la información del 'Order-Header'
SET @FilterXml = '
<Filter>
    <CustomerId>1</CustomerId>
    <OrderId>4</OrderId>
    <Status>OPEN</Status>
</Filter>';

-- Datos de tablas para obtener la información del 'Order-Header'
SET @DataTablesXml = '
<Tables>
    <Table>
        <TableName>Order-History-Json</TableName>
    </Table>
</Tables>';

-- Ejecutar el procedimiento almacenado para obtener la información del 'Order-Header'
EXEC marketplace.ReadOrder @FilterXml, @DataTablesXml, @Language = 'ES';


