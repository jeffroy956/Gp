if not exists(select * From dbo.tblFamilies)
Begin
	print 'filling tblFamilies'

	insert into dbo.tblFamilies(FamilyID, name)
	select 1 as FamilyID, 'basil' as Name
	union all select 2 as FamilyID, 'bean, bush' as Name
	union all select 3 as FamilyID, 'bean, pole' as Name
	union all select 4 as FamilyID, 'beans' as Name
	union all select 5 as FamilyID, 'beet' as Name
	union all select 6 as FamilyID, 'broccoli' as Name
	union all select 7 as FamilyID, 'cabbage' as Name
	union all select 8 as FamilyID, 'carrot' as Name
	union all select 9 as FamilyID, 'cauliflower' as Name
	union all select 10 as FamilyID, 'cucumber' as Name
	union all select 11 as FamilyID, 'eggplant' as Name
	union all select 12 as FamilyID, 'green beans' as Name
	union all select 13 as FamilyID, 'lettuce' as Name
	union all select 14 as FamilyID, 'marigold' as Name
	union all select 15 as FamilyID, 'moss rose' as Name
	union all select 16 as FamilyID, 'onion' as Name
	union all select 17 as FamilyID, 'parsley' as Name
	union all select 18 as FamilyID, 'pea' as Name
	union all select 19 as FamilyID, 'pepper' as Name
	union all select 20 as FamilyID, 'potato' as Name
	union all select 21 as FamilyID, 'red ace' as Name
	union all select 22 as FamilyID, 'rosemary' as Name
	union all select 23 as FamilyID, 'sage' as Name
	union all select 24 as FamilyID, 'spinach' as Name
	union all select 25 as FamilyID, 'squash' as Name
	union all select 26 as FamilyID, 'summer squash' as Name
	union all select 27 as FamilyID, 'thyme' as Name
	union all select 28 as FamilyID, 'tomato' as Name
	union all select 29 as FamilyID, 'turnip' as Name
	union all select 30 as FamilyID, 'wax beans' as Name
	union all select 31 as FamilyID, 'zinnia' as Name
	union all select 32 as FamilyID, 'zucchini' as Name
End

GO