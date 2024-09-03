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
    ('user1', 'password123', 'user1@example.com', 1, 1, 0, GETDATE(), 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('user2', 'password456', 'user2@example.com', 2, 1, 0, GETDATE(), 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_role
INSERT INTO m_role (name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('sysadmin', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('User', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_menu
--truncate table m_menu
INSERT INTO m_menu (name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    ('Products', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    ('Product Category', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
    ('Product Variant', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	('Manage Transaction', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	('Transaction', 1, 'sysadmin', GETDATE(), 'sysadmin', GETDATE());

-- Insert data into m_menu_access
--truncate table m_menu_access
INSERT INTO m_menu_access (role_id,menu_id,menu_name, active, created_user, created_date, updated_user, updated_date)
VALUES 
    (1,1,'Products',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()), 
    (1,2,'Product Category',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(1,3,'Product Variant',1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(2,4,'Manage Transaction' ,1,'sysadmin', GETDATE(), 'sysadmin', GETDATE()),
	(2,5,'Transaction' ,1,'sysadmin', GETDATE(), 'sysadmin', GETDATE());
