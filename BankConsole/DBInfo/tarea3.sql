USE Bank;

GO

CREATE PROCEDURE SelectClient
    @ClientID INT = NULL
AS
    IF @ClientID IS NULL
        SELECT * FROM Client
    ELSE
        SELECT * FROM Client
        WHERE ID = @ClientID
GO

ALTER PROCEDURE InsertClient
    @Name VARCHAR(200),
    @PhoneNumber VARCHAR(40),
    @Email VARCHAR(50) = NULL
AS
    DECLARE @ANTERIOR INT = NULL;
    SET @ANTERIOR = (SELECT ID FROM Client WHERE Email = @Email);
    IF @ANTERIOR IS NULL
    BEGIN
        INSERT INTO Client (Name, PhoneNumber, Email)
        VALUES (@Name, @PhoneNumber, @Email);
        RETURN 0
    END
    ELSE
    BEGIN
        PRINT 'Error: Email ya existe'
        RETURN 1
    END
GO

ALTER PROCEDURE InsertBankTransaction
    @AccountID INT,
    @TransactionType INT,
    @Amount DECIMAL(10,2),
    @ExternalAccount INT = NULL
AS
    DECLARE @CurrentBalance DECIMAL(10,2), 
            @NewBalance DECIMAL(10,2);

    BEGIN TRANSACTION
    SET @CurrentBalance = (SELECT Balance FROM Account WHERE ID = @AccountID);

    -- Obtiene nuevo saldo
    IF @TransactionType = 2 OR @TransactionType = 4
        -- Retiros
        SET @NewBalance = @CurrentBalance - @Amount;
    ELSE
        -- Depósitos
        SET @NewBalance = @CurrentBalance + @Amount;

    -- Actualiza saldo
    UPDATE Account
    SET Balance = @NewBalance
    WHERE ID = @AccountID;

    -- Registra transacción
    INSERT INTO BankTransaction 
    (AccountID, TransactionType, Amount, ExternalAccount)
    VALUES
    (@AccountID, @TransactionType, @Amount, @ExternalAccount)
    
    -- Cancelar si nuevo balance es menor a 0
    IF @NewBalance >= 0
        COMMIT TRANSACTION
    ELSE
    BEGIN  
        ROLLBACK
        PRINT 'No hay saldo suficiente para completar la transacción'
		RETURN 1
    END
GO


