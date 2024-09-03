-- Insert data into m_product_category
INSERT INTO m_product_category (name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('Electronics', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('Furniture', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_product
INSERT INTO m_product (plu, product_category_id, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('PLU123456', 1, 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('PLU654321', 2, 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_product_variant
INSERT INTO m_product_variant (product_id, code, name, qty, price, active, created_user, created_date, updated_user, updated_date)
VALUES 
    (1, 'CODE123', 'Smartphone', 100, 299.99, 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    (2, 'CODE456', 'Sofa', 50, 499.99, 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_user
INSERT INTO m_user (user_id, password, email, role_id, is_use, is_lock, last_login, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('sysadmin', 'qRizh/nbpiQW3o65B/eXuA==', 'sysadmin@gmail.com', 1, 1, 0, GETDATE(), 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('user1', 'qRizh/nbpiQW3o65B/eXuA==', 'user1@example.com', 2, 1, 0, GETDATE(), 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_role
INSERT INTO m_role (name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('sysadmin', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('User', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_menu
--truncate table m_menu
INSERT INTO m_menu (name,path, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('Products','/products', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('Product Category','/products-category', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
    ('Product Variant','/product-variant', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	('Manage Transaction','/manage-transactions', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	('Transaction','/transactions', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_menu_access
--truncate table m_menu_access
INSERT INTO m_menu_access (role_id,menu_id,menu_name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    (1,1,'Products',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    (1,2,'Product Category',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(1,3,'Product Variant',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(2,4,'Manage Transaction' ,1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(2,5,'Transaction' ,1,'sysadmin', GETDATE(), 'sysadmin', GETDATE());


	
INSERT INTO m_transaction 
(
    transaction_no,
    total_amount,
    active,
    created_user,
    created_date,
    updated_user,
    updated_date
)
VALUES
(
    'TXN12345',          -- Replace with actual transaction number
    1000.00,             -- Replace with actual total amount
    1,                  -- Replace with actual active status (1 for true, 0 for false)
    'John Doe',          -- Replace with actual created user
    GETDATE(),           -- Replace with actual created date if needed
    'Jane Smith',        -- Replace with actual updated user
    GETDATE()            -- Replace with actual updated date if needed
);
--truncate table m_transaction_detail
INSERT INTO m_transaction_detail
(
    transaction_id,
    product_variant_id,
    price,
    qty,
    subtotal,
    active,
    created_user,
    created_date,
    updated_user,
    updated_date
)
VALUES
(
    1,                  -- Replace with actual transaction_id (must match an ID in m_transaction)
    1,                -- Replace with actual product_variant_id
    50.00,              -- Replace with actual price
    2,                  -- Replace with actual quantity
    10000.00,             -- Replace with actual subtotal (price * qty)
    1,                  -- Replace with actual active status (1 for true, 0 for false)
    'User1',    -- Replace with actual created user
    GETDATE(),          -- Replace with actual created date if needed
    'User1',        -- Replace with actual updated user
    GETDATE()           -- Replace with actual updated date if needed
);
