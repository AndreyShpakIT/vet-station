use Ветстанция

go
CREATE TRIGGER КлиентыОбновление
ON Клиенты
AFTER UPDATE
AS
 ROLLBACK TRAN
