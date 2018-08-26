--INSERT INTO [__EFMigrationsHistory] (MigrationId,ProductVersion) VALUES ('20170920005310_Initial','2.0.0-rtm-26452');
INSERT INTO [dbo].[Orgs] (Id,CreatedAt,Identifier,Metadata,[Name],ParentId,ParentOrgId,[Status],[Type],UpdatedAt) VALUES ('org-1','2018-01-11 11:46:22.3487331','CONTOSO-SUPERHEROES',NULL,'Superhero School District',NULL,NULL,0,2,'2018-01-11 11:46:22.3487328');
INSERT INTO [dbo].[Orgs] (Id,CreatedAt,Identifier,Metadata,[Name],ParentId,ParentOrgId,[Status],[Type],UpdatedAt) VALUES ('org-2','2018-01-11 11:46:22.3494052','CONTOSO-PREP',NULL,'Superhero Prep School',NULL,'org-1',0,1,'2018-01-11 11:46:22.3494045');

INSERT INTO [Users] (Id,CreatedAt,Email,EnabledUser,FamilyName,GivenName,Identifier,Metadata,MiddleName,[Password],Phone,[Role],SMS,[Status],UpdatedAt,Username,_grades,_userIds) VALUES ('user-2','2018-01-11 11:46:22.4907365','brian@norreka.se',1,'Fakeperson','Brian','legacy-identifier-user-2',NULL,'Knot',NULL,'+1 345 678 9012',7,'+1 345 678 9012',0,'2018-01-11 11:46:22.4907361','brianf','["UG"]','[{"Type":"legacy-system-1","Identifier":"legacy-identifier-user-2"},{"Type":"legacy-system-0","Identifier":"brian+fakeperson+teacher+1995"}]');
INSERT INTO [Users] (Id,CreatedAt,Email,EnabledUser,FamilyName,GivenName,Identifier,Metadata,MiddleName,[Password],Phone,[Role],SMS,[Status],UpdatedAt,Username,_grades,_userIds) VALUES ('user-1','2018-01-11 11:46:22.4793573','alice@norreka.se',1,'Realperson','Alice','legacy-identifier-user-1',NULL,'Pretend','cGFzc3dvcmQ=','+1 234 567 8901',6,'+1 234 567 8901',0,'2018-01-11 11:46:22.4793569','alicer','["UG"]','[{"Type":"legacy-system-1","Identifier":"legacy-identifier-user-1"},{"Type":"personal-email","Identifier":"alice.realperson@emailprovider.com"}]');
INSERT INTO [Users] (Id,CreatedAt,Email,EnabledUser,FamilyName,GivenName,Identifier,Metadata,MiddleName,[Password],Phone,[Role],SMS,[Status],UpdatedAt,Username,_grades,_userIds) VALUES ('user-4','2018-01-11 11:46:22.490762','admin@norreka.se',1,'Admin','Contoso','root-admin',NULL,NULL,NULL,NULL,0,NULL,0,'2018-01-11 11:46:22.490762','admin@norreka.se','[]',NULL);
INSERT INTO [Users] (Id,CreatedAt,Email,EnabledUser,FamilyName,GivenName,Identifier,Metadata,MiddleName,[Password],Phone,[Role],SMS,[Status],UpdatedAt,Username,_grades,_userIds) VALUES ('user-3','2018-01-11 11:46:22.490758','eve@norreka.se',1,'Reallyfakeperson','Eve','legacy-system-user-3',NULL,NULL,NULL,NULL,7,NULL,0,'2018-01-11 11:46:22.4907577','ever','["09","10","11","12","UG"]','[{"Type":"legacy-system-1","Identifier":"legacy-identifier-user-3"}]');

INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-1','2018-01-11 11:46:22.5590382',NULL,'org-2',0,'2018-01-11 11:46:22.5590378','user-1');
INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-2','2018-01-11 11:46:22.5592544',NULL,'org-2',0,'2018-01-11 11:46:22.559254','user-2');
INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-3','2018-01-11 11:46:22.5592551',NULL,'org-1',0,'2018-01-11 11:46:22.5592548','user-3');
INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-4','2018-01-11 11:46:22.5592562',NULL,'org-2',0,'2018-01-11 11:46:22.5592559','user-3');
INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-5','2018-01-11 11:46:22.559257',NULL,'org-1',0,'2018-01-11 11:46:22.559257','user-4');
INSERT INTO [UserOrgs] (Id,CreatedAt,Metadata,OrgId,[Status],UpdatedAt,UserId) VALUES ('user-org-6','2018-01-11 11:46:22.559258',NULL,'org-2',0,'2018-01-11 11:46:22.5592577','user-4');

INSERT INTO [AcademicSessions] (Id,CreatedAt,EndDate,Metadata,ParentAcademicSessionId,SchoolYear,StartDate,[Status],Title,[Type],UpdatedAt) VALUES ('academic-session-1','2018-01-11 11:46:22.2863953','2017-06-15 00:00:00',NULL,NULL,'2017','2017-02-15 00:00:00',2,'Spring Term',3,'2018-01-11 11:46:22.2863953');
INSERT INTO [AcademicSessions] (Id,CreatedAt,EndDate,Metadata,ParentAcademicSessionId,SchoolYear,StartDate,[Status],Title,[Type],UpdatedAt) VALUES ('academic-session-2','2018-01-11 11:46:22.2934506','2017-08-21 00:00:00',NULL,NULL,'2017','2017-06-21 00:00:00',0,'Summer Term',3,'2018-01-11 11:46:22.2934506');
INSERT INTO [AcademicSessions] (Id,CreatedAt,EndDate,Metadata,ParentAcademicSessionId,SchoolYear,StartDate,[Status],Title,[Type],UpdatedAt) VALUES ('academic-session-3','2018-01-11 11:46:22.2935611','2017-07-20 00:00:00',NULL,'academic-session-2','2017','2017-06-21 00:00:00',0,'Summer First Half',0,'2018-01-11 11:46:22.2934729');
INSERT INTO [AcademicSessions] (Id,CreatedAt,EndDate,Metadata,ParentAcademicSessionId,SchoolYear,StartDate,[Status],Title,[Type],UpdatedAt) VALUES ('academic-session-4','2018-01-11 11:46:22.2939845','2017-08-21 00:00:00',NULL,'academic-session-2','2017','2017-07-21 00:00:00',0,'Summer Second Half',0,'2018-01-11 11:46:22.2939842');

INSERT INTO [Courses] (Id,CourseCode,CreatedAt,Metadata,OrgId,SchoolYearAcademicSessionId,[Status],Title,UpdatedAt,_grades,_subjectCodes,_resources) VALUES ('course-1','CAF304','2018-01-11 11:46:22.39584',NULL,'org-2','academic-session-2',0,'Advanced Caffeination Techniques','2018-01-11 11:46:22.3958393','["13","UG"]','["03098"]','["resource-1","resource-2"]');
INSERT INTO [Courses] (Id,CourseCode,CreatedAt,Metadata,OrgId,SchoolYearAcademicSessionId,[Status],Title,UpdatedAt,_grades,_subjectCodes,_resources) VALUES ('course-2','SUP201','2018-01-11 11:46:22.4145025',NULL,'org-2','academic-session-1',0,'Superheroes: Theory and Practice','2018-01-11 11:46:22.4145018','["13","UG"]','["15058","15098"]','["resource-3"]');

INSERT INTO [Resources] (Id,CreatedAt,Metadata,[Status],UpdatedAt,Title,Importance,VendorResourceId,VendorId,ApplicationId,_Roles) VALUES ('resource-2','2018-01-11 11:46:22.5779789',NULL,0,'2018-01-11 11:46:22.5779789','Espresso Dynamics (2nd Edition)','1','vendor-resource-2','vendor-1','application-1','[1,0]');
INSERT INTO [Resources] (Id,CreatedAt,Metadata,[Status],UpdatedAt,Title,Importance,VendorResourceId,VendorId,ApplicationId,_Roles) VALUES ('resource-1','2018-01-11 11:46:22.575773',NULL,0,'2018-01-11 11:46:22.5757726','Fundamentals of Cappucino Analysis (1st Edition)','0','vendor-resource-1','vendor-1','application-1','[7]');
INSERT INTO [Resources] (Id,CreatedAt,Metadata,[Status],UpdatedAt,Title,Importance,VendorResourceId,VendorId,ApplicationId,_Roles) VALUES ('resource-4','2018-01-11 11:46:22.5779877',NULL,0,'2018-01-11 11:46:22.5779877','Journal of Overcaffeinated Scientists','1','vendor-resource-4','vendor-3','application-1',NULL);
INSERT INTO [Resources] (Id,CreatedAt,Metadata,[Status],UpdatedAt,Title,Importance,VendorResourceId,VendorId,ApplicationId,_Roles) VALUES ('resource-3','2018-01-11 11:46:22.5779869',NULL,0,'2018-01-11 11:46:22.5779866','Journal of Superhero Science','0','vendor-resource-3','vendor-2','application-1',NULL);

INSERT INTO [UserAgents] (Id,AgentUserId,CreatedAt,Metadata,[Status],SubjectUserId,UpdatedAt) VALUES ('user-agent-1','user-3','2018-01-11 11:46:22.5498776',NULL,0,'user-2','2018-01-11 11:46:22.5498772');

INSERT INTO [LineItemCategories] (Id,CreatedAt,Metadata,[Status],Title,UpdatedAt) VALUES ('category-1','2018-01-11 11:46:22.3864621',NULL,0,'Homework','2018-01-11 11:46:22.3864617');
INSERT INTO [LineItemCategories] (Id,CreatedAt,Metadata,[Status],Title,UpdatedAt) VALUES ('category-2','2018-01-11 11:46:22.3866433',NULL,0,'Labwork','2018-01-11 11:46:22.3866429');

INSERT INTO [Klasses] (Id,ClassCode,ClassType,CourseId,CreatedAt,[Location],Metadata,SchoolOrgId,[Status],Title,UpdatedAt,_grades,_periods,_subjectCodes,_resources) VALUES ('class-1','CAF304-2017A',1,'course-1','2018-01-11 11:46:22.4316479','Chemistry Lab 4',NULL,'org-2',0,'Advanced Caffeination Techniques','2018-01-11 11:46:22.4316475','["13","UG"]','["1"]','["03098"]','["resource-4"]');
INSERT INTO [Klasses] (Id,ClassCode,ClassType,CourseId,CreatedAt,[Location],Metadata,SchoolOrgId,[Status],Title,UpdatedAt,_grades,_periods,_subjectCodes,_resources) VALUES ('class-2','CAF304-2017B',1,'course-1','2018-01-11 11:46:22.4339752','Chemistry Lab 4',NULL,'org-2',0,'Advanced Caffeination Techniques','2018-01-11 11:46:22.4339749','["13","UG"]','["3","6"]','["03098"]',NULL);

INSERT INTO [KlassAcademicSessions] (Id,AcademicSessionId,CreatedAt,KlassId,Metadata,[Status],UpdatedAt) VALUES ('class-academic-session-1','academic-session-1','2018-01-11 11:46:22.5404924','class-1',NULL,0,'2018-01-11 11:46:22.540492');
INSERT INTO [KlassAcademicSessions] (Id,AcademicSessionId,CreatedAt,KlassId,Metadata,[Status],UpdatedAt) VALUES ('class-academic-session-2','academic-session-1','2018-01-11 11:46:22.5407214','class-2',NULL,0,'2018-01-11 11:46:22.540721');

INSERT INTO [Demographics] (Id,CreatedAt,Metadata,[Status],UpdatedAt,BirthDate,Sex,AmericanIndianOrAlaskaNative,Asian,BlackOrAfricanAmerican,NativeHawaiianOrOtherPacificIslander,White,DemographicRaceTwoOrMoreRaces,HispanicOrLatinoEthnicity,CountryOfBirthCode,StateOfBirthAbbreviation,CityOfBirth,PublicSchoolResidenceStatus) VALUES ('user-3','2018-01-11 11:46:22.5691147',NULL,0,'2018-01-11 11:46:22.5691144','0001-01-01 00:00:00','1','0','0','0','0','1','0','0','FR',NULL,'Paris','1653');
INSERT INTO [Demographics] (Id,CreatedAt,Metadata,[Status],UpdatedAt,BirthDate,Sex,AmericanIndianOrAlaskaNative,Asian,BlackOrAfricanAmerican,NativeHawaiianOrOtherPacificIslander,White,DemographicRaceTwoOrMoreRaces,HispanicOrLatinoEthnicity,CountryOfBirthCode,StateOfBirthAbbreviation,CityOfBirth,PublicSchoolResidenceStatus) VALUES ('user-2','2018-01-11 11:46:22.5690057',NULL,0,'2018-01-11 11:46:22.5690057','0001-01-01 00:00:00','1','0','0','0','0','1','0','0','US','CA','San Francisco','1653');
INSERT INTO [Demographics] (Id,CreatedAt,Metadata,[Status],UpdatedAt,BirthDate,Sex,AmericanIndianOrAlaskaNative,Asian,BlackOrAfricanAmerican,NativeHawaiianOrOtherPacificIslander,White,DemographicRaceTwoOrMoreRaces,HispanicOrLatinoEthnicity,CountryOfBirthCode,StateOfBirthAbbreviation,CityOfBirth,PublicSchoolResidenceStatus) VALUES ('user-1','2018-01-11 11:46:22.5681826',NULL,0,'2018-01-11 11:46:22.5681819','0001-01-01 00:00:00','0','1','0','0','0','0','0','0','US','AK','Juneau','1652');

INSERT INTO [Enrollments] (Id,BeginDate,CreatedAt,EndDate,KlassId,Metadata,[Primary],[Role],SchoolOrgId,[Status],UpdatedAt,UserId) VALUES ('enrollment-1','0001-01-01 00:00:00','2018-01-11 11:46:22.5132488','0001-01-01 00:00:00','class-1',NULL,NULL,7,'org-2',0,'2018-01-11 11:46:22.5132484','user-2');
INSERT INTO [Enrollments] (Id,BeginDate,CreatedAt,EndDate,KlassId,Metadata,[Primary],[Role],SchoolOrgId,[Status],UpdatedAt,UserId) VALUES ('enrollment-2','0001-01-01 00:00:00','2018-01-11 11:46:22.5137575','0001-01-01 00:00:00','class-1',NULL,NULL,6,'org-2',0,'2018-01-11 11:46:22.5137572','user-1');

INSERT INTO [LineItems] (Id,AcademicSessionId,AssignDate,CreatedAt,[Description],DueDate,KlassId,LineItemCategoryId,Metadata,ResultValueMax,ResultValueMin,[Status],Title,UpdatedAt) VALUES ('line-item-1','academic-session-1','2017-03-01 00:00:00','2018-01-11 11:46:22.4611211','You must drink at least one cup of coffee, no decaf','2017-04-01 00:00:00','class-1','category-1',NULL,100.0,0.0,0,'Drink Coffee','2018-01-11 11:46:22.4611207');
INSERT INTO [LineItems] (Id,AcademicSessionId,AssignDate,CreatedAt,[Description],DueDate,KlassId,LineItemCategoryId,Metadata,ResultValueMax,ResultValueMin,[Status],Title,UpdatedAt) VALUES ('line-item-2','academic-session-1','2017-04-05 00:00:00','2018-01-11 11:46:22.4625586',NULL,'2017-04-05 00:00:00','class-1','category-2',NULL,100.0,0.0,0,'Intravenous Application','2018-01-11 11:46:22.4625582');

INSERT INTO [Results] (Id,Comment,CreatedAt,LineItemId,Metadata,Score,ScoreDate,ScoreStatus,[Status],StudentUserId,UpdatedAt) VALUES ('result-1',NULL,'2018-01-11 11:46:22.5301874','line-item-1',NULL,75.4000015258789,'2017-04-20 00:00:00',1,0,'user-1','2018-01-11 11:46:22.5301871');
INSERT INTO [Results] (Id,Comment,CreatedAt,LineItemId,Metadata,Score,ScoreDate,ScoreStatus,[Status],StudentUserId,UpdatedAt) VALUES ('result-2',NULL,'2018-01-11 11:46:22.5307615','line-item-2',NULL,95.3000030517578,'2017-04-10 00:00:00',3,0,'user-1','2018-01-11 11:46:22.5307615');