if not exists(select * From dbo.tblPlan)
Begin

	insert into dbo.tblPlan(PlanId,EventDescription,PlanDate,CalendarYear)
select newid(), 'Start onions on porch','2017-01-31', 2017
union all select newid(), 'Start lettuce','2017-02-21', 2017
union all select newid(), 'Start shallots on porch','2017-03-14', 2017
union all select newid(), 'Start lettuce','2017-03-14', 2017
union all select newid(), 'Start broccoli raab on porch','2017-03-14', 2017
union all select newid(), 'Start broccoli on porch','2017-03-14', 2017
union all select newid(), 'Start cauliflower on porch','2017-03-14', 2017
union all select newid(), 'Start cabbage','2017-03-14', 2017
union all select newid(), 'Start thyme','2017-03-14', 2017
union all select newid(), 'Start basil','2017-03-14', 2017
union all select newid(), 'Start Parsley','2017-03-14', 2017
union all select newid(), 'Start rosemary','2017-03-14', 2017
union all select newid(), 'Start green peppers','2017-03-21', 2017
union all select newid(), 'Start red peppers','2017-03-21', 2017
union all select newid(), 'Start eggplant','2017-03-21', 2017
union all select newid(), 'Start 100 cherry tomato','2017-03-21', 2017
union all select newid(), 'Start early tomato','2017-03-21', 2017
union all select newid(), 'Start roma tomato','2017-03-21', 2017
union all select newid(), 'Start pink tomato','2017-03-21', 2017
union all select newid(), 'Start lemon drop','2017-03-21', 2017
union all select newid(), 'Start amish paste','2017-03-21', 2017
union all select newid(), 'Start Japanese Trifle','2017-03-21', 2017
union all select newid(), 'Start ground cherry','2017-03-21', 2017
union all select newid(), 'Start anaheim chili','2017-03-21', 2017
union all select newid(), 'Start specialty hot peppers','2017-03-21', 2017
union all select newid(), 'Start jalapeno','2017-03-21', 2017
union all select newid(), 'Start brussel sprouts','2017-03-21', 2017
union all select newid(), 'Plant spinach','2017-03-28', 2017
union all select newid(), 'Plant carrots','2017-03-28', 2017
union all select newid(), 'Plant peas','2017-04-04', 2017
union all select newid(), 'Plant carrots','2017-04-04', 2017
union all select newid(), 'Plant chard','2017-04-04', 2017
union all select newid(), 'Plant lettuce','2017-04-04', 2017
union all select newid(), 'Plant spinach','2017-04-04', 2017
union all select newid(), 'Plant beets in garden','2017-04-04', 2017
union all select newid(), 'Plant turnips','2017-04-04', 2017
union all select newid(), 'Plant Brussels Sprouts','2017-04-04', 2017
union all select newid(), 'Start butternut','2017-04-11', 2017
union all select newid(), 'Start zinnia','2017-04-11', 2017
union all select newid(), 'Start moss rose','2017-04-11', 2017
union all select newid(), 'Start marigold','2017-04-11', 2017
union all select newid(), 'Transplant broccoli raab','2017-04-11', 2017
union all select newid(), 'Plant Pak Choi','2017-04-11', 2017
union all select newid(), 'Plant potatoes','2017-04-11', 2017
union all select newid(), 'Plant carrots','2017-04-18', 2017
union all select newid(), 'Plant chard','2017-04-18', 2017
union all select newid(), 'Plant lettuce','2017-04-18', 2017
union all select newid(), 'Plant spinach','2017-04-18', 2017
union all select newid(), 'Plant beets in garden','2017-04-18', 2017
union all select newid(), 'Plant peas','2017-04-18', 2017
union all select newid(), 'Start beets in garden','2017-04-25', 2017
union all select newid(), 'Start summer squash','2017-04-25', 2017
union all select newid(), 'Start zucchini','2017-04-25', 2017
union all select newid(), 'Plant green onions','2017-04-25', 2017
union all select newid(), 'Plant carrots','2017-05-02', 2017
union all select newid(), 'Plant chard','2017-05-02', 2017
union all select newid(), 'Plant lettuce','2017-05-02', 2017
union all select newid(), 'Plant spinach','2017-05-02', 2017
union all select newid(), 'Transplant broccoli','2017-05-02', 2017
union all select newid(), 'Transplant cauliflower','2017-05-02', 2017
union all select newid(), 'Transplant onions','2017-05-02', 2017
union all select newid(), 'Plant rutabaga','2017-05-02', 2017
union all select newid(), 'Plant green beans','2017-05-09', 2017
union all select newid(), 'Plant soybeans','2017-05-09', 2017
union all select newid(), 'Plant wax beans','2017-05-09', 2017
union all select newid(), 'Start cucumber','2017-05-09', 2017
union all select newid(), 'Transplant basil','2017-05-09', 2017
union all select newid(), 'Transplant rosemary','2017-05-09', 2017
union all select newid(), 'Transplant sage','2017-05-09', 2017
union all select newid(), 'Plant carrots','2017-05-16', 2017
union all select newid(), 'Plant chard','2017-05-16', 2017
union all select newid(), 'Plant calypso','2017-05-16', 2017
union all select newid(), 'Plant lettuce','2017-05-16', 2017
union all select newid(), 'Plant spinach','2017-05-16', 2017
union all select newid(), 'Transplant marigold','2017-05-16', 2017
union all select newid(), 'Transplant thyme','2017-05-16', 2017
union all select newid(), 'Transplant butternut','2017-05-23', 2017
union all select newid(), 'Transplant summer squash','2017-05-23', 2017
union all select newid(), 'Transplant zucchini','2017-05-23', 2017
union all select newid(), 'Plant carrots','2017-05-30', 2017
union all select newid(), 'Plant green beans','2017-05-30', 2017
union all select newid(), 'Plant wax beans','2017-05-30', 2017
union all select newid(), 'Transplant tomatoes','2017-05-30', 2017
union all select newid(), 'Transplant jalapeno','2017-05-30', 2017
union all select newid(), 'Plant pole beans','2017-05-30', 2017
union all select newid(), 'Transplant anaheim chili','2017-05-30', 2017
union all select newid(), 'Transplant green peppers','2017-05-30', 2017
union all select newid(), 'Transplant cucumber','2017-06-06', 2017
union all select newid(), 'Transplant eggplant','2017-06-06', 2017
union all select newid(), 'Plant carrots','2017-06-13', 2017
union all select newid(), 'Plant green beans','2017-06-20', 2017
union all select newid(), 'Plant wax beans','2017-06-22', 2017
union all select newid(), 'Plant carrots','2017-06-29', 2017
union all select newid(), 'Plant carrots','2017-07-11', 2017
union all select newid(), 'Plant green beans','2017-07-11', 2017
union all select newid(), 'Plant wax beans','2017-07-11', 2017
union all select newid(), 'Plant carrots','2017-07-25', 2017
union all select newid(), 'Plant green beans','2017-08-01', 2017
union all select newid(), 'Plant wax beans','2017-08-01', 2017

End