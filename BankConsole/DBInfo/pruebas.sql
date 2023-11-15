USE Bank;
-- Probando SelectClient
EXEC SelectClient;
EXEC SelectClient @ClientID=5
-- Probando Insert client con repetido
EXEC InsertClient @Name = 'John', @PhoneNumber = '4324323', @Email = 'john@gmail.com'
EXEC InsertClient @Name = 'John', @PhoneNumber = '4324323', @Email = 'jon@gmail.com'
EXEC SelectClient;
-- Probando Transacción de Banco
EXEC InsertBankTransaction @AccountID = 2, @TransactionType = 2, @Amount = 10500
SELECT * FROM BankTransaction
EXEC SelectAccount
-- Backup
BACKUP DATABASE Bank
TO DISK = 'C:\Users\Funxb\Banco.bak';