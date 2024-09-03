CREATE TABLE m_product (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    plu NVARCHAR(50),
    product_category_id BIGINT,
    active BIT,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
);

CREATE TABLE m_product_category (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    active BIT,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
);
CREATE TABLE m_product_variant (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    product_id BIGINT,
    code NVARCHAR(50),
	name NVARCHAR(50),
	qty BIGINT,
	price DECIMAL,
    active BIT,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
);

CREATE TABLE m_user (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    user_id NVARCHAR(50),
    password NVARCHAR(50),
	email NVARCHAR(50),
	role_id int,
	is_use BIT,
	is_lock BIT,
	last_login DATETIME2,
    active BIT,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
);

CREATE TABLE m_role (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    active BIT,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
);

CREATE TABLE m_menu
(
	id BIGINT IDENTITY(1,1) not null,
	name NVARCHAR(20),
	active bit,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
)
CREATE TABLE m_menu_access
(
	id BIGINT IDENTITY(1,1) not null,
	menu_id BIGINT,
	menu_name NVARCHAR(50),
	role_id BIGINT,
	active bit,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
)

CREATE TABLE m_transaction
(
	id BIGINT IDENTITY(1,1) not null,
	transaction_no NVARCHAR(20),
	total_amount decimal,
	active bit,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
)

CREATE TABLE m_transaction_detail
(
	id BIGINT IDENTITY(1,1) not null,
	transaction_id BIGINT,
	product_variant_id BIGINT,
	price decimal,
	qty int,
	subtotal decimal,
	active bit,
    created_user NVARCHAR(50),
    created_date DATETIME2,
    updated_user NVARCHAR(50),
    updated_date DATETIME2
)