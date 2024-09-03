CREATE OR ALTER VIEW vw_transaction_detail
AS
Select 
	a.transaction_id as transaction_id,
	b.transaction_no,
	e.name as category,
	c.name,
	a.qty,
	a.subtotal
from 
m_transaction_detail a
join m_transaction b on a.transaction_id  = b.id
join m_product_variant c on a.product_variant_id = c.id
join m_product d on d.id = c.product_id
join m_product_category e on d.product_category_id = e.id;