INSERT INTO service (cost, deadline, type_of_service)
VALUES
(50000.00, DATE_ADD(CURDATE(), INTERVAL 6 MONTH), 'Кримінальний'),
(20000.00, DATE_ADD(CURDATE(), INTERVAL 3 MONTH), 'Цивільний'),
(8000.00, DATE_ADD(CURDATE(), INTERVAL 1 MONTH), 'Адміністративний');